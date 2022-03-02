using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.OptionExtensions;

public class TapTests
{
  [Fact]
  public void GivenSome_ExecutesIfSome()
  {
    var wasExecuted = false;

    void IfSome(int _)
    {
      wasExecuted = true;
    }

    OptionExtensionsData.IntSome.Tap(IfSome, OptionExtensionsData.NoOp);

    Assert.True(wasExecuted);
  }

  [Fact]
  public void GivenNone_DoesNotExecuteIfSome()
  {
    var wasExecuted = false;

    void IfSome(int _)
    {
      wasExecuted = true;
    }

    OptionExtensionsData.IntNone.Tap(IfSome, OptionExtensionsData.NoOp);

    Assert.False(wasExecuted);
  }

  [Fact]
  public void GivenSome_DoesNotExecuteIfNone()
  {
    var wasExecuted = false;

    void IfNone()
    {
      wasExecuted = true;
    }

    OptionExtensionsData.IntSome.Tap(OptionExtensionsData.NoOp, IfNone);

    Assert.False(wasExecuted);
  }

  [Fact]
  public void GivenNone_ExecutesIfNone()
  {
    var wasExecuted = false;

    void IfNone()
    {
      wasExecuted = true;
    }

    OptionExtensionsData.IntNone.Tap(OptionExtensionsData.NoOp, IfNone);

    Assert.True(wasExecuted);
  }
}
