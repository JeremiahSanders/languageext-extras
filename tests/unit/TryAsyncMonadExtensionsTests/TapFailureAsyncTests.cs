using LanguageExt;
using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.TryAsyncMonadExtensionsTests;

public class TapFailureAsyncTests
{
  private const int SuccessfulValue = 42;
  private TryAsync<int> SuccessfulResult { get; } = Prelude.TryAsync(() => SuccessfulValue.AsTask());
  private static Exception FailureValue { get; } = new ArgumentOutOfRangeException();
  private TryAsync<int> FailureResult { get; } = Prelude.TryAsync(() => Prelude.raise<int>(FailureValue).AsTask());

  [Fact]
  public async Task GivenSuccess_DoesNotExecuteSideEffect()
  {
    Exception? onFailureValue = null;

    await SuccessfulResult.TapFailure(async exception =>
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

    await FailureResult.TapFailure(async exception =>
    {
      await Task.Delay(1);
      onFailureValue = exception;
    });

    Assert.Equal(FailureValue, onFailureValue);
  }
}
