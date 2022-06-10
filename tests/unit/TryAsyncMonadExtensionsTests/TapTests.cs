using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.TryAsyncMonadExtensionsTests;

public class TapTests
{
  private const int SuccessfulValue = 42;
  private TryAsync<int> SuccessfulResult { get; } = Prelude.TryAsync(() => SuccessfulValue.AsTask());
  private static Exception FailureValue { get; } = new ArgumentOutOfRangeException();
  private TryAsync<int> FailureResult { get; } = Prelude.TryAsync(() => Prelude.raise<int>(FailureValue).AsTask());

  [Fact]
  public void GivenSuccess_ExecutesSuccessSideEffect()
  {
    int? onSuccessValue = null;
    Exception? onFailureValue = null;

    _ = SuccessfulResult.Tap(value => onSuccessValue = value, exception => onFailureValue = exception).Try();

    Assert.Equal(SuccessfulValue, onSuccessValue);
    Assert.Null(onFailureValue);
  }

  [Fact]
  public void GivenFailure_ExecutesFailureSideEffect()
  {
    int? onSuccessValue = null;
    Exception? onFailureValue = null;

    _ = FailureResult.Tap(value => onSuccessValue = value, exception => onFailureValue = exception).Try();

    Assert.Null(onSuccessValue);
    Assert.Equal(FailureValue, onFailureValue);
  }
}
