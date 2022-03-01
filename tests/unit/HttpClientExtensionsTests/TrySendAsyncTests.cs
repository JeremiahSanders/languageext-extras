using System.Net;
using System.Text.Json;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.HttpClientExtensionsTests;

public class TrySendAsyncTests
{
  [Fact]
  public async Task ExecutesSend()
  {
    const string Url = "https://not-real/thing";
    var arrangedObject = new { value = 42 };
    var expectedJson = JsonSerializer.Serialize(arrangedObject);
    var arrangedSendUri = new Uri(Url, UriKind.Absolute);
    var message = new HttpRequestMessage(HttpMethod.Head, arrangedSendUri);
    using var client = SendClient(arrangedSendUri, arrangedObject);

    var actual =
      await (await client.TrySendAsync(message)).BindAsync(response => response.TryReadContentAsStringAsync());

    Assert.Equal(expectedJson, actual);
  }

  private static HttpClient SendClient<T>(Uri sendRoute, T content)
  {
    return ArrangedHttpMessageHandlerBuilder.CreateInstance()
      .WithArrangedHandler(builder =>
        builder
          .AcceptRoute(HttpMethod.Head, sendRoute)
          .RespondContentJson(HttpStatusCode.OK, content)
      )
      .CreateHttpClient();
  }
}
