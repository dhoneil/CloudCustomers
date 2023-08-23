using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Runtime.InteropServices;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnStatusCode200()
    {
        //arrange
        var mockAuthService = new Mock<IAuthService>();
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(s=>s.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mockUserService.Object, mockAuthService.Object);

        //act
        var result = (OkObjectResult) await sut.Get();


        //assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokeUserServiceExactlyOnce()
    {
        //arrange
        var mockAuthService = new Mock<IAuthService>();
        var mockUsersService = new Mock<IUserService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUsersService.Object, mockAuthService.Object);


        //act
        var result = await sut.Get();

        //assert
        mockUsersService.Verify(s => s.GetAllUsers(), Times.Once);
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListofUsers()
    {
        //arrange
        var mockAuthService = new Mock<IAuthService>();
        var mockUsersService = new Mock<IUserService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mockUsersService.Object, mockAuthService.Object);

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
        var mockAuthService = new Mock<IAuthService>();
        var mockUsersService = new Mock<IUserService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUsersService.Object, mockAuthService.Object);

        //act
        var res = await sut.Get();

        //assert
        res.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)res;
        objectResult.StatusCode.Should().Be(404);
    }





    // Test Name: Test Retrieving User Profile for Authenticated User    // Purpose: 
    //    This test is intended to verify that an authenticated user can retrieve 
    //    their profile details successfully. We want to ensure that upon providing 
    //    valid credentials and user ID, the system returns the correct profile 
    //    information.
    // 
    // Method Under Test:
    //    UserProfileService.GetUserProfile(userId)
    // 
    // Input:
    //    A valid user ID representing an authenticated user within the system. 
    //    This ID should correspond to an actual user in the test database or mock 
    //    repository.
    // 
    // Expected Output:
    //    The expected output is a user profile object containing the user's 
    //    username, email, name, and contact information. It should match the 
    //    predefined or mocked details for the given user ID.
    // 
    // Notes:
    //    The test must include proper authentication handling to simulate a real 
    //    user's session. Depending on the method's dependencies, mocking or 
    //    stubbing may be required for services like authentication, database 
    //    access, etc.
    [Fact]
    public async Task GetUserProfile_OnSuccess_ReturnsUserProfile()
    {
        //arrange
        var mockAuthService = new Mock<IAuthService>();
        var mockUserProfileService = new Mock<IUserService>();
        mockUserProfileService
            .Setup(service => service.GetUserProfile(It.IsAny<int>()))
            .ReturnsAsync(UsersFixture.GetAuthenticatedUser());

        var sut = new UsersController(mockUserProfileService.Object, mockAuthService.Object);

        //act
        var res = await sut.GetUserProfile(1);

        //assert
        res.Should().BeOfType<OkObjectResult>();
        var objectRes = (OkObjectResult)res;
        objectRes.Value.Should().BeOfType<User>();
    }

    [Fact]

    public async Task AuthenticateUser_OnSuccess_ReturnStatusCode200()
    {
        // Arrange
        var mockUserProfileService = new Mock<IUserService>();
        mockUserProfileService
            .Setup(service => service.GetUserProfile(It.IsAny<int>()))
            .ReturnsAsync(UsersFixture.GetAuthenticatedUser());

        var mockAuthService = new Mock<IAuthService>();
        mockAuthService
            .Setup(auth => auth.AuthenticateUser("test@gmail.com", "1234"))
            .ReturnsAsync(UsersFixture.GetAuthenticatedUser());

        var sut = new UsersController(mockUserProfileService.Object, mockAuthService.Object);


        // Act
        var res = await sut.AuthenticateUser("test@gmail.com", "1234");

        // Assert
        res.Should().BeOfType<OkObjectResult>();
        var objectRes = (OkObjectResult)res;

        Assert.True(((User)objectRes.Value).IsAuthenticated);

        objectRes.Value.Should().BeOfType<User>();
    }









}