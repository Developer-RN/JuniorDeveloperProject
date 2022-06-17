using Newtonsoft.Json;

namespace JuniorDeveloperProject.Models
{
    /// <summary>
    /// Used documentation for JSON serializer - https://www.newtonsoft.com/json/help/html/jsonpropertyname.html
    /// </summary>
    public class UserBase
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}
