using FluentAssertions;
using FoodTruckNationApi.FoodTrucks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http.Formatting;
using FoodTruckNation.Data.EF;
using Microsoft.EntityFrameworkCore;
using FoodTruckNation.Core.Domain;

namespace FoodTruckNationApi.Tests.Integration
{
    public class FoodTrucksTests
    {


        [Fact]
        [Trait("Category", "Integration")]
        public async Task ShouldReturnAllFoodTrucks()
        {
            // Arrange
            var testServer = TestUtil.CreateTestServer();
            var testClient = testServer.CreateClient();

            // Act
            var response = await testClient.GetAsync("/api/FoodTrucks");

            // Assert
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            var models = await response.Content.ReadAsAsync<List<FoodTruckModel>>();
            models.Count.Should().Be(3);            
        }


        [Fact]
        [Trait("Category", "Integration")]
        public async Task ShouldReturnSingleFoodTruckWhenIdExists()
        {
            // Arrange
            var testServer = TestUtil.CreateTestServer();
            var testClient = testServer.CreateClient();

            // Act
            var response = await testClient.GetAsync("/api/FoodTrucks/1");

            // Assert
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            var model = await response.Content.ReadAsAsync<FoodTruckModel>();
            model.FoodTruckId.Should().Be(1);
            model.Name.Should().Be("Dogs of Chicago");
        }


        [Fact]
        [Trait("Category", "Integration")]
        public async Task ShouldReturn404NotFoundWhenFoodTruckIdDoesNotExist()
        {
            // Arrange
            var testServer = TestUtil.CreateTestServer();
            var testClient = testServer.CreateClient();

            // Act
            var response = await testClient.GetAsync("/api/FoodTrucks/4");

            // Assert
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);
        }



    }
}
