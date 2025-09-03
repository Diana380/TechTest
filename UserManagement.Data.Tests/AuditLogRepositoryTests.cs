using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using UserManagement.Data.Entities;
using UserManagement.Data.Repositories;
using Xunit;

namespace UserManagement.Data.Tests;

public class AuditLogRepositoryTests
{
    private Mock<DbSet<AuditLog>> CreateMockSet(List<AuditLog> backingList)
    {
        var mockSet = new Mock<DbSet<AuditLog>>();
        mockSet.As<IQueryable<AuditLog>>().Setup(m => m.Provider).Returns(() => backingList.AsQueryable().Provider);
        mockSet.As<IQueryable<AuditLog>>().Setup(m => m.Expression).Returns(() => backingList.AsQueryable().Expression);
        mockSet.As<IQueryable<AuditLog>>().Setup(m => m.ElementType).Returns(() => backingList.AsQueryable().ElementType);
        mockSet.As<IQueryable<AuditLog>>().Setup(m => m.GetEnumerator()).Returns(() => backingList.AsQueryable().GetEnumerator());
        mockSet.Setup(m => m.Add(It.IsAny<AuditLog>())).Callback<AuditLog>(log => backingList.Add(log));
        return mockSet;
    }

    private DataContext CreateContext(List<AuditLog> auditLogs)
    {
        var mockSet = CreateMockSet(auditLogs);
        var context = new Mock<DataContext>();
        context.Setup(c => c.AuditLogs).Returns(mockSet.Object);
        context.Setup(c => c.SaveChangesAsync()).ReturnsAsync(1);
        return context.Object;
    }

    [Fact]
    public async Task LogAsync_AddsAuditLogToContext()
    {
        // Arrange
        var auditLogs = new List<AuditLog>();
        var context = CreateContext(auditLogs);
        var repository = new AuditLogRepository(context);

        var log = new AuditLog
        {
            Action = "Created",
            EntityName = "User",
            EntityId = 1,
            Details = "User created",
            Timestamp = System.DateTime.UtcNow
        };

        // Act
        await repository.LogAsync(log);

        // Assert
        Assert.Single(auditLogs);
        Assert.Equal("Created", auditLogs[0].Action);
        Assert.Equal("User", auditLogs[0].EntityName);
        Assert.Equal(1, auditLogs[0].EntityId);
        Assert.Equal("User created", auditLogs[0].Details);
    }

    [Fact]
    public void GetAll_ReturnsAllAuditLogs()
    {
        // Arrange
        var auditLogs = new List<AuditLog>
        {
            new AuditLog { Action = "A", EntityName = "User", EntityId = 1, Timestamp = System.DateTime.UtcNow },
            new AuditLog { Action = "B", EntityName = "User", EntityId = 2, Timestamp = System.DateTime.UtcNow }
        };
        var context = CreateContext(auditLogs);
        var repository = new AuditLogRepository(context);

        // Act
        var result = repository.GetAll().ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, l => l.Action == "A");
        Assert.Contains(result, l => l.Action == "B");
    }

    [Fact]
    public async Task LogAsync_ThrowsOnNullArgument()
    {
        // Arrange
        var context = CreateContext(new List<AuditLog>());
        var repository = new AuditLogRepository(context);

        // Act & Assert
        await Assert.ThrowsAsync<System.ArgumentNullException>(() => repository.LogAsync(null!));
    }
}
