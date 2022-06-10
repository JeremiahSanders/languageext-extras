using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.TryMonadExtensionsTests;

public class TapTests
{
  private const int SuccessfulValue = 42;
  private Try<int> SuccessfulResult { get; } = Prelude.Try(() => SuccessfulValue);
  private static Exception FailureValue { get; } = new ArgumentOutOfRangeException();
  private Try<int> FailureResult { get; } = Prelude.Try(() => Prelude.raise<int>(FailureValue));

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
