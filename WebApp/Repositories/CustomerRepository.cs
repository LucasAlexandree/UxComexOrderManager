
using Dapper;
using System.Data;
using WebApp.Models;

namespace WebApp.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IDbConnection _db;
    public CustomerRepository(IDbConnection db) => _db = db;

    public async Task<IEnumerable<Customer>> GetAllAsync(string? search)
    {
        var sql = @"SELECT * FROM Customers
                    WHERE (@s IS NULL OR Name LIKE '%' + @s + '%' OR Email LIKE '%' + @s + '%')
                    ORDER BY CreatedAt DESC";
        return await _db.QueryAsync<Customer>(sql, new { s = string.IsNullOrWhiteSpace(search) ? null : search });
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<Customer>(
            "SELECT * FROM Customers WHERE Id=@id", new { id });
    }

    public async Task<int> CreateAsync(Customer c)
    {
        var sql = @"INSERT INTO Customers (Name, Email, Phone, CreatedAt)
                    VALUES (@Name, @Email, @Phone, SYSUTCDATETIME());
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
        return await _db.ExecuteScalarAsync<int>(sql, c);
    }

    public async Task UpdateAsync(Customer c)
    {
        var sql = @"UPDATE Customers SET Name=@Name, Email=@Email, Phone=@Phone WHERE Id=@Id";
        await _db.ExecuteAsync(sql, c);
    }

    public async Task DeleteAsync(int id)
    {
        await _db.ExecuteAsync("DELETE FROM Customers WHERE Id=@id", new { id });
    }
}
