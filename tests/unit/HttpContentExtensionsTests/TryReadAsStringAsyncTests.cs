using System.Text;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.HttpContentExtensionsTests;

public class TryReadAsStringAsyncTests
{
  private const string SourceText = "hi!";

  [Fact]
  public async Task GivenStringResponse_ReturnsString()
  {
    using HttpContent content = new StringContent(SourceText, Encoding.UTF8);
    var expected = SourceText;

    var actual = (await content.TryReadAsStringAsync()).IfFailThrow();

    Assert.Equal(expected, actual);
  }

  [Fact]
  public async Task GivenInfrastructureFailure_ReturnsException()
  {
    using HttpContent content = new StringContent(SourceText, Encoding.UTF8);
    CancellationTokenSource source = new();
    source.Cancel(); // Use a canceled token to force a failure.

    var actual = await content.TryReadAsStringAsync(source.Token);

    Assert.True(actual.IsFaulted);
  }
}
