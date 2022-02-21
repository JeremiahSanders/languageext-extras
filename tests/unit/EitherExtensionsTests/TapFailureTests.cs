using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.EitherExtensionsTests;

public class TapLeftTests
{
  private const int RightValue = 42;
  private Either<Exception, int> RightResult { get; } = Prelude.Right(RightValue);
  private static Exception LeftValue { get; } = new ArgumentOutOfRangeException();
  private Either<Exception, int> LeftResult { get; } = Prelude.Left(LeftValue);

  [Fact]
  public void GivenSuccess_DoesNotExecuteSideEffect()
  {
    Exception? onLeftValue = null;

    RightResult.TapLeft(left => onLeftValue = left);

    Assert.Null(onLeftValue);
  }

  [Fact]
  public void GivenLeft_ExecutesSideEffect()
  {
    Exception? onLeftValue = null;

    LeftResult.TapLeft(left => onLeftValue = left);

    Assert.Equal(LeftValue, onLeftValue);
  }
}
