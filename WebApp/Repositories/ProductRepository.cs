
using Dapper;
using System.Data;
using WebApp.Models;

namespace WebApp.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IDbConnection _db;
    public ProductRepository(IDbConnection db) => _db = db;

    public async Task<IEnumerable<Product>> GetAllAsync(string? search)
    {
        var sql = @"SELECT * FROM Products
                    WHERE (@s IS NULL OR Name LIKE '%' + @s + '%')
                    ORDER BY Name ASC";
        return await _db.QueryAsync<Product>(sql, new { s = string.IsNullOrWhiteSpace(search) ? null : search });
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<Product>(
            "SELECT * FROM Products WHERE Id=@id", new { id });
    }

    public async Task<int> CreateAsync(Product p)
    {
        var sql = @"INSERT INTO Products (Name, Description, Price, StockQuantity)
                    VALUES (@Name, @Description, @Price, @StockQuantity);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
        return await _db.ExecuteScalarAsync<int>(sql, p);
    }

    public async Task UpdateAsync(Product p)
    {
        var sql = @"UPDATE Products SET Name=@Name, Description=@Description, Price=@Price, StockQuantity=@StockQuantity
                    WHERE Id=@Id";
        await _db.ExecuteAsync(sql, p);
    }

    public async Task DeleteAsync(int id)
    {
        await _db.ExecuteAsync("DELETE FROM Products WHERE Id=@id", new { id });
    }

    public async Task<bool> HasStockAsync(int productId, int quantity)
    {
        var stock = await _db.QueryFirstOrDefaultAsync<int>(
            "SELECT StockQuantity FROM Products WHERE Id=@id", new { id = productId });
        return stock >= quantity;
    }

    public async Task DecreaseStockAsync(int productId, int quantity)
    {
        var sql = @"UPDATE Products SET StockQuantity = StockQuantity - @q WHERE Id=@id";
        await _db.ExecuteAsync(sql, new { id = productId, q = quantity });
    }
}
