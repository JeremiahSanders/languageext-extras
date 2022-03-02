using LanguageExt;
using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class MapFailureAsyncTests
{
  [Fact]
  public async Task GivenSuccess_ReturnsCurrentValue()
  {
    var success = new Result<int>(42);

    var actual = await success.MapFailureAsync(ex => new InvalidOperationException("failed", ex).AsTask());

    Assert.Equal(success, actual);
  }

  [Fact]
  public async Task GivenFailure_ExecutesFunc()
  {
    var initialException = new Exception("Arranged failure");
    var failure = new Result<int>(initialException);

    var actualException = (await failure.MapFailureAsync(async ex =>
      {
        await Task.Delay(1);
        return new InvalidOperationException("wrapped", ex);
      }))
      .Match(_ => throw new Exception("expected failure"), Prelude.identity);

    Assert.Equal(initialException, actualException.InnerException);
  }
}
