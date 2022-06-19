using JuniorDeveloperProject.Models;

namespace JuniorDeveloperProject.Data
{
    public interface ICustomerDataConnector
    {
        public Task<ResponseBase> GetUsersByCity(HttpClient httpClient, string apiBaseUrl, string apiGetUSersByCityPath, string city = "London");
        public Task<ResponseBase> GetAllUsers(HttpClient httpClient, string apiBaseUrl, string TechTestApiGetUsersPath);
        public double GetDistance(double longitude1, double latitude1, double longitude2, double latitude2);
    }
}
