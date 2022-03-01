using System.Net;
using System.Text;
using System.Text.Json;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.HttpResponseMessageExtensionsTests;

public class TryDeserializeContentAsJsonAsyncTests
{
  private const string expectedValue = "whoop!";
  private const string SourceText = "{ \"value\": \"" + expectedValue + "\" }";

  public TryDeserializeContentAsJsonAsyncTests()
  {
    JsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
  }

  private JsonSerializerOptions JsonSerializerOptions { get; }

  [Fact]
  public async Task GivenStringResponse_ReturnsString()
  {
    using HttpContent content = new StringContent(SourceText, Encoding.UTF8);
    using var message = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };
    var expected = new TestType { Value = expectedValue };

    var actual = (await message.TryDeserializeContentAsJsonAsync<TestType>(JsonSerializerOptions)).IfFailThrow();

    Assert.Equal(expected, actual);
  }

  [Fact]
  public async Task GivenInfrastructureFailure_ReturnsException()
  {
    using HttpContent content = new StringContent(SourceText, Encoding.UTF8);
    using var message = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };
    CancellationTokenSource source = new();
    source.Cancel(); // Use a canceled token to force a failure.

    var actual = await message.TryDeserializeContentAsJsonAsync<TestType>(JsonSerializerOptions, source.Token);

    Assert.True(actual.IsFaulted);
  }

  private record TestType
  {
    public string Value { get; init; } = string.Empty;
  }
}
