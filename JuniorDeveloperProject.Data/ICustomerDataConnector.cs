using JuniorDeveloperProject.Models;

namespace JuniorDeveloperProject.Data
{
    public interface ICustomerDataConnector
    {
        public Task<IEnumerable<UserBase>> GetUsersByCity(HttpClient httpClient, string apiBaseUrl, string apiGetUSersByCityPath, string city = "London");
        public Task<IEnumerable<UserBase>> GetAllUsers(HttpClient httpClient, string apiBaseUrl, string TechTestApiGetUsersPath);
        public double GetDistance(double longitude1, double latitude1, double longitude2, double latitude2);
    }
}
