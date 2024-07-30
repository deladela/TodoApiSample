using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApi.Controllers;
using TodoApi.Models;
using Shared.Models;
using Moq;
using Xunit.Abstractions;

namespace TodoControllerTests;
public class TodoControllerUnitTests
{
    private readonly ITestOutputHelper _output;
    private readonly TodoContext _context;
    private readonly TodoController _controller;

    public TodoControllerUnitTests(ITestOutputHelper output)
    {
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoList")
            .Options;

        _context = new TodoContext(options);
        _output = output;

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
}