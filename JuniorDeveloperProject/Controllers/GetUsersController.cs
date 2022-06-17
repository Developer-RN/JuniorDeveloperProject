using JuniorDeveloperProject.Data;
using JuniorDeveloperProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace JuniorDeveloperProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetUsersController:Controller
    {
        #region README
        /// <summary>
        /// Learning resources used:
        /// Dependency injection pattern - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0
        /// Configuration in .NET Core - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-6.0
        /// Newtonsoft JSON desirializer documentation = https://www.newtonsoft.com/json/help/html/deserializeobject.htm
        /// </summary>
        #endregion

        #region FIELDS & PROPERTIES
        private readonly ILogger<GetUsersController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ICustomerDataConnector _dataLayer;
        #endregion

        #region CTOR
        public GetUsersController(ILogger<GetUsersController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration, ICustomerDataConnector dataLayer)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _dataLayer = dataLayer;
        }
        #endregion

        #region ACTIONS
        [HttpGet]
        public IEnumerable<UserBase> Get(string city = "London")
        {
            //throw new ArgumentException("Text Exception");
            string searchDistance = _configuration["ApplicationSettings:DefaultDistanceInMiles"];
            string searchBaseLatitude = _configuration["ApplicationSettings:DefaultCordinatesLat"];
            string searchBaseLongitude = _configuration["ApplicationSettings:DefaultCordinatesLong"];

            string apiBaseUrl = _configuration["ApplicationSettings:TechTestApiBaseUrl"];
            string apiGetUSersByCityPath = _configuration["ApplicationSettings:TechTestApiGetUSersByCityPath"];
            string TechTestApiGetUsersPath = _configuration["ApplicationSettings:TechTestApiGetUsersPath"];

            var httpClient = _httpClientFactory.CreateClient();

            List<UserBase> allUsersList= new List<UserBase>();

            getUsersFromLondon(allUsersList, httpClient, apiBaseUrl, apiGetUSersByCityPath, city);
            getUsersByDistance(allUsersList, httpClient, apiBaseUrl, TechTestApiGetUsersPath, searchBaseLongitude, searchBaseLatitude, searchDistance);

            if (allUsersList.Count() > 0)
            {
                return allUsersList.Distinct().ToList();
            }
            else
            {
                return allUsersList;
            }

        }
        #endregion
 private void getUsersFromLondon(List<UserBase> allUsersList, HttpClient httpClient, string apiBaseUrl, string apiGetUSersByCityPath, string city)
            {

            List<UserBase> usersList = _dataLayer.GetUsersByCity(httpClient, apiBaseUrl, apiGetUSersByCityPath, city).Result.ToList();
            if (usersList == null) { usersList = new List<UserBase>(); };

            foreach (var user in usersList)
            {
                if (String.IsNullOrEmpty(user.City))
                {
                    user.City = "London";
                }
                allUsersList.Add(user);
            }
        }
    
        private void getUsersByDistance(List<UserBase> allUsersList, HttpClient httpClient, string apiBaseUrl, string TechTestApiGetUsersPath, string searchBaseLongitude, string searchBaseLatitude, string searchDistance)
        {
            List<UserBase> allOtherUsers = _dataLayer.GetAllUsers(httpClient, apiBaseUrl, TechTestApiGetUsersPath).Result.ToList();
            if (allOtherUsers != null)
            {
                foreach (var user in allOtherUsers)
                {
                    double distanceInMiles = _dataLayer.GetDistance(Convert.ToDouble(searchBaseLongitude), Convert.ToDouble(searchBaseLatitude), user.Longitude, user.Latitude);

                    if (distanceInMiles <= Convert.ToInt64(searchDistance))
                    {
                        user.City = "Distance from London - " + distanceInMiles + " miles";
                        allUsersList.Add(user);
                    }
                }
            }
        }
}
}
