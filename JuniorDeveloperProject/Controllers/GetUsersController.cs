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
        public Task<ResponseBase> Get(string city = "London")
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
            string code = "";
           Task<ResponseBase> responseBaseByCity = getUsersFromLondon( code, allUsersList, httpClient, apiBaseUrl, apiGetUSersByCityPath, city);

            Task<ResponseBase> responseBaseByDistance = getUsersByDistance(allUsersList, httpClient, apiBaseUrl, TechTestApiGetUsersPath, searchBaseLongitude, searchBaseLatitude, searchDistance);

             if (allUsersList.Count() > 0)
             {
                  allUsersList.Distinct().ToList();
             }

            if (responseBaseByCity.Result.ResponseCode != null && responseBaseByDistance.Result.ResponseCode != null)
            {
               
                if(responseBaseByCity.Result.ResponseCode != "200")
                {

                    responseBaseByDistance.Result.ResponseCode = responseBaseByCity.Result.ResponseCode;
                }
            }

            responseBaseByDistance.Result.UserList = allUsersList;
            
            
            return responseBaseByDistance;
        }
        #endregion
        private Task<ResponseBase> getUsersFromLondon(string code,List<UserBase> allUsersList, HttpClient httpClient, string apiBaseUrl, string apiGetUSersByCityPath, string city)
            {
             Task < ResponseBase > responseBase = _dataLayer.GetUsersByCity(httpClient, apiBaseUrl, apiGetUSersByCityPath, city);

            try
            {
                List<UserBase> usersList = responseBase.Result.UserList.ToList();
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
            catch (ArgumentNullException e)
            {

                responseBase.Result.ResponseCode = "400";
            }
            return responseBase;
        }
    
        private Task<ResponseBase> getUsersByDistance(List<UserBase> allUsersList, HttpClient httpClient, string apiBaseUrl, string TechTestApiGetUsersPath, string searchBaseLongitude, string searchBaseLatitude, string searchDistance)
        {
            Task<ResponseBase> responseBase = _dataLayer.GetAllUsers(httpClient, apiBaseUrl, TechTestApiGetUsersPath);
            List<UserBase> allOtherUsers = responseBase.Result.UserList.ToList();
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
            return responseBase;
        }
}
}
