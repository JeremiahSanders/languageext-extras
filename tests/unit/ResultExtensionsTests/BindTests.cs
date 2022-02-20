using System.Diagnostics.CodeAnalysis;

using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class BindTests
{
  [Fact]
  public void GivenFailure_DoesNotCallBinder()
  {
    var exception = new InvalidOperationException();
    var failure = new Result<int>(exception);
    var expected = new Result<string>(exception);

    var actual = failure.Bind(AlwaysFails);

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void GivenSuccess_CallsBinder()
  {
    var success = new Result<int>(42);
    var expected = new Result<string>("42");

    var actual = success.Bind(ConvertToStringIfEven);

    Assert.Equal(expected, actual);
  }

  [ExcludeFromCodeCoverage]
  private static Result<string> AlwaysFails(int value)
  {
    throw new InvalidOperationException("Shouldn't get here");
  }

  private static Result<string> ConvertToStringIfEven(int value)
  {
    return value % 2 == 0 ? new Result<string>(value.ToString()) : new Result<string>(new Exception());
  }
}
