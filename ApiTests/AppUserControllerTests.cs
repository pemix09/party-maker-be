using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Controllers;
using Application.User.Commands;
using Core.Models;
using DataFaker;
using MediatR;
using Moq;
using Persistence.Services.Database;
using Xunit;

namespace ApiTests;

public class AppUserControllerTests
{
    private readonly Mock<IMediator> mediator = new Mock<IMediator>();

    [Fact]
    public async Task Register_ShouldCreateUSer()
    {
        //Arrange
        mediator
            .Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value) //<-- return Task to allow await to continue
            .Verifiable("Notification was not sent.");
        
        RegisterUserCommand command = FakeDataFactory.GenerateFakeRegisterRequest();
        
        //Act
        Unit result = await mediator.Object.Send(command);

        //Assert
        mediator.Verify(x => x.Send(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task RegisterControllerMethod_ShouldReturnOk()
    {
        //Arrange
        mediator
            .Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value) //<-- return Task to allow await to continue
            .Verifiable("Notification was not sent.");

        var userController = new UserController()
    }
    
}