using JuniorDeveloperProject.Models;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
namespace JuniorDeveloperProject.Data
{
    internal class CustomerDataConnector : ICustomerDataConnector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="apiBaseUrl"></param>
        /// <param name="TechTestApiGetUsersPath"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserBase>> GetAllUsers(HttpClient httpClient, string apiBaseUrl, string TechTestApiGetUsersPath)
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
                return response;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="longitude1"></param>
        /// <param name="latitude1"></param>
        /// <param name="longitude2"></param>
        /// <param name="latitude2"></param>
        /// <returns></returns>
        public double GetDistance(double longitude1, double latitude1, double longitude2, double latitude2)
        {
            double distance1 = latitude1 * (Math.PI / 180.0);
            double num1 = longitude1 * (Math.PI / 180.0);
            double distance2 = latitude2 * (Math.PI / 180.0);
            double num2 = longitude2 * (Math.PI / 180.0) - num1;
            double distance3 = Math.Pow(Math.Sin((distance2 - distance1) / 2.0), 2.0) + Math.Cos(distance1) * Math.Cos(distance2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(distance3), Math.Sqrt(1.0 - distance3)));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="apiBaseUrl"></param>
        /// <param name="apiGetUSersByCityPath"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserBase>> GetUsersByCity(HttpClient httpClient, string apiBaseUrl, string apiGetUSersByCityPath, string city = "London")
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
                return response;
            }
            else
            {
                return null;
            }
        }
    }
}
