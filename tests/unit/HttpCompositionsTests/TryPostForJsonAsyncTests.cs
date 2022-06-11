using System.Net;
using System.Text.Json;

using Jds.LanguageExt.Extras.Tests.Unit.HttpClientExtensionsTests;

using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.HttpCompositionsTests;

public class TryPostForJsonAsyncTests
{
  [Fact]
  public async Task ExecutesPost()
  {
    const string Url = "https://not-real/thing";
    var arrangedObject = new ExampleResponseObject {Value = 42};
    var expectedRequest = new {id = "abc"};
    var arrangedPostUri = new Uri(Url, UriKind.Absolute);
    using var client = GetClient(arrangedPostUri, expectedRequest, arrangedObject);

    var actual = (await client
        .TryPostForJsonAsync<ExampleResponseObject>(arrangedPostUri,
          new StringContent(JsonSerializer.Serialize(expectedRequest)))
        .Map(result => result.Body.IfFail(Prelude.raise<ExampleResponseObject>))
        .Try())
      .IfFailThrow();

    Assert.Equal(arrangedObject, actual);
  }

  private static HttpClient GetClient<TRequest, TResponse>(Uri postRoute, TRequest expectedBody,
    TResponse responseContent) where TRequest : notnull
  {
    return ArrangedHttpMessageHandlerBuilder.CreateInstance()
      .WithArrangedHandler(builder =>
        builder
          .WithAcceptRule(async message => message.Method == HttpMethod.Post && message.RequestUri == postRoute
                                                                             && await DoesBodyMatch(message.Content,
                                                                               expectedBody)
          )
          .RespondContentJson(HttpStatusCode.OK, responseContent)
      )
      .CreateHttpClient();
  }

  private static async Task<bool> DoesBodyMatch<TRequest>(HttpContent? messageContent, TRequest expectedBody)
    where TRequest : notnull
  {
    if (messageContent == null)
    {
      return false;
    }

    var body = await messageContent.ReadAsStringAsync(CancellationToken.None);

    var actual = body.TryDeserializeJson<TRequest>();

    return actual.Filter(request => expectedBody.Equals(request), _ => new Exception()).Map(_ => true).IfFail(false);
  }
}
