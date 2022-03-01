using System.Net;
using System.Text.Json;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.HttpClientExtensionsTests;

public class TryGetAsyncTests
{
  [Fact]
  public async Task ExecutesGet()
  {
    const string Url = "https://not-real/thing";
    var arrangedObject = new { value = 42 };
    var expectedJson = JsonSerializer.Serialize(arrangedObject);
    var arrangedGetUri = new Uri(Url, UriKind.Absolute);
    using var client = GetClient(arrangedGetUri, arrangedObject);

    var actual =
      await (await client.TryGetAsync(arrangedGetUri)).BindAsync(response => response.TryReadContentAsStringAsync());

    Assert.Equal(expectedJson, actual);
  }

  private static HttpClient GetClient<T>(Uri getRoute, T content)
  {
    return ArrangedHttpMessageHandlerBuilder.CreateInstance()
      .WithArrangedHandler(builder =>
        builder
          .AcceptRoute(HttpMethod.Get, getRoute)
          .RespondContentJson(HttpStatusCode.OK, content)
      )
      .CreateHttpClient();
  }
}
