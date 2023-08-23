using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using Microsoft.Extensions.Options;
using System.Net;

namespace CloudCustomers.API.Services
{
    public interface IAuthService
    {
        Task<User> AuthenticateUser(string email, string passWord);
        object GetCurrentUserId();
    }

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly UsersApiOptions apiConfig;

        public AuthService(HttpClient httpClient, IOptions<UsersApiOptions> apiConfig)
        {
            _httpClient = httpClient;
            this.apiConfig = apiConfig.Value;
        }
        public object GetCurrentUserId()
        {
            throw new NotImplementedException();
        }
        public async Task<User> AuthenticateUser(string email, string passWord)
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
