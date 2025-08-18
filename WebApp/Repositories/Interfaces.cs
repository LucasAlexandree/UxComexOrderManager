
using WebApp.Models;

namespace WebApp.Repositories;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync(string? search);
    Task<Customer?> GetByIdAsync(int id);
    Task<int> CreateAsync(Customer c);
    Task UpdateAsync(Customer c);
    Task DeleteAsync(int id);
}

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(string? search);
    Task<Product?> GetByIdAsync(int id);
    Task<int> CreateAsync(Product p);
    Task UpdateAsync(Product p);
    Task DeleteAsync(int id);
    Task<bool> HasStockAsync(int productId, int quantity);
    Task DecreaseStockAsync(int productId, int quantity);
}

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync(int? customerId, string? status);
    Task<Order?> GetByIdAsync(int id);
    Task<int> CreateAsync(Order order, bool decreaseStock = true);
    Task UpdateStatusAsync(int orderId, string newStatus);
}

public interface INotificationRepository
{
    Task CreateAsync(Notification n);
}
