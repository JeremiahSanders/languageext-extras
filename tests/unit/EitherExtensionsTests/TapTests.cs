using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.EitherExtensionsTests;

public class TapTests
{
  private const int RightValue = 42;
  private Either<Exception, int> RightResult { get; } = Prelude.Right(RightValue);
  private static Exception LeftValue { get; } = new ArgumentOutOfRangeException();
  private Either<Exception, int> LeftResult { get; } = Prelude.Left(LeftValue);

  [Fact]
  public void GivenSuccess_ExecutesSuccessSideEffect()
  {
    int? onRightValue = null;
    Exception? onLeftValue = null;

    RightResult.Tap(right => onRightValue = right, left => onLeftValue = left);

    Assert.Equal(RightValue, onRightValue);
    Assert.Null(onLeftValue);
  }

  [Fact]
  public void GivenLeft_ExecutesLeftSideEffect()
  {
    int? onRightValue = null;
    Exception? onLeftValue = null;

    LeftResult.Tap(right => onRightValue = right, left => onLeftValue = left);

    Assert.Null(onRightValue);
    Assert.Equal(LeftValue, onLeftValue);
  }
}
