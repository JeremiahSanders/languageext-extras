using System.Text.Json;

using LanguageExt;
using LanguageExt.Common;

namespace Jds.LanguageExt.Extras;

internal static class StringExtensions
{
  public static Result<T> TryDeserializeJson<T>(this string possibleJson, JsonSerializerOptions? options = null)
    where T : notnull
  {
    return Prelude.Try(() => JsonSerializer.Deserialize<T>(possibleJson, options)!).Try();
  }
}
