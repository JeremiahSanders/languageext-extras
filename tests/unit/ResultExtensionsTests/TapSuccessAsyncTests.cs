using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class TapSuccessAsyncTests
{
  private const int SuccessfulValue = 42;
  private Result<int> SuccessfulResult { get; } = new(SuccessfulValue);
  private static Exception FailureValue { get; } = new ArgumentOutOfRangeException();
  private Result<int> FailureResult { get; } = new(FailureValue);

  [Fact]
  public async Task GivenSuccess_ExecutesSideEffect()
  {
    int? onSuccessValue = null;

    await SuccessfulResult.TapSuccessAsync(async value =>
    {
      await Task.Delay(1);
      onSuccessValue = value;
    });

    Assert.Equal(SuccessfulValue, onSuccessValue);
  }

  [Fact]
  public async Task GivenFailure_DoesNotExecuteSideEffect()
  {
    int? onSuccessValue = null;

    await FailureResult.TapSuccessAsync(async value =>
    {
      await Task.Delay(1);
      onSuccessValue = value;
    });

    Assert.Null(onSuccessValue);
  }
}
