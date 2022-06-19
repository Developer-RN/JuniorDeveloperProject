using JuniorDeveloperProject.Data;
using JuniorDeveloperProject.Models;

namespace JuniorDeveloperProjectTests
{
    public class CustomerDataConnectorTests

    {
        double LatitudeOfLondon = 51.5074;
        double LongitudeOfLondon = -0.1277;

        private CustomerDataConnector customerDataConnector= new CustomerDataConnector();
       
        [Test]
        public void DistanceThatIsMoreThan50MilesTest()
        {

            double distance = customerDataConnector.GetDistance(LongitudeOfLondon, LatitudeOfLondon, -117.7228641, 34.003135);
            Assert.AreEqual((decimal)(5431.23), (decimal)(distance));
        }

        [Test]
        public void DistanceThatIslessThan50MilesTest()
        {
            double distance = customerDataConnector.GetDistance(LongitudeOfLondon, LatitudeOfLondon, 0.8078532, 51.6710832);

            Assert.AreEqual((decimal)(41.76), (decimal)(distance));
        }

        //This is integration test
       [Test]
        public void GetAllUsersListNotEmptyTest()
        {
            var httpClient = new HttpClient();

        string apiBaseUrl = "https://dwp-techtest.herokuapp.com";
        string TechTestApiGetUsersPath = "/users";
          
        List<UserBase> usersList = customerDataConnector.GetAllUsers(httpClient, apiBaseUrl, TechTestApiGetUsersPath).Result.UserList.ToList();

        CollectionAssert.IsNotEmpty(usersList);
          
        }
    }
    }