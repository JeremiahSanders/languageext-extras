using System.Text.Json;

using LanguageExt.Common;

namespace Jds.LanguageExt.Extras;

public static class HttpResponseMessageExtensions
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
    return (await message.TryReadContentAsStringAsync(cancellationToken)).Bind(body =>
      body.TryDeserializeJson<T>(options));
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
