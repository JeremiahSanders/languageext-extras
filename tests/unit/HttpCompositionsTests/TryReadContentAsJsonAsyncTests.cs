using System.Net;
using System.Text;
using System.Text.Json;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.HttpCompositionsTests;

public class TryReadContentAsJsonAsyncTests
{
  private const string expectedValue = "whoop!";
  private const string SourceText = "{ \"value\": \"" + expectedValue + "\" }";

  public TryReadContentAsJsonAsyncTests()
  {
    JsonSerializerOptions = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
  }

  private JsonSerializerOptions JsonSerializerOptions { get; }

  [Fact]
  public async Task GivenStringResponse_ReturnsString()
  {
    using HttpContent httpContent = new StringContent(SourceText, Encoding.UTF8);
    using var message = new HttpResponseMessage(HttpStatusCode.OK) {Content = httpContent};
    var expected = new TestType {Value = expectedValue};

    var (actualMessage, contentResult, bodyResult) =
      await message.TryReadContentAsJsonAsync<TestType>(JsonSerializerOptions);
    var actualContent = contentResult.IfFailThrow();
    var actualBody = bodyResult.IfFailThrow();

    Assert.Equal(message.StatusCode, actualMessage.StatusCode);
    Assert.Equal(SourceText, actualContent);
    Assert.Equal(expected, actualBody);
  }

  [Fact]
  public async Task GivenInfrastructureFailure_ReturnsException()
  {
    using HttpContent content = new StringContent(SourceText, Encoding.UTF8);
    using var message = new HttpResponseMessage(HttpStatusCode.OK) {Content = content};
    CancellationTokenSource source = new();
    source.Cancel(); // Use a canceled token to force a failure.


    var (actualMessage, contentResult, bodyResult) =
      await message.TryReadContentAsJsonAsync<TestType>(JsonSerializerOptions, source.Token);

    Assert.Equal(message.StatusCode, actualMessage.StatusCode);
    Assert.True(contentResult.IsFaulted);
    Assert.True(bodyResult.IsFaulted);
  }

  private record TestType
  {
    public string Value { get; init; } = string.Empty;
  }
}
