using System.Text.Json;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.StringExtensionsTests;

public class TryDeserializeJsonTests
{
  private const string IsJson = @"{ ""value"": 42 }";

  public TryDeserializeJsonTests()
  {
    JsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
  }

  private JsonSerializerOptions JsonSerializerOptions { get; }

  [Theory]
  [InlineData("notJson")]
  [InlineData("")]
  [InlineData("false")]
  [InlineData("15")]
  public void FailsToParseNonJsonWithJsonException(string nonJson)
  {
    var expectedExceptionType = typeof(JsonException);
    Type? thrownExceptionType = null;

    var result = nonJson.TryDeserializeJson<ExampleJson>(JsonSerializerOptions);
    result.IfFail(exception => thrownExceptionType = exception.GetType());

    Assert.Equal(expectedExceptionType, thrownExceptionType);
  }

  [Theory]
  [InlineData(IsJson, 42)]
  public void ParsesJson(string json, int expectedValue)
  {
    var expected = new ExampleJson { Value = expectedValue };

    var actual = json.TryDeserializeJson<ExampleJson>(JsonSerializerOptions).IfFailThrow();

    Assert.Equal(expected, actual);
  }

  private record ExampleJson
  {
    public int Value { get; init; }
  }
}
