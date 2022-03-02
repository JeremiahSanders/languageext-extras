using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.EitherExtensionsTests;

public class BindLeftAsyncTests
{
  [Fact]
  public async Task GivenRight_DoesNotExecuteFunc()
  {
    var rightValue = 42;
    var either = Prelude.Right<string, int>(rightValue);
    var expected = Prelude.Right<bool, int>(rightValue);

    var actual = await either.BindLeftAsync<string, int, bool>(_ => throw new Exception());

    Assert.Equal(expected, actual);
  }

  [Fact]
  public async Task GivenLeft_ExecutesFunc()
  {
    var plannedNewLeft = Prelude.Left<bool, int>(true);
    var either = Prelude.Left<string, int>("thing");

    var actual = await either.BindLeftAsync(_ => plannedNewLeft.AsTask());

    Assert.Equal(plannedNewLeft, actual);
  }
}
