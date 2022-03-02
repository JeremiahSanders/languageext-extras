using System.Diagnostics.CodeAnalysis;

using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.OptionExtensions;

[SuppressMessage("ReSharper", "SuggestVarOrType_BuiltInTypes")]
[SuppressMessage("ReSharper", "ConvertToConstant.Local")]
[SuppressMessage("ReSharper", "VariableCanBeNotNullable")]
[SuppressMessage("ReSharper", "RedundantSuppressNullableWarningExpression")]
public class ToNonNullableTests
{
  [Fact]
  public void GivenNullableReferenceType_Some_ReturnsSameValue()
  {
    var nullableStringOption = OptionExtensionsData.NullableStringSome;
    var expected = Option<string>.Some(OptionExtensionsData.StringValue);

    var actual = nullableStringOption.ToNonNullable();

    Assert.Equal(actual, expected);
  }

  [Fact]
  public void GivenNullableReferenceType_None_ReturnsSameValue()
  {
    var nullableStringOption = OptionExtensionsData.NullableStringNone;
    var expected = Option<string>.None;

    var actual = nullableStringOption.ToNonNullable();

    Assert.Equal(actual, expected);
  }

  [Fact]
  public void GivenNullableValueType_Some_ReturnsSameValue()
  {
    var nullableIntOption = OptionExtensionsData.NullableIntSome;
    var expected = Option<int>.Some(OptionExtensionsData.IntValue);

    var actual = nullableIntOption.ToNonNullable();

    Assert.Equal(actual, expected);
  }

  [Fact]
  public void GivenNullableValueType_None_ReturnsNone()
  {
    var nullableIntOption = OptionExtensionsData.NullableIntNone;
    var expected = Option<int>.None;

    var actual = nullableIntOption.ToNonNullable();

    Assert.Equal(actual, expected);
  }
}
