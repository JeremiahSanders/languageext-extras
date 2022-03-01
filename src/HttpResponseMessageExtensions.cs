using System.Net.Http.Json;
using System.Text.Json;

using LanguageExt;
using LanguageExt.Common;

namespace Jds.LanguageExt.Extras;

internal static class HttpResponseMessageExtensions
{
  public static Task<Result<string>> TryReadContentAsStringAsync(this HttpResponseMessage message,
    CancellationToken cancellationToken = default)
  {
    return message.Content.TryReadAsStringAsync(cancellationToken);
  }

  public static async Task<Result<T>> TryDeserializeContentAsJsonAsync<T>(this HttpResponseMessage message,
    JsonSerializerOptions? options = null,
    CancellationToken cancellationToken = default) where T : notnull
  {
    return await Prelude.TryAsync(async () =>
        await message.Content.ReadFromJsonAsync<T>(options, cancellationToken) ??
        throw new Exception("Failed to deserialize JSON")
      )
      .Try();
  }

  public static async Task<(HttpResponseMessage Message, Result<string> Content, Result<T> Body)>
    TryReadContentAsJsonAsync<T>(
      this HttpResponseMessage message,
      JsonSerializerOptions? jsonSerializerOptions = null,
      CancellationToken cancellationToken = default
    ) where T : notnull
  {
    var possibleContent = await message.TryReadContentAsStringAsync(cancellationToken);
    var possibleBody = possibleContent.Bind(content => content.TryDeserializeJson<T>(jsonSerializerOptions));

    return (message, possibleContent, possibleBody);
  }
}
