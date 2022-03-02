using LanguageExt;

namespace Jds.LanguageExt.Extras.Tests.Unit.OptionExtensions;

internal static class OptionExtensionsData
{
  public static int IntValue => 42;
  public static Option<int> IntSome { get; } = Option<int>.Some(IntValue);
  public static Option<int> IntNone => Option<int>.None;
  public static Option<int?> NullableIntSome { get; } = Option<int?>.Some(IntValue);
  public static Option<int?> NullableIntNone => Option<int?>.None;
  public static string StringValue => "abc";
  public static Option<string> StringSome { get; } = Option<string>.Some(StringValue);
  public static Option<string> StringNone => Option<string>.None;
  public static Option<string?> NullableStringSome { get; } = Option<string?>.Some(StringValue);
  public static Option<string?> NullableStringNone => Option<string?>.None;
  public static void NoOp() { }
  public static void NoOp<T>(T _) { }

  public static async Task NoOpAsync()
  {
    await Task.Delay(1);
  }

  public static async Task NoOpAsync<T>(T _)
  {
    await Task.Delay(1);
  }
}
