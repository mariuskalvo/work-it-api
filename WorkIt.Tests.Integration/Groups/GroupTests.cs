using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using WebApi;
using Xunit;

namespace WorkIt.Tests.Integration.Groups
{


    class GroupTests
    {
        private readonly HttpClient _httpClient;
        private readonly TestServer _server;

        public GroupTests()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _httpClient = _server.CreateClient();
        }

    }
}
