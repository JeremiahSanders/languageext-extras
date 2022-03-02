using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.OptionExtensions;

public class TapAsyncTests
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

    await OptionExtensionsData.IntSome.TapAsync(IfSome, OptionExtensionsData.NoOpAsync);

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

    await OptionExtensionsData.IntNone.TapAsync(IfSome, OptionExtensionsData.NoOpAsync);

    Assert.False(wasExecuted);
  }

  [Fact]
  public async Task GivenSome_DoesNotExecuteIfNone()
  {
    var wasExecuted = false;

    async Task IfNone()
    {
      await Task.Delay(1);
      wasExecuted = true;
    }

    await OptionExtensionsData.IntSome.TapAsync(OptionExtensionsData.NoOpAsync, IfNone);

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

    await OptionExtensionsData.IntNone.TapAsync(OptionExtensionsData.NoOpAsync, IfNone);

    Assert.True(wasExecuted);
  }
}
