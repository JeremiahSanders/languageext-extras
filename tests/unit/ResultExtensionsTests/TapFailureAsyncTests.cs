using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class TapFailureAsyncTests
{
  private const int SuccessfulValue = 42;
  private Result<int> SuccessfulResult { get; } = new(SuccessfulValue);
  private static Exception FailureValue { get; } = new ArgumentOutOfRangeException();
  private Result<int> FailureResult { get; } = new(FailureValue);

  [Fact]
  public async Task GivenSuccess_DoesNotExecuteSideEffect()
  {
    Exception? onFailureValue = null;

    await SuccessfulResult.TapFailureAsync(async exception =>
    {
      await Task.Delay(1);
      onFailureValue = exception;
    });

    Assert.Null(onFailureValue);
  }

  [Fact]
  public async Task GivenFailure_ExecutesSideEffect()
  {
    Exception? onFailureValue = null;

    await FailureResult.TapFailureAsync(async exception =>
    {
      await Task.Delay(1);
      onFailureValue = exception;
    });

    Assert.Equal(FailureValue, onFailureValue);
  }
}
