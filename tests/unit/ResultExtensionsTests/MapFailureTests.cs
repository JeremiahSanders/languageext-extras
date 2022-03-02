using LanguageExt;
using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class MapFailureTests
{
  [Fact]
  public void GivenSuccess_ReturnsCurrentValue()
  {
    var success = new Result<int>(42);

    var actual = success.MapFailure(ex => new InvalidOperationException("failed", ex));

    Assert.Equal(success, actual);
  }

  [Fact]
  public void GivenFailure_ExecutesFunc()
  {
    var initialException = new Exception("Arranged failure");
    var failure = new Result<int>(initialException);

    var actualException = failure.MapFailure(ex => new InvalidOperationException("wrapped", ex))
      .Match(_ => throw new Exception("expected failure"), Prelude.identity);

    Assert.Equal(initialException, actualException.InnerException);
  }
}
