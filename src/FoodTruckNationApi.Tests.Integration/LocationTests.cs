using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FoodTruckNationApi.Locations;
using Xunit;

namespace FoodTruckNationApi.Tests.Integration
{
    public class LocationTests
    {


        [Fact]
        [Trait("Category", "Integration")]
        public async Task ShouldReturnAllLocations()
        {
            // Arrange
            var testServer = TestUtil.CreateTestServer();
            var testClient = testServer.CreateClient();

            // Act
            var response = await testClient.GetAsync("/api/Locations");

            // Assert
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            var models = await response.Content.ReadAsAsync<List<LocationModel>>();
            models.Count.Should().Be(4);
        }




        [Fact]
        [Trait("Category", "Integration")]
        public async Task ShouldCreateNewLocationObject()
        {
            // Arrange
            var testServer = TestUtil.CreateTestServer();
            var testClient = testServer.CreateClient();
            var newLocation = new CreateLocationModel() { Name = "Loyola University", StreetAddress = "6430 Kenmore Ave", City = "Chicago", State = "IL", ZipCode = "60626" };

            // Act
            var response = await testClient.PostAsJsonAsync<CreateLocationModel>("/api/Locations", newLocation);

            // Assert
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
            var model = await response.Content.ReadAsAsync<LocationModel>();
            model.LocationName.Should().Be("Loyola University");


            // Confirm
            var confirmResponse = await testClient.GetAsync("/api/Locations");
            confirmResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
            var confirmModels = await confirmResponse.Content.ReadAsAsync<List<LocationModel>>();
            confirmModels.Count.Should().Be(5);
        }



    }
}
