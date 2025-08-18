
using Dapper;
using System.Data;
using WebApp.Models;

namespace WebApp.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IDbConnection _db;
    private readonly IProductRepository _products;
    private readonly INotificationRepository _notifications;

    public OrderRepository(IDbConnection db, IProductRepository products, INotificationRepository notifications)
    {
        _db = db;
        _products = products;
        _notifications = notifications;
    }

    public async Task<IEnumerable<Order>> GetAllAsync(int? customerId, string? status)
    {
        var sql = @"SELECT o.*, c.* FROM Orders o
                    INNER JOIN Customers c ON c.Id = o.CustomerId
                    WHERE (@cid IS NULL OR o.CustomerId = @cid)
                      AND (@st IS NULL OR o.Status = @st)
                    ORDER BY o.OrderDate DESC";
        var lookup = new Dictionary<int, Order>();
        var list = await _db.QueryAsync<Order, Customer, Order>(sql, (o, c) =>
        {
            if (!lookup.TryGetValue(o.Id, out var order))
            {
                order = o;
                order.Customer = c;
                lookup[o.Id] = order;
            }
            return order;
        }, new { cid = customerId, st = string.IsNullOrWhiteSpace(status) ? null : status });

        return lookup.Values;
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        var sqlOrder = @"SELECT o.*, c.* FROM Orders o
                         INNER JOIN Customers c ON c.Id = o.CustomerId
                         WHERE o.Id=@id;";
        var order = (await _db.QueryAsync<Order, Customer, Order>(sqlOrder, (o,c) => { o.Customer = c; return o; }, new { id })).FirstOrDefault();
        if (order == null) return null;

        var sqlItems = @"SELECT i.*, p.* FROM OrderItems i
                         INNER JOIN Products p ON p.Id = i.ProductId
                         WHERE i.OrderId=@id";
        var items = await _db.QueryAsync<OrderItem, Product, OrderItem>(sqlItems, (i,p) => { i.Product = p; return i; }, new { id });
        order.Items = items.ToList();
        return order;
    }

    public async Task<int> CreateAsync(Order order, bool decreaseStock = true)
    {
        using var tx = _db.BeginTransaction();
        try
        {
            var orderId = await _db.ExecuteScalarAsync<int>(@"
                INSERT INTO Orders (CustomerId, OrderDate, Total, Status)
                VALUES (@CustomerId, SYSUTCDATETIME(), @Total, @Status);
                SELECT CAST(SCOPE_IDENTITY() AS INT);", 
                new { order.CustomerId, order.Total, order.Status }, tx);

            foreach (var item in order.Items)
            {
                
                var has = await _products.HasStockAsync(item.ProductId, item.Quantity);
                if (!has) throw new InvalidOperationException($"Insufficient stock for product {item.ProductId}");

               
                var price = await _db.ExecuteScalarAsync<decimal>("SELECT Price FROM Products WHERE Id=@id", new { id = item.ProductId }, tx);
                await _db.ExecuteAsync(@"
                    INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice)
                    VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice);",
                    new { OrderId = orderId, ProductId = item.ProductId, Quantity = item.Quantity, UnitPrice = price }, tx);

                if (decreaseStock)
                    await _db.ExecuteAsync(@"UPDATE Products SET StockQuantity = StockQuantity - @q WHERE Id=@id",
                        new { id = item.ProductId, q = item.Quantity }, tx);
            }

            
            var total = await _db.ExecuteScalarAsync<decimal>(@"
                SELECT SUM(CAST(Quantity AS DECIMAL(18,2)) * UnitPrice) FROM OrderItems WHERE OrderId=@id", new { id = orderId }, tx);
            await _db.ExecuteAsync("UPDATE Orders SET Total=@t WHERE Id=@id", new { t = total, id = orderId }, tx);

            tx.Commit();
            return orderId;
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public async Task UpdateStatusAsync(int orderId, string newStatus)
    {
        var oldStatus = await _db.ExecuteScalarAsync<string>("SELECT Status FROM Orders WHERE Id=@id", new { id = orderId });
        await _db.ExecuteAsync("UPDATE Orders SET Status=@st WHERE Id=@id", new { id = orderId, st = newStatus });
        await _notifications.CreateAsync(new Notification
        {
            OrderId = orderId,
            OldStatus = oldStatus,
            NewStatus = newStatus,
            Message = $"Order #{orderId} status changed from {oldStatus} to {newStatus}."
        });
    }
}
