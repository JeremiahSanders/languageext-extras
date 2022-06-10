using LanguageExt;
using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.TryAsyncMonadExtensionsTests;

public class TapAsyncTests
{
  private const int SuccessfulValue = 42;
  private TryAsync<int> SuccessfulResult { get; } = Prelude.TryAsync(() => SuccessfulValue.AsTask());
  private static Exception FailureValue { get; } = new ArgumentOutOfRangeException();
  private TryAsync<int> FailureResult { get; } = Prelude.TryAsync(() => Prelude.raise<int>(FailureValue).AsTask());

  [Fact]
  public async Task GivenSuccess_ExecutesSuccessSideEffect()
  {
    int? onSuccessValue = null;
    Exception? onFailureValue = null;

    await SuccessfulResult.Tap(async value =>
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

    await FailureResult.Tap(async value =>
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
