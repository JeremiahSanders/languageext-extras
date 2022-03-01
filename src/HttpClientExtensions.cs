using LanguageExt;
using LanguageExt.Common;

namespace Jds.LanguageExt.Extras;

internal static class HttpClientExtensions
{
  private const HttpCompletionOption DefaultCompletionOption = HttpCompletionOption.ResponseContentRead;

  public static Task<Result<HttpResponseMessage>> TryDeleteAsync(this HttpClient httpClient,
    Uri? uri,
    CancellationToken cancellationToken = default
  )
  {
    return Prelude.TryAsync(() => httpClient.DeleteAsync(uri, cancellationToken)).Try();
  }

  public static Task<Result<HttpResponseMessage>> TryGetAsync(this HttpClient httpClient,
    Uri? uri,
    HttpCompletionOption completionOption,
    CancellationToken cancellationToken = default
  )
  {
    return Prelude.TryAsync(() => httpClient.GetAsync(uri, completionOption, cancellationToken)).Try();
  }

  public static Task<Result<HttpResponseMessage>> TryGetAsync(this HttpClient httpClient,
    Uri? uri,
    CancellationToken cancellationToken = default
  )
  {
    return httpClient.TryGetAsync(uri, DefaultCompletionOption, cancellationToken);
  }

  public static Task<Result<HttpResponseMessage>> TryPostAsync(this HttpClient httpClient,
    Uri? uri,
    HttpContent content,
    CancellationToken cancellationToken = default
  )
  {
    return Prelude.TryAsync(() => httpClient.PostAsync(uri, content, cancellationToken)).Try();
  }

  public static Task<Result<HttpResponseMessage>> TryPutAsync(this HttpClient httpClient,
    Uri? uri,
    HttpContent content,
    CancellationToken cancellationToken = default
  )
  {
    return Prelude.TryAsync(() => httpClient.PutAsync(uri, content, cancellationToken)).Try();
  }

  public static Task<Result<HttpResponseMessage>> TrySendAsync(this HttpClient httpClient,
    HttpRequestMessage requestMessage,
    CancellationToken cancellationToken = default
  )
  {
    return httpClient.TrySendAsync(requestMessage, DefaultCompletionOption, cancellationToken);
  }

  public static Task<Result<HttpResponseMessage>> TrySendAsync(this HttpClient httpClient,
    HttpRequestMessage requestMessage,
    HttpCompletionOption completionOption,
    CancellationToken cancellationToken = default
  )
  {
    return Prelude.TryAsync(() => httpClient.SendAsync(requestMessage, completionOption, cancellationToken)).Try();
  }
}
