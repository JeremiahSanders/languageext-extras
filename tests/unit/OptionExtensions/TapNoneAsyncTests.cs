using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.OptionExtensions;

public class TapNoneAsyncTests
{
  [Fact]
  public async Task GivenSome_DoesNotExecuteIfNone()
  {
    var wasExecuted = false;

    async Task IfNone()
    {
      await Task.Delay(1);
      wasExecuted = true;
    }

    await OptionExtensionsData.IntSome.TapNoneAsync(IfNone);

    Assert.False(wasExecuted);
  }

  [Fact]
  public async Task GivenNone_ExecutesIfNone()
  {
    var wasExecuted = false;

    async Task IfNone()
    {
      await Task.Delay(1);
      wasExecuted = true;
    }

    await OptionExtensionsData.IntNone.TapNoneAsync(IfNone);

    Assert.True(wasExecuted);
  }
}
