using System.Net.Http.Json;
using System.Text.Json;

using LanguageExt;

namespace Jds.LanguageExt.Extras;

/// <summary>
///   Methods extending <see cref="HttpResponseMessage" />.
/// </summary>
public static class HttpResponseMessageExtensions
{
  /// <summary>
  ///   Read the message <see cref="HttpResponseMessage.Content" /> as a <see cref="string" />, catching exceptions.
  /// </summary>
  /// <param name="message">This <see cref="HttpResponseMessage" />.</param>
  /// <param name="cancellationToken">An asynchronous operation cancellation token.</param>
  /// <returns>A <see cref="TryAsync{A}" /> of <see cref="string" />.</returns>
  public static TryAsync<string> TryReadContentAsStringAsync(this HttpResponseMessage message,
    CancellationToken cancellationToken = default)
  {
    return message.Content.TryReadAsStringAsync(cancellationToken);
  }

  /// <summary>
  ///   Deserialize the message <see cref="HttpResponseMessage.Content" /> using
  ///   <see
  ///     cref="HttpContentJsonExtensions.ReadFromJsonAsync{T}(System.Net.Http.HttpContent,System.Text.Json.JsonSerializerOptions?,System.Threading.CancellationToken)" />
  ///   , catching exceptions.
  /// </summary>
  /// <param name="message">This <see cref="HttpResponseMessage" />.</param>
  /// <param name="options">Optional <see cref="JsonSerializerOptions" />.</param>
  /// <param name="cancellationToken">An asynchronous operation cancellation token.</param>
  /// <typeparam name="T">A response <see cref="HttpResponseMessage.Content" /> deserialization type.</typeparam>
  /// <returns>A <see cref="TryAsync{A}" /> of <typeparamref name="T" />.</returns>
  public static TryAsync<T> TryDeserializeContentAsJsonAsync<T>(this HttpResponseMessage message,
    JsonSerializerOptions? options = null,
    CancellationToken cancellationToken = default) where T : notnull
  {
    return Prelude.TryAsync(async () =>
      await message.Content.ReadFromJsonAsync<T>(options, cancellationToken) ??
      throw new JsonException("Failed to deserialize JSON")
    );
  }
}
