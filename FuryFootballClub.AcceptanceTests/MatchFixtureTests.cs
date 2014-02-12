using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FuryFootballClub.AcceptanceTests
{
    public class PostFixtureData
    {
        public string Competition { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
    }

    public class FixtureData
    {
        public Guid Id { get; set; }
        public string Competition { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
    }

    [TestFixture]
    public class MatchFixtureTests
    {
        //private static readonly string URI = "http://localhost:7830/api/matchfixture";
        private static readonly string URI = "http://localhost.fiddler:7830/api/matchfixture";
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(URI);

            // Add an Accept header for JSON format.
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        [TearDown]
        public void CleanUp()
        {
            _client.Dispose();
        }

        private Guid create(PostFixtureData targetData)
        {
            var json = JsonConvert.SerializeObject(targetData);

            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

            var response = _client.PostAsync(URI, content).Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var responseFixture = JsonConvert.DeserializeObject<FixtureData>(response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(targetData.Competition, responseFixture.Competition);
            Assert.AreEqual(targetData.HomeTeam, responseFixture.HomeTeam);
            Assert.AreEqual(targetData.AwayTeam, responseFixture.AwayTeam);

            return responseFixture.Id;
        }

        private void getFixture(FixtureData target)
        {
            var stringGet = _client.GetStringAsync(URI + "/" + target.Id).Result;

            var responseFixture = JsonConvert.DeserializeObject<FixtureData>(stringGet);

            var targetJson = JsonConvert.SerializeObject(target);
            var responseJson = JsonConvert.SerializeObject(responseFixture);
            Assert.AreEqual(targetJson, responseJson);
        }

        private void updateCompetition(FixtureData target, string targetCompetition)
        {
            var originalCompetition = target.Competition;
            target.Competition = targetCompetition;

            var json = JsonConvert.SerializeObject(target);

            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

            var response = _client.PutAsync(URI + "/" + target.Id, content).Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var responseFixture = JsonConvert.DeserializeObject<FixtureData>(response.Content.ReadAsStringAsync().Result);

            var targetJson = JsonConvert.SerializeObject(target);
            var responseJson = JsonConvert.SerializeObject(responseFixture);
            Assert.AreEqual(targetJson, responseJson);
        }

        private void deleteFixture(FixtureData target)
        {
            var response = _client.DeleteAsync(URI + "/" + target.Id).Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var stringGet = _client.GetAsync(URI + "/" + target.Id).Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public void testGetFixture()
        {
            // TODO: Inject fixture directly into the database instead of violating principles by doing a post to add it?

            // Add the fixture first
            var fixture = new PostFixtureData()
            {
                Competition = "Baby Seals",
                HomeTeam = "Fury FC",
                AwayTeam = "Baby Seals"
            };

            var guid = create(fixture);

            var fullFixture = new FixtureData() {
                Id = guid,
                Competition = fixture.Competition,
                HomeTeam = fixture.HomeTeam,
                AwayTeam = fixture.AwayTeam
            };

            getFixture(fullFixture);

            updateCompetition(fullFixture, "TeamAwesome");

            deleteFixture(fullFixture);

        }
    }
}
