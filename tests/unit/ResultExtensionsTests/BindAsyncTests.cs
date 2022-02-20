using System.Diagnostics.CodeAnalysis;

using LanguageExt;
using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class BindAsyncTests
{
  [Fact]
  public async Task GivenFailure_DoesNotCallBinder()
  {
    var exception = new InvalidOperationException();
    var failure = new Result<int>(exception);
    var expected = new Result<string>(exception);

    var actual = await failure.BindAsync(AlwaysFailsAsync);

    Assert.Equal(expected, actual);
  }

  [Fact]
  public async Task GivenSuccess_CallsBinder()
  {
    var success = new Result<int>(42);
    var expected = new Result<string>("42");

    var actual = await success.BindAsync(ConvertToStringIfEvenAsync);

    Assert.Equal(expected, actual);
  }

  [ExcludeFromCodeCoverage]
  private static Task<Result<string>> AlwaysFailsAsync(int value)
  {
    throw new InvalidOperationException("Shouldn't get here");
  }

  private static Task<Result<string>> ConvertToStringIfEvenAsync(int value)
  {
    return value % 2 == 0
      ? new Result<string>(value.ToString()).AsTask()
      : new Result<string>(new Exception()).AsTask();
  }
}
