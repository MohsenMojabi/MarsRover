using MarsRover.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarsRover.WebApi.Test
{
    public class MovementsControllerIntegrationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _fixture;
        public MovementsControllerIntegrationTest(
        WebApplicationFactory<Startup> fixture)
        {
            _fixture = fixture;
        }

        /// <summary>
        /// Testing for incorrect initial position for example
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task MovementsApiPost_ShouldReturnBadRequest404_WhenInputsAreNotCorrect()
        {

            HttpClient client = _fixture.CreateClient();
            var data = new InitialInputs
            {
                Edges = "5 5",
                Obstacles = "(8 9)",
                InitialPosition = "1 2 G",
                Commands = "ff"
            };
            var myContent = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(myContent, Encoding.UTF8, "application/json");


            var response = await client.PostAsync("/Movements", stringContent);


            response.StatusCode.Equals(404);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("initialPosition (Parameter 'Insufficient arguments provided. Syntax is x<whitespace>y<whitespace>direction and only possitive numbers like '3 2 N' or '4 6 W'')", content);
        }

        [Fact]
        public async Task MovementsApiPost_ShouldReturnCorrectResponse_WhenInputsAreCorrect()
        {

            HttpClient client = _fixture.CreateClient();
            var data = new InitialInputs
            {
                Edges = "5 5",
                Obstacles = "(8 9)",
                InitialPosition = "1 2 N",
                Commands = "ff"
            };
            var myContent = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(myContent, Encoding.UTF8, "application/json");


            var response = await client.PostAsync("/Movements", stringContent);


            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CommandsResult>(content);
            Assert.Equal(1, result.X);
            Assert.Equal(4, result.Y);
            Assert.Equal("Rover touched the destination successfully", result.Message);
        }
    }
}
