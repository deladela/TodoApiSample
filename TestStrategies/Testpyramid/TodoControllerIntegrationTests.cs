using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Shared.Models;	

namespace TodoControllerTests;
public class TodoControllerIntegrationTests : IClassFixture<WebApplicationFactory<TodoApi.Startup>>
{
    private readonly HttpClient _client;

    public TodoControllerIntegrationTests(WebApplicationFactory<TodoApi.Startup> factory)
    {
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Add("CanAccess", "true");
    }

    [Fact]
    public async Task CreateAndGetAll_ShouldReturnCreatedItem()
    {
        // Arrange
        var newItem = new TodoItem { Name = "Test Item", IsComplete = false };

        // Act - Create
        var createResponse = await _client.PostAsJsonAsync("/api/Todo", newItem);
        createResponse.EnsureSuccessStatusCode();

        // Act - GetAll
        var getAllResponse = await _client.GetAsync("/api/Todo");
        getAllResponse.EnsureSuccessStatusCode();

        var jsonString = await getAllResponse.Content.ReadAsStringAsync();
        var items = JsonConvert.DeserializeObject<List<TodoItem>>(jsonString);

        // Assert
        Assert.Contains(items, item => item.Name == "Test Item" && item.IsComplete == false);
    }
}