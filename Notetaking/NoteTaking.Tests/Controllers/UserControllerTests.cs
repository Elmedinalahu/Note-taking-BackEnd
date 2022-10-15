

using Notetaking.Controllers;
using Notetaking.Data;

namespace NoteTaking.Tests.Controllers
{
    public class UserControllerTests
    {
        UserController _controller;

        public UserControllerTests(UserController controller)
        {
            _controller = controller;
        }

        [Fact]
        public void GetAllTest()
        {
            // Arrange
            var result = _controller.GetType();

            // Act

            // Assert

        }
    }
}
