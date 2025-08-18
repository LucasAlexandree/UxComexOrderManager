
using Dapper;
using System.Data;
using WebApp.Models;

namespace WebApp.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly IDbConnection _db;
    public NotificationRepository(IDbConnection db) => _db = db;

    public async Task CreateAsync(Notification n)
    {
        var sql = @"INSERT INTO Notifications (OrderId, OldStatus, NewStatus, Message)
                    VALUES (@OrderId, @OldStatus, @NewStatus, @Message)";
        await _db.ExecuteAsync(sql, n);
    }
}
