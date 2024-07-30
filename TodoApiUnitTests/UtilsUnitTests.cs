using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Models;

namespace TodoApiUnitTests
{
    [TestClass]
    public class UtilsUnitTests
    {
        [TestMethod]
        public void IsItemNameValidReturnsFalseWhenItemNameIsEmptyString()
        {
            TodoItem item = new TodoItem();
            item.Name = "";

            var result = TodoApi.Utils.IsItemNameValid(item);

            Assert.IsFalse(result);

        }
    }
}
