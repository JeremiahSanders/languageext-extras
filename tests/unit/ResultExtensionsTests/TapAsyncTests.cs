using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class TapAsyncTests
{
  private const int SuccessfulValue = 42;
  private Result<int> SuccessfulResult { get; } = new(SuccessfulValue);
  private static Exception FailureValue { get; } = new ArgumentOutOfRangeException();
  private Result<int> FailureResult { get; } = new(FailureValue);

  [Fact]
  public async Task GivenSuccess_ExecutesSuccessSideEffect()
  {
    int? onSuccessValue = null;
    Exception? onFailureValue = null;

    await SuccessfulResult.TapAsync(async value =>
    {
      await Task.Delay(1);
      onSuccessValue = value;
    }, async exception =>
    {
      await Task.Delay(1);
      onFailureValue = exception;
    });

    Assert.Equal(SuccessfulValue, onSuccessValue);
    Assert.Null(onFailureValue);
  }

  [Fact]
  public async Task GivenFailure_ExecutesFailureSideEffect()
  {
    int? onSuccessValue = null;
    Exception? onFailureValue = null;

    await FailureResult.TapAsync(async value =>
    {
      await Task.Delay(1);
      onSuccessValue = value;
    }, async exception =>
    {
      await Task.Delay(1);
      onFailureValue = exception;
    });

    Assert.Null(onSuccessValue);
    Assert.Equal(FailureValue, onFailureValue);
  }
}
