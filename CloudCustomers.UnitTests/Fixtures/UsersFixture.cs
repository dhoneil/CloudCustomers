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
                    Address = new Address
                    {
                        Street = "333 market st",
                        City = "Somewhere",
                        ZipCode = "333zip",
                    }
                }
            };
        }
    }
}
