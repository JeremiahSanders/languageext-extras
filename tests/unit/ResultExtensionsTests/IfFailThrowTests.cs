using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class IfFailThrowTests
{
  [Fact]
  public void ReturnsValueIfSuccess()
  {
    var value = 42;
    var result = new Result<int>(value);

    var actual = result.IfFailThrow();

    Assert.Equal(value, actual);
  }

  [Fact]
  public void ThrowsIfFail()
  {
    var exception = new OverflowException();
    var result = new Result<int>(exception);

    Assert.Throws<OverflowException>(() => result.IfFailThrow());
  }
}
