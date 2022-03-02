using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.EitherExtensionsTests;

public class MapLeftAsyncTests
{
  [Fact]
  public async Task GivenRight_DoesNotExecuteFunc()
  {
    var rightValue = 42;
    var either = Prelude.Right<string, int>(rightValue);
    var expected = Prelude.Right<bool, int>(rightValue);

    var actual = await either.MapLeftAsync<string, int, bool>(_ => throw new Exception());

    Assert.Equal(expected, actual);
  }

  [Fact]
  public async Task GivenLeft_ExecutesFunc()
  {
    var expectedNewLeft = true;
    var either = Prelude.Left<string, int>("bad");
    var expected = Prelude.Left<bool, int>(expectedNewLeft);

    var actual = await either.MapLeftAsync(_ => expectedNewLeft.AsTask());

    Assert.Equal(expected, actual);
  }
}
