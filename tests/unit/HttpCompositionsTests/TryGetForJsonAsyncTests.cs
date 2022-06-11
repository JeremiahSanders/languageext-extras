using System.Net;

using Jds.LanguageExt.Extras.Tests.Unit.HttpClientExtensionsTests;

using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.HttpCompositionsTests;

public class TryGetForJsonAsyncTests
{
  [Fact]
  public async Task ExecutesGet()
  {
    const string Url = "https://not-real/thing";
    var arrangedObject = new ExampleResponseObject {Value = 42};
    var arrangedGetUri = new Uri(Url, UriKind.Absolute);
    using var client = GetClient(arrangedGetUri, arrangedObject);

    var result = await client.TryGetForJsonAsync<ExampleResponseObject>(arrangedGetUri)
      .Map(result => result.Body.IfFail(Prelude.raise<ExampleResponseObject>))
      .Try();
    var actual = result.IfFailThrow();

    Assert.Equal(arrangedObject, actual);
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
