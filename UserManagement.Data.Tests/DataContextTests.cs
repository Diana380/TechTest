using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data.Entities;
using UserManagement.Data.Models;
//using FluentAssertions;
using UserManagement.Data.Repositories;

namespace UserManagement.Data.Tests;

public class DataContextTests
{
    
    [Fact]
    public async Task GetAll_WhenNewEntityAdded_MustIncludeNewEntity()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var context = CreateContext();
        var repository = new UserRepository(context);

        var entity = new User
        {
            Forename = "Brand New",
            Surname = "User",
            Email = "brandnewuser@example.com"
        };
        await repository.CreateAsync(entity);

        // Act: Invokes the method under test with the arranged parameters.
        var result = repository.GetAll<User>();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result
            .Should().Contain(s => s.Email == entity.Email)
            .Which.Should().BeEquivalentTo(entity);
    }

    [Fact]
    public async Task GetAll_WhenDeleted_MustNotIncludeDeletedEntity()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var context = CreateContext();
        var repository = new UserRepository(context);
        var entity = repository.GetAll<User>().Where(x =>x.Forename == "Brand New").ToList();
        foreach (var user in entity)
        {
            await repository.DeleteAsync(user);
        }
       

        // Act: Invokes the method under test with the arranged parameters.
        var result = repository.GetAll<User>();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().NotContain(s => s.Forename == "Brand New");
    }
    
 
    private DataContext CreateContext()
    {
        var userList = new List<User>();
        var queryable = userList.AsQueryable();

        var mockSet = new Mock<DbSet<User>>();
        mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(() => userList.AsQueryable().Provider);
        mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(() => userList.AsQueryable().Expression);
        mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(() => userList.AsQueryable().ElementType);
        mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => userList.AsQueryable().GetEnumerator());

        mockSet.Setup(m => m.Add(It.IsAny<User>())).Callback<User>(user => userList.Add(user));

        var context = new Mock<DataContext>();
        context.Setup(c => c.Set<User>()).Returns(mockSet.Object);
        return context.Object;
    }
}
