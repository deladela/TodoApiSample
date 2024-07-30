using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApi.Controllers;
using TodoApi.Models;
using Shared.Models;
using Moq;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Http;

namespace TodoControllerTests;
public class TodoControllerUnitTests
{
    private readonly ITestOutputHelper _output;
    private readonly TodoContext _context;
    private readonly TodoController _controller;
    private readonly DefaultHttpContext _httpContext = new DefaultHttpContext();

    public TodoControllerUnitTests(ITestOutputHelper output)
    {
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoList")
            .Options;

        _context = new TodoContext(options);
        _output = output;
        _httpContext.Request.Headers["CanAccess"] = "true";

        // Ensure the database is deleted to avoid duplicate key issues
        _context.Database.EnsureDeleted();

        // Seed the in-memory database with test data
        _context.TodoItems.AddRange(new List<TodoItem>
        {
            new TodoItem { Id = 1, Name = "Test Item 1", IsComplete = false },
            new TodoItem { Id = 2, Name = "Test Item 2", IsComplete = true }
        });
        _context.SaveChanges();

        var mockLogger = new Mock<ILogger<TodoController>>();
        _controller = new TodoController(_context, mockLogger.Object);
    }

    [Fact]
    public void GetAll_ReturnsAllItems()
    {
        // Act
        var result = _controller.GetAll();

        // Assert
        var actionResult = Assert.IsType<ActionResult<List<TodoItem>>>(result);
        _output.WriteLine("ActionResult type assertion passed.");

        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        _output.WriteLine("OkObjectResult type assertion passed.");

        var returnValue = Assert.IsType<List<TodoItem>>(okResult.Value);
        _output.WriteLine("ReturnValue type assertion passed.");

        Assert.Equal(2, returnValue.Count);
        _output.WriteLine("Count assertion passed. Number of items: " + returnValue.Count);
    }

    [Fact]
    public void Create_ReturnsCreatedAtRoute_WhenItemIsValid()
    {
        // Arrange
        var newItem = new TodoItem
        {
            Id = 3,
            Name = "Created Valid Todo Item",
            DateDue = DateTime.Now.AddDays(1)
        };

        // Act
        // Simulate authorization header
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = _httpContext
        };
        var result = _controller.Create(newItem);

        // Assert
        var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
        Assert.Equal(201, createdAtRouteResult.StatusCode);
        Assert.Equal("GetTodo", createdAtRouteResult.RouteName);
        Assert.Equal(newItem.Id, ((TodoItem)createdAtRouteResult.Value).Id);
    }

    [Fact]
    public void Create_ReturnsBadRequest_WhenItemNameIsInvalid()
    {
        // Arrange
        var newItem = new TodoItem
        {
            Id = 4,
            Name = "", // Invalid name
            DateDue = DateTime.Now.AddDays(1)
        };

        // Act
        // Simulate authorization header
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = _httpContext
        };
        var result = _controller.Create(newItem);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Name is required and must be 1-255 chars long.", badRequestResult.Value);
    }

    [Fact]
    public void Create_ReturnsUnauthorized_WhenAuthorizationHeaderIsMissing()
    {
        // Arrange
        _httpContext.Request.Headers.Remove("CanAccess");
        var newItem = new TodoItem
        {
            Id = 5,
            Name = "Test Item",
            DateDue = DateTime.Now.AddDays(1)
        };

        // Simulate missing authorization header
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        var result = _controller.Create(newItem);

        // Assert
        var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result);
        Assert.Equal(401, unauthorizedResult.StatusCode);
    }
}