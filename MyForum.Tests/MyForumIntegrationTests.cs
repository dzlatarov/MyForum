using System;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;
using MyForum.Web;
using MyForum.Infrastructure;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyForum.Tests
{
    public class MyForumIntegrationTests
    {
        private readonly WebApplicationFactory<Startup> server;
        private readonly HttpClient client;
        private HttpResponseMessage requestUrl;

        public MyForumIntegrationTests()
        {
            this.server = new WebApplicationFactory<Startup>();
            this.client = this.server.CreateClient();
            this.requestUrl = client.GetAsync("/Home/Index").Result;
        }


        [Fact]
        public void VerifyIndexPageReturnsStatusOk()
        {
            var expected = true;
            var actual = requestUrl.IsSuccessStatusCode;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CheckIfTheTitleOfThePageDoesNotContainIndex()
        {
            var expected = "Index";
            var actualTitle = await requestUrl.Content.ReadAsStringAsync();

            Assert.DoesNotContain(expected, actualTitle);
        }

        [Fact]
        public async Task CheckIfTheTitleOfTheIndexPageIsCorrect()
        {
            var expected = GlobalConstants.ProjectName;
            var actual = await requestUrl.Content.ReadAsStringAsync();

            Assert.Contains(expected, actual);
        }

        [Fact]
        public async Task CheckIfIndexPageContainsTagStructure()
        {
            var actual = await requestUrl.Content.ReadAsStringAsync();

            var expected = new List<string>
            {
                "<header", "<nav", "</nav>", "</header>", "<main", "</main>", "<footer", "</footer>"
            };

            foreach (var tag in expected)
            {
                Assert.Contains(tag, actual);
            }
        }
    }
}
