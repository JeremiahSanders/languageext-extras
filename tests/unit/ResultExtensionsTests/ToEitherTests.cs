using LanguageExt;
using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class ToEitherTests
{
  [Fact]
  public void GivenFailure_ReturnsExpected()
  {
    var exception = new ArgumentException();
    var expected = Prelude.Left<Exception, int>(exception);
    var failure = new Result<int>(exception);

    var actual = failure.ToEither();

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void GivenSuccess_ReturnsExpected()
  {
    const int value = 10;
    var expected = Prelude.Right<Exception, int>(value);
    var success = new Result<int>(value);

    var actual = success.ToEither();

    Assert.Equal(expected, actual);
  }
}
