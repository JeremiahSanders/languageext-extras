using System.Net;
using System.Text;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.HttpResponseMessageExtensionsTests;

public class TryReadContentAsStringAsyncTests
{
  private const string SourceText = "hi!";

  [Fact]
  public async Task GivenStringResponse_ReturnsString()
  {
    using HttpContent content = new StringContent(SourceText, Encoding.UTF8);
    using var message = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };
    var expected = SourceText;

    var actual = (await message.TryReadContentAsStringAsync()).IfFailThrow();

    Assert.Equal(expected, actual);
  }

  [Fact]
  public async Task GivenInfrastructureFailure_ReturnsException()
  {
    using HttpContent content = new StringContent(SourceText, Encoding.UTF8);
    using var message = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };
    CancellationTokenSource source = new();
    source.Cancel(); // Use a canceled token to force a failure.

    var actual = await message.TryReadContentAsStringAsync(source.Token).Try();

    Assert.True(actual.IsFaulted);
  }
}
