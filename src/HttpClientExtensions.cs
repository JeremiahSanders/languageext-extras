using System.Net.Http.Json;

using LanguageExt;

namespace Jds.LanguageExt.Extras;

/// <summary>
///   Methods extending <see cref="HttpClient" />.
/// </summary>
public static class HttpClientExtensions
{
  private const HttpCompletionOption DefaultCompletionOption = HttpCompletionOption.ResponseContentRead;

  /// <summary>
  ///   Executes <see cref="HttpClient.DeleteAsync(Uri?)" />, catching exceptions.
  /// </summary>
  /// <param name="httpClient">This <see cref="HttpClient" />.</param>
  /// <param name="uri">A <see cref="Uri" /> the request is sent to.</param>
  /// <param name="cancellationToken">An asynchronous operation cancellation token.</param>
  /// <returns>A <see cref="TryAsync{A}" /> of <see cref="HttpResponseMessage" />.</returns>
  public static TryAsync<HttpResponseMessage> TryDeleteAsync(this HttpClient httpClient,
    Uri? uri,
    CancellationToken cancellationToken = default
  )
  {
    return Prelude.TryAsync(() => httpClient.DeleteAsync(uri, cancellationToken));
  }

  /// <summary>
  ///   Executes <see cref="HttpClient.GetAsync(Uri?,HttpCompletionOption,CancellationToken)" />, catching exceptions.
  /// </summary>
  /// <param name="httpClient">This <see cref="HttpClient" />.</param>
  /// <param name="uri">A <see cref="Uri" /> the request is sent to.</param>
  /// <param name="completionOption">When the operation should complete.</param>
  /// <param name="cancellationToken">An asynchronous operation cancellation token.</param>
  /// <returns>A <see cref="TryAsync{A}" /> of <see cref="HttpResponseMessage" />.</returns>
  public static TryAsync<HttpResponseMessage> TryGetAsync(this HttpClient httpClient,
    Uri? uri,
    HttpCompletionOption completionOption,
    CancellationToken cancellationToken = default
  )
  {
    return Prelude.TryAsync(() => httpClient.GetAsync(uri, completionOption, cancellationToken));
  }

  /// <summary>
  ///   Executes <see cref="HttpClient.GetAsync(Uri?,HttpCompletionOption,CancellationToken)" />, with
  ///   <see cref="HttpCompletionOption.ResponseContentRead" />, catching exceptions.
  /// </summary>
  /// <param name="httpClient">This <see cref="HttpClient" />.</param>
  /// <param name="uri">A <see cref="Uri" /> the request is sent to.</param>
  /// <param name="cancellationToken">An asynchronous operation cancellation token.</param>
  /// <returns>A <see cref="TryAsync{A}" /> of <see cref="HttpResponseMessage" />.</returns>
  public static TryAsync<HttpResponseMessage> TryGetAsync(this HttpClient httpClient,
    Uri? uri,
    CancellationToken cancellationToken = default
  )
  {
    return httpClient.TryGetAsync(uri, DefaultCompletionOption, cancellationToken);
  }

  /// <summary>
  ///   Executes <see cref="HttpClient.PostAsync(Uri?,HttpContent,CancellationToken)" />, catching exceptions.
  /// </summary>
  /// <param name="httpClient">This <see cref="HttpClient" />.</param>
  /// <param name="uri">A <see cref="Uri" /> the request is sent to.</param>
  /// <param name="content">A <see cref="HttpContent" /> sent to the server.</param>
  /// <param name="cancellationToken">An asynchronous operation cancellation token.</param>
  /// <returns>A <see cref="TryAsync{A}" /> of <see cref="HttpResponseMessage" />.</returns>
  public static TryAsync<HttpResponseMessage> TryPostAsync(this HttpClient httpClient,
    Uri? uri,
    HttpContent? content,
    CancellationToken cancellationToken = default
  )
  {
    return Prelude.TryAsync(() => httpClient.PostAsync(uri, content, cancellationToken));
  }

  /// <summary>
  ///   Executes <see cref="HttpClient.PutAsync(Uri?,HttpContent,CancellationToken)" />, catching exceptions.
  /// </summary>
  /// <param name="httpClient">This <see cref="HttpClient" />.</param>
  /// <param name="uri">A <see cref="Uri" /> the request is sent to.</param>
  /// <param name="content">A <see cref="HttpContent" /> sent to the server.</param>
  /// <param name="cancellationToken">An asynchronous operation cancellation token.</param>
  /// <returns>A <see cref="TryAsync{A}" /> of <see cref="HttpResponseMessage" />.</returns>
  public static TryAsync<HttpResponseMessage> TryPutAsync(this HttpClient httpClient,
    Uri? uri,
    HttpContent? content,
    CancellationToken cancellationToken = default
  )
  {
    return Prelude.TryAsync(() => httpClient.PutAsync(uri, content, cancellationToken));
  }

  /// <summary>
  ///   Executes <see cref="HttpClient.SendAsync(HttpRequestMessage,HttpCompletionOption,CancellationToken)" />, with
  ///   <see cref="HttpCompletionOption.ResponseContentRead" />, catching
  ///   exceptions.
  /// </summary>
  /// <param name="httpClient">This <see cref="HttpClient" />.</param>
  /// <param name="requestMessage">A <see cref="HttpRequestMessage" /> to send.</param>
  /// <param name="cancellationToken">An asynchronous operation cancellation token.</param>
  /// <returns>A <see cref="TryAsync{A}" /> of <see cref="HttpResponseMessage" />.</returns>
  public static TryAsync<HttpResponseMessage> TrySendAsync(this HttpClient httpClient,
    HttpRequestMessage requestMessage,
    CancellationToken cancellationToken = default
  )
  {
    return httpClient.TrySendAsync(requestMessage, DefaultCompletionOption, cancellationToken);
  }

  /// <summary>
  ///   Executes <see cref="HttpClient.SendAsync(HttpRequestMessage,HttpCompletionOption,CancellationToken)" />, catching
  ///   exceptions.
  /// </summary>
  /// <param name="httpClient">This <see cref="HttpClient" />.</param>
  /// <param name="requestMessage">A <see cref="HttpRequestMessage" /> to send.</param>
  /// <param name="completionOption">When the operation should complete.</param>
  /// <param name="cancellationToken">An asynchronous operation cancellation token.</param>
  /// <returns>A <see cref="TryAsync{A}" /> of <see cref="HttpResponseMessage" />.</returns>
  public static TryAsync<HttpResponseMessage> TrySendAsync(this HttpClient httpClient,
    HttpRequestMessage requestMessage,
    HttpCompletionOption completionOption,
    CancellationToken cancellationToken = default
  )
  {
    return Prelude.TryAsync(() => httpClient.SendAsync(requestMessage, completionOption, cancellationToken));
  }
}
