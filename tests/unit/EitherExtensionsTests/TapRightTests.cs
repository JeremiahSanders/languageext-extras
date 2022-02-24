using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.EitherExtensionsTests;

public class TapRightTests
{
  private const int RightValue = 42;
  private Either<Exception, int> RightResult { get; } = Prelude.Right(RightValue);
  private static Exception LeftValue { get; } = new ArgumentOutOfRangeException();
  private Either<Exception, int> LeftResult { get; } = Prelude.Left(LeftValue);

  [Fact]
  public void GivenSuccess_ExecutesSideEffect()
  {
    int? onRightValue = null;

    RightResult.TapRight(right => onRightValue = right);

    Assert.Equal(RightValue, onRightValue);
  }

  [Fact]
  public void GivenLeft_DoesNotExecuteSideEffect()
  {
    int? onRightValue = null;

    LeftResult.TapRight(right => onRightValue = right);

    Assert.Null(onRightValue);
  }
}
