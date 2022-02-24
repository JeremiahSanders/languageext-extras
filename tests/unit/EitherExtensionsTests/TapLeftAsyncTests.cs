using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.EitherExtensionsTests;

public class TapLeftAsyncTests
{
  private const int RightValue = 42;
  private Either<Exception, int> RightResult { get; } = Prelude.Right(RightValue);
  private static Exception LeftValue { get; } = new ArgumentOutOfRangeException();
  private Either<Exception, int> LeftResult { get; } = Prelude.Left(LeftValue);

  [Fact]
  public async Task GivenSuccess_DoesNotExecuteSideEffect()
  {
    Exception? onLeftValue = null;

    await RightResult.TapLeftAsync(async left =>
    {
      await Task.Delay(1);
      onLeftValue = left;
    });

    Assert.Null(onLeftValue);
  }

  [Fact]
  public async Task GivenFailure_ExecutesSideEffect()
  {
    Exception? onLeftValue = null;

    await LeftResult.TapLeftAsync(async left =>
    {
      await Task.Delay(1);
      onLeftValue = left;
    });

    Assert.Equal(LeftValue, onLeftValue);
  }
}
