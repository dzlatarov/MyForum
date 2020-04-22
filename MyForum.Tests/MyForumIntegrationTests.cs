using System;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;
using MyForum.Web;

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
    }
}
