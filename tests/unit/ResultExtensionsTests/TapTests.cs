using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class TapTests
{
  private const int SuccessfulValue = 42;
  private Result<int> SuccessfulResult { get; } = new(SuccessfulValue);
  private static Exception FailureValue { get; } = new ArgumentOutOfRangeException();
  private Result<int> FailureResult { get; } = new(FailureValue);

  [Fact]
  public void GivenSuccess_ExecutesSuccessSideEffect()
  {
    int? onSuccessValue = null;
    Exception? onFailureValue = null;

    SuccessfulResult.Tap(value => onSuccessValue = value, exception => onFailureValue = exception);

    Assert.Equal(SuccessfulValue, onSuccessValue);
    Assert.Null(onFailureValue);
  }

  [Fact]
  public void GivenFailure_ExecutesFailureSideEffect()
  {
    int? onSuccessValue = null;
    Exception? onFailureValue = null;

    FailureResult.Tap(value => onSuccessValue = value, exception => onFailureValue = exception);

    Assert.Null(onSuccessValue);
    Assert.Equal(FailureValue, onFailureValue);
  }
}
