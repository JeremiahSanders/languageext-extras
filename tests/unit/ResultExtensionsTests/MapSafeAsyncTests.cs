using System.Diagnostics.CodeAnalysis;

using LanguageExt;
using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class MapSafeAsyncTests
{
  [Fact]
  public async Task GivenFailure_DoesNotCallFunc()
  {
    var exception = new InvalidOperationException();
    var failure = new Result<int>(exception);
    var expected = new Result<string>(exception);

    var actual = await failure.MapSafeAsync(AlwaysFails);

    Assert.Equal(expected, actual);
  }

  [Fact]
  public async Task GivenSuccess_CallsFunc()
  {
    var success = new Result<int>(42);
    var expected = new Result<string>("42");

    var actual = await success.MapSafeAsync(ConvertToStringIfEven);

    Assert.Equal(expected, actual);
  }

  [Fact]
  public async Task GivenThrownException_ReturnsException()
  {
    var success = new Result<int>(42);
    var actual = (await success.MapSafeAsync(AlwaysFails))
      .Match(_ => throw new Exception("Should have failed"), Prelude.identity);

    Assert.IsType<ArithmeticException>(actual);
  }

  [ExcludeFromCodeCoverage]
  private static async Task<string> AlwaysFails(int value)
  {
    await Task.Delay(1);
    throw new ArithmeticException("Shouldn't get here");
  }

  private static async Task<string> ConvertToStringIfEven(int value)
  {
    await Task.Delay(1);
    return value % 2 == 0 ? value.ToString() : throw new ArgumentException();
  }
}
