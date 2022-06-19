using JuniorDeveloperProject.Models;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
namespace JuniorDeveloperProject.Data
{
    public class CustomerDataConnector : ICustomerDataConnector
    {
        /// <summary>
        /// Method to GET all users registered by CITY. 
        /// </summary>
        /// <param name="city"></param>
        /// <returns>IEnumerable<UserBase></returns>
        public async Task<ResponseBase> GetUsersByCity(HttpClient httpClient, string apiBaseUrl, string apiGetUSersByCityPath, string city = "London")
        {
            ResponseBase responseBase = new ResponseBase();

            try
            {
                Uri callUrl = new Uri(String.Concat(apiBaseUrl + apiGetUSersByCityPath.Replace(@"{city}", city)));

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, callUrl)
                {
                    Headers =
                    {
                        { HeaderNames.Accept, "application/vnd.github.v3+json" },
                        { HeaderNames.UserAgent, "HttpRequestsSample" }
                    }
                };

                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var json = await httpResponseMessage.Content.ReadAsStringAsync();
                    List<UserBase> response = JsonConvert.DeserializeObject<List<UserBase>>(json);

                    responseBase.UserList = response;
                    responseBase.ResponseCode = "200";
                    
                }
                else
                {
                  
                    responseBase.ResponseCode = "900"; //httpResponseMessage.StatusCode.ToString();
                    responseBase.ResponseDescription = "Error Accesing API";
                }

                return responseBase;
            }
            catch (Exception)
            {
                responseBase.ResponseCode = "500";
                responseBase.ResponseDescription = "Server Downtime issue.";
                return responseBase;
                throw;
            }   
        }

        /// <summary>
        /// Method to GET all users registered by CITY. 
        /// </summary>
        /// <param name="city"></param>
        /// <returns>IEnumerable<UserBase></returns>
        public async Task<ResponseBase> GetAllUsers(HttpClient httpClient, string apiBaseUrl, string TechTestApiGetUsersPath)
        {
            ResponseBase responseBase = new ResponseBase();
            try
            {
                Uri callUrl = new Uri(String.Concat(apiBaseUrl + TechTestApiGetUsersPath));

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, callUrl)
                {
                    Headers =
                    {
                        { HeaderNames.Accept, "application/vnd.github.v3+json" },
                        { HeaderNames.UserAgent, "HttpRequestsSample" }
                    }
                };

                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var json = await httpResponseMessage.Content.ReadAsStringAsync();
                    List<UserBase> response = JsonConvert.DeserializeObject<List<UserBase>>(json);
                    responseBase.UserList = response;
                    responseBase.ResponseCode = "200";
                }
                else
                {
                    responseBase.ResponseCode = httpResponseMessage.StatusCode.ToString();
                    responseBase.ResponseDescription = "Error Accesing API";
                }
                return responseBase;

            }
            catch (Exception)
            {
                responseBase.ResponseCode = "500";
                responseBase.ResponseDescription = "Server Downtime issue.";
                return responseBase;
                
            }
        }
    /// <summary>
    /// Returns distance between two sets of coordinates. 
    /// Based on ASP .NET System.Device.Location implementation - https://docs.microsoft.com/en-us/dotnet/api/system.device.location.geocoordinate.-ctor?redirectedfrom=MSDN&view=netframework-4.8&viewFallbackFrom=netcore-3.1#System_Device_Location_GeoCoordinate__ctor 
    /// Earth radius (6376500.0) might need review, error margin acceptable with current number.  
    /// </summary>
    /// <param name="longitude"></param>
    /// <param name="latitude"></param>
    /// <param name="otherLongitude"></param>
    /// <param name="otherLatitude"></param>
    /// <returns>double distance (in Miles)</returns>
    /// 1 meter is 0.000621371 miles
    public double GetDistance(double longitude1, double latitude1, double longitude2, double latitude2)
        {
            double distance1 = latitude1 * (Math.PI / 180.0);
            double num1 = longitude1 * (Math.PI / 180.0);
            double distance2 = latitude2 * (Math.PI / 180.0);
            double num2 = longitude2 * (Math.PI / 180.0) - num1;
            double distance3 = Math.Pow(Math.Sin((distance2 - distance1) / 2.0), 2.0) + Math.Cos(distance1) * Math.Cos(distance2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            double distanceInMeters = 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(distance3), Math.Sqrt(1.0 - distance3)));
            
            return Math.Round(distanceInMeters / 1000 * 0.621371,2, MidpointRounding.AwayFromZero);
        }
    }
}
