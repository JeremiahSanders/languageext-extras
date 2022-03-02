using LanguageExt;
using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class BindFailureAsyncTests
{
  [Fact]
  public async Task GivenFailure_CallsFunc()
  {
    var arrangedSuccess = new Result<int>(42);
    var exception = new InvalidOperationException();
    var failure = new Result<int>(exception);

    var actual = await failure.BindFailureAsync(priorFailure => arrangedSuccess.AsTask());

    Assert.Equal(arrangedSuccess, actual);
  }

  [Fact]
  public async Task GivenSuccess_DoesNotCallFunc()
  {
    var success = new Result<int>(42);

    var actual = await success.BindFailureAsync(unknown => throw new InvalidOperationException("Shouldn't get here"));

    Assert.Equal(success, actual);
  }
}
