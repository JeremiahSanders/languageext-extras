using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.TryMonadExtensionsTests;

public class TapFailureTests
{
  private const int SuccessfulValue = 42;
  private Try<int> SuccessfulResult { get; } = Prelude.Try(() => SuccessfulValue);
  private static Exception FailureValue { get; } = new ArgumentOutOfRangeException();
  private Try<int> FailureResult { get; } = Prelude.Try(() => Prelude.raise<int>(FailureValue));

  [Fact]
  public void GivenSuccess_DoesNotExecuteSideEffect()
  {
    Exception? onFailureValue = null;

    _ = SuccessfulResult.TapFailure(exception => onFailureValue = exception).Try();

    Assert.Null(onFailureValue);
  }

  [Fact]
  public void GivenFailure_ExecutesSideEffect()
  {
    Exception? onFailureValue = null;

    _ = FailureResult.TapFailure(exception => onFailureValue = exception).Try();

    Assert.Equal(FailureValue, onFailureValue);
  }
}
