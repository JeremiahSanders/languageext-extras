using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.EitherExtensionsTests;

public class TapRightAsyncTests
{
  private const int RightValue = 42;
  private Either<Exception, int> RightResult { get; } = Prelude.Right(RightValue);
  private static Exception LeftValue { get; } = new ArgumentOutOfRangeException();
  private Either<Exception, int> LeftResult { get; } = Prelude.Left(LeftValue);

  [Fact]
  public async Task GivenSuccess_ExecutesSideEffect()
  {
    int? onRightValue = null;

    await RightResult.TapRightAsync(async right =>
    {
      await Task.Delay(1);
      onRightValue = right;
    });

    Assert.Equal(RightValue, onRightValue);
  }

  [Fact]
  public async Task GivenLeft_DoesNotExecuteSideEffect()
  {
    int? onRightValue = null;

    await LeftResult.TapRightAsync(async right =>
    {
      await Task.Delay(1);
      onRightValue = right;
    });

    Assert.Null(onRightValue);
  }
}
