# Experimental Features

The following APIs are not yet part of the public API. They may be removed or completed and moved to the public API.

## `HttpClient` / Web API Helpers

### `HttpClient` Extensions

* `HttpClient.TryDeleteAsync(Uri? uri, CancellationToken cancellationToken = default)`
  * Tries to asynchronously execute `.DeleteAsync()`, returning a `Task<Result<HttpResponseMessage>>`
* `HttpClient.TryGetAsync(Uri? uri, HttpCompletionOption completionOption, CancellationToken cancellationToken = default)`
* `HttpClient.TryGetAsync(Uri? uri, CancellationToken cancellationToken = default)`
  * Tries to asynchronously execute `.GetAsync()`, returning a `Task<Result<HttpResponseMessage>>`
* `HttpClient.TryPostAsync(Uri? uri, HttpContent content, CancellationToken cancellationToken = default)`
  * Tries to asynchronously execute `.PostAsync()`, returning a `Task<Result<HttpResponseMessage>>`
* `HttpClient.TryPutAsync(Uri? uri, HttpContent content, CancellationToken cancellationToken = default)`
  * Tries to asynchronously execute `.PutAsync()`, returning a `Task<Result<HttpResponseMessage>>`
* `HttpClient.TrySendAsync(HttpRequestMessage requestMessage, HttpCompletionOption completionOption, CancellationToken cancellationToken = default)`
* `HttpClient.TrySendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken = default)`
  * Tries to asynchronously execute `.SendAsync()`, returning a `Task<Result<HttpResponseMessage>>`

### `HttpContent` Extensions

* `HttpContent.TryReadAsStringAsync(CancellationToken cancellationToken = default)`
  * Tries to asynchronously read the `HttpContent` as a `string`, returning a `Task<Result<string>>`

### `HttpResponseMessage` Extensions

* `HttpResponseMessage.TryReadContentAsStringAsync(CancellationToken cancellationToken = default)`
  * Tries to asynchronously read the message's `.Content` as a `string`, returning a `Task<Result<string>>`.
* `HttpResponseMessage.TryDeserializeContentAsJsonAsync<T>(JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) where T : notnull`
  * Tries to asynchronously read the message's `.Content` as a JSON `string` and deserialize it as `T`, returning a `Task<Result<T>>`.
* `HttpResponseMessage.TryReadContentAsJsonAsync<T>(JsonSerializerOptions? jsonSerializerOptions = null, CancellationToken cancellationToken = default) where T : notnull`
  * Tries to asynchronously read the message's `.Content` as a JSON `string` and deserialize it as `T`, returning all components, `Task<(HttpResponseMessage Message, Result<string> Content, Result<T> Body)>`.
  * This method is extremely useful in ASP.NET integration tests. It succinctly parses API responses and maintains full access to the `HttpResponseMessage` and any `Exception`s, supporting thorough testing of success and failure paths. 

### `string` Extensions

* `string.TryDeserializeJson<T>(JsonSerializerOptions? options = null) where T : notnull`
  * Attempts to deserialize the `string` as a `T`, returning `Result<T>`.
