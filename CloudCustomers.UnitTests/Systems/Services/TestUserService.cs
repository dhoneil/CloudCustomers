using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUserService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            //arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpclient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com/users";
            var config = Options.Create(
            new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpclient, config);


            //act
            await sut.GetAllUsers();

            //assert
            //verify http request was made!
            handlerMock
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                );
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsListEmptyListOfUsers()
        {
            //arrange
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var httpclient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com/users";
            var config = Options.Create(
            new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpclient, config);


            //act
            var result = await sut.GetAllUsers();

            //assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            //arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpclient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com/users";
            var config = Options.Create(
            new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpclient, config);


            //act
            var result = await sut.GetAllUsers();

            //assert
            result.Count.Should().Be(expectedResponse.Count());
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
        {
            //arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var endpoint = "https://example.com/users";
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endpoint);
            var httpclient = new HttpClient(handlerMock.Object);
            var config = Options.Create(
            new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpclient, config);


            //act
            var result = await sut.GetAllUsers();
            var url = new Uri(endpoint);

            //assert
            handlerMock
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(
                        req => req.Method == HttpMethod.Get && req.RequestUri == url),
                    ItExpr.IsAny<CancellationToken>()
                );
        }
    }
}
