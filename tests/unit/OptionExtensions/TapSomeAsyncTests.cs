using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.OptionExtensions;

public class TapSomeAsyncTests
{
  [Fact]
  public async Task GivenSome_ExecutesIfSome()
  {
    var wasExecuted = false;

    async Task IfSome(int _)
    {
      await Task.Delay(1);
      wasExecuted = true;
    }

    await OptionExtensionsData.IntSome.TapSomeAsync(IfSome);

    Assert.True(wasExecuted);
  }

  [Fact]
  public async Task GivenNone_DoesNotExecuteIfSome()
  {
    var wasExecuted = false;

    async Task IfSome(int _)
    {
      await Task.Delay(1);
      wasExecuted = true;
    }

    await OptionExtensionsData.IntNone.TapSomeAsync(IfSome);

    Assert.False(wasExecuted);
  }
}
