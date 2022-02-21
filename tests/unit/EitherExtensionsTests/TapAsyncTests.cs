using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.EitherExtensionsTests;

public class TapAsyncTests
{
  private const int RightValue = 42;
  private Either<Exception, int> RightResult { get; } = Prelude.Right(RightValue);
  private static Exception LeftValue { get; } = new ArgumentOutOfRangeException();
  private Either<Exception, int> LeftResult { get; } = Prelude.Left(LeftValue);

  [Fact]
  public async Task GivenSuccess_ExecutesSuccessSideEffect()
  {
    int? onSuccessValue = null;
    Exception? onLeftValue = null;

    await RightResult.TapAsync(async right =>
    {
      await Task.Delay(1);
      onSuccessValue = right;
    }, async left =>
    {
      await Task.Delay(1);
      onLeftValue = left;
    });

    Assert.Equal(RightValue, onSuccessValue);
    Assert.Null(onLeftValue);
  }

  [Fact]
  public async Task GivenLeft_ExecutesLeftSideEffect()
  {
    int? onSuccessValue = null;
    Exception? onLeftValue = null;

    await LeftResult.TapAsync(async right =>
    {
      await Task.Delay(1);
      onSuccessValue = right;
    }, async left =>
    {
      await Task.Delay(1);
      onLeftValue = left;
    });

    Assert.Null(onSuccessValue);
    Assert.Equal(LeftValue, onLeftValue);
  }
}
