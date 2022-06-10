using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.TryAsyncMonadExtensionsTests;

public class TapFailureTests
{
  private const int SuccessfulValue = 42;
  private TryAsync<int> SuccessfulResult { get; } = Prelude.TryAsync(() => SuccessfulValue.AsTask());
  private static Exception FailureValue { get; } = new ArgumentOutOfRangeException();
  private TryAsync<int> FailureResult { get; } = Prelude.TryAsync(() => Prelude.raise<int>(FailureValue).AsTask());

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
