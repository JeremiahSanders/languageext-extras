using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.OptionExtensions;

public class TapNoneTests
{
  [Fact]
  public void GivenSome_DoesNotExecuteIfNone()
  {
    var wasExecuted = false;

    void IfNone()
    {
      wasExecuted = true;
    }

    OptionExtensionsData.IntSome.TapNone(IfNone);

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

    OptionExtensionsData.IntNone.TapNone(IfNone);

    Assert.True(wasExecuted);
  }
}
