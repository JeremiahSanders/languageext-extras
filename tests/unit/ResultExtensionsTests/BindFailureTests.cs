using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class BindFailureTests
{
  [Fact]
  public void GivenFailure_CallsFunc()
  {
    var arrangedSuccess = new Result<int>(42);
    var exception = new InvalidOperationException();
    var failure = new Result<int>(exception);

    var actual = failure.BindFailure(priorFailure => arrangedSuccess);

    Assert.Equal(arrangedSuccess, actual);
  }

  [Fact]
  public void GivenSuccess_DoesNotCallFunc()
  {
    var success = new Result<int>(42);

    var actual = success.BindFailure(unknown => throw new InvalidOperationException("Shouldn't get here"));

    Assert.Equal(success, actual);
  }
}
