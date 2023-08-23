using CloudCustomers.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers()
        {
            return new()
            {
                new User
                {
                    Name = "Test User 1",
                    Email = "testuser1@test.com",
                    Password = "123",
                    Address = new Address
                    {
                        Street = "111 market st",
                        City = "Somewhere",
                        ZipCode = "111zip",
                    }
                },
                new User
                {
                    Name = "Test User 2",
                    Email = "testuser2@test.com",
                    Password = "1234",
                    Address = new Address
                    {
                        Street = "222 market st",
                        City = "Somewhere",
                        ZipCode = "222zip",
                    }
                }
                ,
                new User
                {
                    Name = "Test User 3",
                    Email = "testuser3@test.com",
                    Password = "12345",
                    Address = new Address
                    {
                        Street = "333 market st",
                        City = "Somewhere",
                        ZipCode = "333zip",
                    }
                }
            };
        }

        public static User GetAuthenticatedUser()
        {
            return new()
            {
                Name = "Test User 1",
                Email = "",
                IsAuthenticated = true,
            };
        }
    }
}
