using Shared.Models;

namespace Helpers {
    public static class TestData
    {
        public static List<TodoItem> GetTestTodoItems()
        {
            return new List<TodoItem>
            {
                new TodoItem { Id = 1, Name = "Test Item 1", IsComplete = false },
                new TodoItem { Id = 2, Name = "Test Item 2", IsComplete = true }
            };
        }
    }
}
