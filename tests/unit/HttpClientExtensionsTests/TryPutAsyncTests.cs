using System.Net;
using System.Text.Json;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.HttpClientExtensionsTests;

public class TryPutAsyncTests
{
  [Fact]
  public async Task ExecutesPut()
  {
    const string Url = "https://not-real/thing";
    var arrangedObject = new { value = 42 };
    var expectedRequest = new { id = "abc" };
    var expectedJson = JsonSerializer.Serialize(arrangedObject);
    var arrangedPutUri = new Uri(Url, UriKind.Absolute);
    using var client = GetClient(arrangedPutUri, expectedRequest, arrangedObject);

    var actual =
      await (await client.TryPutAsync(arrangedPutUri, new StringContent(JsonSerializer.Serialize(expectedRequest))))
        .BindAsync(response => response.TryReadContentAsStringAsync());

    Assert.Equal(expectedJson, actual);
  }

  private static HttpClient GetClient<TRequest, TResponse>(Uri putRoute, TRequest expectedBody,
    TResponse responseContent) where TRequest : notnull
  {
    return ArrangedHttpMessageHandlerBuilder.CreateInstance()
      .WithArrangedHandler(builder =>
        builder
          .WithAcceptRule(async message => message.Method == HttpMethod.Put && message.RequestUri == putRoute
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
