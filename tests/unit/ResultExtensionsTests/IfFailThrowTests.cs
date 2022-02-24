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

    Assert.Throws<InvalidOperationException>(() => result.IfFailThrow());
  }

  [Fact]
  public void ContainsThrownExceptionAsInnerException()
  {
    var exception = new OverflowException();
    var result = new Result<int>(exception);

    Exception? innerException = null;
    try
    {
      _ = result.IfFailThrow();
    }
    catch (Exception ex)
    {
      innerException = ex.InnerException;
    }

    Assert.Equal(exception, innerException);
  }
}
