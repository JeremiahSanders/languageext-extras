using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.TryMonadExtensionsTests;

public class TapSuccessTests
{
  private const int SuccessfulValue = 42;
  private Try<int> SuccessfulResult { get; } = Prelude.Try(() => SuccessfulValue);
  private static Exception FailureValue { get; } = new ArgumentOutOfRangeException();
  private Try<int> FailureResult { get; } = Prelude.Try(() => Prelude.raise<int>(FailureValue));

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
