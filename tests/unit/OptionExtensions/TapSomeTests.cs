using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.OptionExtensions;

public class TapSomeTests
{
  [Fact]
  public void GivenSome_ExecutesIfSome()
  {
    var wasExecuted = false;

    void IfSome(int _)
    {
      wasExecuted = true;
    }

    OptionExtensionsData.IntSome.TapSome(IfSome);

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

    OptionExtensionsData.IntNone.TapSome(IfSome);

    Assert.False(wasExecuted);
  }
}
