using System;
using System.Net.Http;
using Doggo.Web3;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Doggo.Integration
{
    public class HttpClientFixture : IDisposable
  {
    public HttpClientFixture() => this.Client = new WebApplicationFactory<Startup>().CreateClient();

    public void Dispose() => this.Client.Dispose();
    public HttpClient Client { get; private set; }
  }

  [CollectionDefinition(nameof(HttpClientCollection))]
  public class HttpClientCollection : ICollectionFixture<HttpClientFixture>
  {
    // no code here
  }
}