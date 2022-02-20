using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class TapSuccessTests
{
  private const int SuccessfulValue = 42;
  private Result<int> SuccessfulResult { get; } = new(SuccessfulValue);
  private static Exception FailureValue { get; } = new ArgumentOutOfRangeException();
  private Result<int> FailureResult { get; } = new(FailureValue);

  [Fact]
  public void GivenSuccess_ExecutesSideEffect()
  {
    int? onSuccessValue = null;

    SuccessfulResult.TapSuccess(value => onSuccessValue = value);

    Assert.Equal(SuccessfulValue, onSuccessValue);
  }

  [Fact]
  public void GivenFailure_DoesNotExecuteSideEffect()
  {
    int? onSuccessValue = null;

    FailureResult.TapSuccess(value => onSuccessValue = value);

    Assert.Null(onSuccessValue);
  }
}