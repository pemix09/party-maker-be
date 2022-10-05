using Xunit;
using API.Controllers;
using Application.User.Commands;

namespace Tests;

public class UserControllerTests
{
    [Fact]
    public void Register_Works_Well()
    {
        //Arrange
        var registerCommand = RegisterUserCommand();

        //Act

        //Assert
    }
}