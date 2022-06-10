using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.TryAsyncMonadExtensionsTests;

public class TapSuccessTests
{
  private const int SuccessfulValue = 42;
  private TryAsync<int> SuccessfulResult { get; } = Prelude.TryAsync(() => SuccessfulValue.AsTask());
  private static Exception FailureValue { get; } = new ArgumentOutOfRangeException();
  private TryAsync<int> FailureResult { get; } = Prelude.TryAsync(() => Prelude.raise<int>(FailureValue).AsTask());

  [Fact]
  public void GivenSuccess_ExecutesSideEffect()
  {
    int? onSuccessValue = null;

    _ = SuccessfulResult.TapSuccess(value => onSuccessValue = value).Try();

    Assert.Equal(SuccessfulValue, onSuccessValue);
  }

  [Fact]
  public void GivenFailure_DoesNotExecuteSideEffect()
  {
    int? onSuccessValue = null;

    _ = FailureResult.TapSuccess(value => onSuccessValue = value).Try();

    Assert.Null(onSuccessValue);
  }
}
