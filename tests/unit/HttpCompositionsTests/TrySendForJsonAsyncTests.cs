using System.Net;

using Jds.LanguageExt.Extras.Tests.Unit.HttpClientExtensionsTests;

using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.HttpCompositionsTests;

public class TrySendForJsonAsyncTests
{
  [Fact]
  public async Task ExecutesSend()
  {
    const string Url = "https://not-real/thing";
    var arrangedObject = new ExampleResponseObject {Value = 42};
    var arrangedSendUri = new Uri(Url, UriKind.Absolute);
    var message = new HttpRequestMessage(HttpMethod.Head, arrangedSendUri);
    using var client = SendClient(arrangedSendUri, arrangedObject);

    var actual = (await client.TrySendForJsonAsync<ExampleResponseObject>(message)
        .Map(result => result.Body.IfFail(Prelude.raise<ExampleResponseObject>))
        .Try())
      .IfFailThrow();

    Assert.Equal(arrangedObject, actual);
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
