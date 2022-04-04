using System.Diagnostics.CodeAnalysis;

using LanguageExt;
using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class MapSafeTests
{
  [Fact]
  public void GivenFailure_DoesNotCallFunc()
  {
    var exception = new InvalidOperationException();
    var failure = new Result<int>(exception);
    var expected = new Result<string>(exception);

    var actual = failure.MapSafe(AlwaysFails);

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void GivenSuccess_CallsFunc()
  {
    var success = new Result<int>(42);
    var expected = new Result<string>("42");

    var actual = success.MapSafe(ConvertToStringIfEven);

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void GivenThrownException_ReturnsException()
  {
    var success = new Result<int>(42);
    var actual = success.MapSafe(AlwaysFails)
      .Match(_ => throw new Exception("Should have failed"), Prelude.identity);

    Assert.IsType<ArithmeticException>(actual);
  }

  [ExcludeFromCodeCoverage]
  private static string AlwaysFails(int value)
  {
    throw new ArithmeticException("Shouldn't get here");
  }

  private static string ConvertToStringIfEven(int value)
  {
    return value % 2 == 0 ? value.ToString() : throw new ArgumentException();
  }
}
