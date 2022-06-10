using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.TryAsyncMonadExtensionsTests;

public class TapSuccessAsyncTests
{
  private const int SuccessfulValue = 42;
  private TryAsync<int> SuccessfulResult { get; } = Prelude.TryAsync(() => SuccessfulValue.AsTask());
  private static Exception FailureValue { get; } = new ArgumentOutOfRangeException();
  private TryAsync<int> FailureResult { get; } = Prelude.TryAsync(() => Prelude.raise<int>(FailureValue).AsTask());

  [Fact]
  public async Task GivenSuccess_ExecutesSideEffect()
  {
    int? onSuccessValue = null;

    await SuccessfulResult.TapSuccess(async value =>
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

    await FailureResult.TapSuccess(async value =>
    {
      await Task.Delay(1);
      onSuccessValue = value;
    });

    Assert.Null(onSuccessValue);
  }
}
