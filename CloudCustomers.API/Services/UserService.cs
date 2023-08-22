using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using Microsoft.Extensions.Options;
using System.Net;

namespace CloudCustomers.API.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
    }
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly UsersApiOptions apiConfig;

        public UserService(HttpClient httpClient,IOptions<UsersApiOptions> apiConfig)
        {
            _httpClient = httpClient;
            this.apiConfig = apiConfig.Value;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var usersResponse = await _httpClient.GetAsync(this.apiConfig.Endpoint);
            if (usersResponse.StatusCode == HttpStatusCode.NotFound) 
            {
                return new List<User>();
            }
            var responsecontent = usersResponse.Content;
            var allusers = await responsecontent.ReadFromJsonAsync<List<User>>();
            return allusers.ToList();
        }


    }
}
