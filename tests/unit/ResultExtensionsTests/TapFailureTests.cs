using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class TapFailureTests
{
  private const int SuccessfulValue = 42;
  private Result<int> SuccessfulResult { get; } = new(SuccessfulValue);
  private static Exception FailureValue { get; } = new ArgumentOutOfRangeException();
  private Result<int> FailureResult { get; } = new(FailureValue);

  [Fact]
  public void GivenSuccess_DoesNotExecuteSideEffect()
  {
    Exception? onFailureValue = null;

    SuccessfulResult.TapFailure(exception => onFailureValue = exception);

    Assert.Null(onFailureValue);
  }

  [Fact]
  public void GivenFailure_ExecutesSideEffect()
  {
    Exception? onFailureValue = null;

    FailureResult.TapFailure(exception => onFailureValue = exception);

    Assert.Equal(FailureValue, onFailureValue);
  }
}
