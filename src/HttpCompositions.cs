using System.Text.Json;

using LanguageExt;
using LanguageExt.Common;

namespace Jds.LanguageExt.Extras;

/// <summary>
///   Methods composing HTTP message and request operations into workflows.
/// </summary>
public static class HttpCompositions
{
  /// <summary>
  ///   Asynchronously read the message <see cref="HttpResponseMessage.Content" /> as a string and deserialize it as JSON,
  ///   catching exceptions.
  /// </summary>
  /// <param name="message">This <see cref="HttpResponseMessage" />.</param>
  /// <param name="jsonSerializerOptions">Optional <see cref="JsonSerializerOptions" />.</param>
  /// <param name="cancellationToken">An asynchronous operation cancellation token.</param>
  /// <typeparam name="T">A response <see cref="HttpResponseMessage.Content" /> deserialization type.</typeparam>
  /// <returns>
  ///   A tuple of this message, a <see cref="Result{A}" /> of reading the <see cref="HttpResponseMessage.Content" /> as a
  ///   <see cref="string" />, and a <see cref="Result{A}" /> of deserializing the <see cref="HttpResponseMessage.Content" />
  ///   as <typeparamref name="T" />.
  /// </returns>
  public static async Task<DeserializedHttpResponseMessage<T>>
    TryReadContentAsJsonAsync<T>(
      this HttpResponseMessage message,
      JsonSerializerOptions? jsonSerializerOptions = null,
      CancellationToken cancellationToken = default
    ) where T : notnull
  {
    var possibleContent = await message.TryReadContentAsStringAsync(cancellationToken).Try();
    var possibleBody = possibleContent.Bind(content => content.TryDeserializeJson<T>(jsonSerializerOptions).Try());

    return new DeserializedHttpResponseMessage<T>(message, possibleContent, possibleBody);
  }

  /// <summary>
  ///   Asynchronously sends the <paramref name="httpRequestMessage" /> and attempts to parse the response
  ///   <see cref="HttpResponseMessage.Content" /> as a <see cref="string" /> and deserialize it as JSON, catching
  ///   exceptions.
  /// </summary>
  /// <param name="httpClient">This <see cref="HttpClient" />.</param>
  /// <param name="httpRequestMessage">A <see cref="HttpRequestMessage" />.</param>
  /// <param name="jsonSerializerOptions">Optional <see cref="JsonSerializerOptions" />.</param>
  /// <param name="cancellationToken">An asynchronous operation cancellation token.</param>
  /// <typeparam name="T">A response <see cref="HttpResponseMessage.Content" /> deserialization type.</typeparam>
  /// <returns>
  ///   A <see cref="TryAsync{A}" /> of <see cref="DeserializedHttpResponseMessage{TBody}" />.
  /// </returns>
  public static TryAsync<DeserializedHttpResponseMessage<T>>
    TrySendForJsonAsync<T>(
      this HttpClient httpClient,
      HttpRequestMessage httpRequestMessage,
      JsonSerializerOptions? jsonSerializerOptions = null,
      CancellationToken cancellationToken = default
    ) where T : notnull
  {
    return httpClient.TrySendAsync(httpRequestMessage, cancellationToken)
      .MapAsync(responseMessage =>
        responseMessage.TryReadContentAsJsonAsync<T>(jsonSerializerOptions, cancellationToken));
  }

  /// <summary>
  ///   Executes <see cref="HttpClient.GetAsync(Uri?,HttpCompletionOption,CancellationToken)" />, with
  ///   <see cref="HttpCompletionOption.ResponseContentRead" />, catching exceptions.
  /// </summary>
  /// <param name="httpClient">This <see cref="HttpClient" />.</param>
  /// <param name="uri">A <see cref="Uri" /> the request is sent to.</param>
  /// <param name="jsonSerializerOptions">Optional <see cref="JsonSerializerOptions" />.</param>
  /// <param name="cancellationToken">An asynchronous operation cancellation token.</param>
  /// <returns>A <see cref="TryAsync{A}" /> of <see cref="HttpResponseMessage" />.</returns>
  public static TryAsync<DeserializedHttpResponseMessage<T>> TryGetForJsonAsync<T>(
    this HttpClient httpClient,
    Uri? uri,
    JsonSerializerOptions? jsonSerializerOptions = null,
    CancellationToken cancellationToken = default
  ) where T : notnull
  {
    return httpClient.TryGetAsync(uri, HttpCompletionOption.ResponseContentRead, cancellationToken)
      .MapAsync(responseMessage =>
        responseMessage.TryReadContentAsJsonAsync<T>(jsonSerializerOptions, cancellationToken));
  }

  /// <summary>
  ///   Executes <see cref="HttpClient.PostAsync(Uri?,HttpContent,CancellationToken)" />, catching exceptions.
  /// </summary>
  /// <param name="httpClient">This <see cref="HttpClient" />.</param>
  /// <param name="uri">A <see cref="Uri" /> the request is sent to.</param>
  /// <param name="content">A <see cref="HttpContent" /> sent to the server.</param>
  /// <param name="jsonSerializerOptions">Optional <see cref="JsonSerializerOptions" />.</param>
  /// <param name="cancellationToken">An asynchronous operation cancellation token.</param>
  /// <returns>A <see cref="TryAsync{A}" /> of <see cref="HttpResponseMessage" />.</returns>
  public static TryAsync<DeserializedHttpResponseMessage<T>> TryPostForJsonAsync<T>(
    this HttpClient httpClient,
    Uri? uri,
    HttpContent? content,
    JsonSerializerOptions? jsonSerializerOptions = null,
    CancellationToken cancellationToken = default
  ) where T : notnull
  {
    return httpClient.TryPostAsync(uri, content, cancellationToken)
      .MapAsync(responseMessage =>
        responseMessage.TryReadContentAsJsonAsync<T>(jsonSerializerOptions, cancellationToken));
  }

  /// <summary>
  ///   Executes <see cref="HttpClient.PutAsync(Uri?,HttpContent,CancellationToken)" />, catching exceptions.
  /// </summary>
  /// <param name="httpClient">This <see cref="HttpClient" />.</param>
  /// <param name="uri">A <see cref="Uri" /> the request is sent to.</param>
  /// <param name="content">A <see cref="HttpContent" /> sent to the server.</param>
  /// <param name="jsonSerializerOptions">Optional <see cref="JsonSerializerOptions" />.</param>
  /// <param name="cancellationToken">An asynchronous operation cancellation token.</param>
  /// <returns>A <see cref="TryAsync{A}" /> of <see cref="HttpResponseMessage" />.</returns>
  public static TryAsync<DeserializedHttpResponseMessage<T>> TryPutForJsonAsync<T>(
    this HttpClient httpClient,
    Uri? uri,
    HttpContent? content,
    JsonSerializerOptions? jsonSerializerOptions = null,
    CancellationToken cancellationToken = default
  ) where T : notnull
  {
    return httpClient.TryPutAsync(uri, content, cancellationToken)
      .MapAsync(responseMessage =>
        responseMessage.TryReadContentAsJsonAsync<T>(jsonSerializerOptions, cancellationToken));
  }
}
