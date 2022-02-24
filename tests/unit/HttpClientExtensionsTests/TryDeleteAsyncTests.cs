using System.Net;

using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.HttpClientExtensionsTests;

public class TryDeleteAsyncTests
{
  [Fact]
  public async Task ExecutesDelete()
  {
    const string Url = "https://not-real/thing";
    var arrangedDeleteUri = new Uri(Url, UriKind.Absolute);
    using var client = GetClient(arrangedDeleteUri);
    var expected = new Result<HttpStatusCode>(HttpStatusCode.NoContent);

    var actual = (await client.TryDeleteAsync(arrangedDeleteUri)).Map(response => response.StatusCode);

    Assert.Equal(expected, actual);
  }

  private static HttpClient GetClient(Uri deleteRoute)
  {
    return ArrangedHttpMessageHandlerBuilder.CreateInstance()
      .WithArrangedHandler(builder =>
        builder
          .AcceptRoute(HttpMethod.Delete, deleteRoute)
          .RespondStatusCode(HttpStatusCode.NoContent)
      )
      .CreateHttpClient();
  }
}
