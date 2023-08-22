using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnStatusCode200()
    {
        //arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(s=>s.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mockUserService.Object);

        //act
        var result = (OkObjectResult) await sut.Get();


        //assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokeUserServiceExactlyOnce()
    {
        //arrange
        var mockUsersService = new Mock<IUserService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUsersService.Object);


        //act
        var result = await sut.Get();

        //assert
        mockUsersService.Verify(s => s.GetAllUsers(), Times.Once);
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListofUsers()
    {
        //arrange
        var mockUsersService = new Mock<IUserService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mockUsersService.Object);

        //act
        var res = await sut.Get();

        //assert
        res.Should().BeOfType<OkObjectResult>();
        var objectRes = (OkObjectResult)res;
        objectRes.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task Get_OnNoUsersFound_Returns404()
    {
        //arrange
        var mockUsersService = new Mock<IUserService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUsersService.Object);

        //act
        var res = await sut.Get();

        //assert
        res.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)res;
        objectResult.StatusCode.Should().Be(404);
    }

    

}