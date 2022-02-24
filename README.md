# LanguageExt.Extras

A collection of extension methods and helpers which extend the use of [`LanguageExt`][LanguageExt].

> This project is **not** affiliated with [LanguageExt][] and asserts no claims upon its intellectual property.

## Installation

Install LanguageExt.Extras as a NuGet package, via an IDE package manager, or using the command-line instructions
at [nuget.org][].

## Use / API

### Using Statements

LanguageExt.Extras extends existing [LanguageExt][] types. As such, you'll need _both_ [LanguageExt][]
and [LanguageExt.Extras][nuget.org] in scope.

All API examples shown below assume the following `using` statements:

```c#
using LanguageExt;
using LanguageExt.Common;
using Jds.LanguageExt.Extras;
```

### `Either<TLeft, TRight>` Extensions

* `Either<TLeft, TRight>.Filter<TLeft, TRight>(Func<TRight, bool> filter, Func<TRight, TLeft> onFalse)`
  * Filters right values by executing `filter`. If it returns `true`, the existing value is returned. If it
    returns `false`, then it executes `onFalse` to create a `TLeft`.
* `Either<TLeft, TRight>.FilterAsync<TLeft, TRight>(Func<TRight, Task<bool>> filter, Func<TRight, TLeft> onFalse)`
  * Filters right values by executing `filter`. If it returns `true`, the existing value is returned. If it
    returns `false`, then it executes `onFalse` to create a `TLeft`.
* `Either<TLeft, TRight>.FilterAsync<TLeft, TRight>(Func<TRight, Task<bool>> filter, Func<TRight, Task<TLeft>> onFalse)`
  * Filters right values by executing `filter`. If it returns `true`, the existing value is returned. If it
    returns `false`, then it executes `onFalse` to create a `TLeft`.
* `Either<TLeft, TRight>.Tap<TLeft, TRight>(Action<TRight> onRight, Action<TLeft> onLeft)`
  * Executes a side effect, e.g., logging, based upon the state of the either. The current value is returned unchanged.
* `Either<TLeft, TRight>.TapAsync<TLeft, TRight>(Func<TRight, Task> onSuccess, Func<TLeft, Task> onFailure)`
  * Executes an asynchronous side effect, e.g., dispatching a status notification via `HttpClient`, based upon the state
    of the either. The current value is returned unchanged.
* `Either<TLeft, TRight>.TapLeft<TLeft, TRight>(Action<TLeft> onFailure)`
  * Executes a side effect, e.g., logging, when the either is a left. The current value is returned unchanged.
* `Either<TLeft, TRight>.TapLeftAsync<TLeft, TRight>(Func<TLeft, Task> onFailure)`
  * Executes an asynchronous side effect, e.g., dispatching a status notification via `HttpClient`, when the either is a
    left. The current value is returned unchanged.
* `Either<TLeft, TRight>.TapRight<TLeft, TRight>(Action<TRight> onSuccess)`
  * Executes a side effect, e.g., logging, when the either is a right. The current value is returned unchanged.
* `Either<TLeft, TRight>.TapRightAsync<TLeft, TRight>(Func<TRight, Task> onSuccess)`
  * Executes an asynchronous side effect, e.g., dispatching a status notification via `HttpClient`, when the either is a
    right. The current value is returned unchanged.
* `Either<TLeft, TRight>.ToResult<TLeft, TRight>(Func<TLeft, Exception> ifLeft)`
  * Converts the `Either<TLeft, TRight>` into a `Result<TRight>`.
* `Either<TLeft, TRight>.ToResult<TLeft, TRight>() where TLeft : Exception`
  * Converts the `Either<TLeft, TRight>` into a `Result<TRight>`.

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

### `Result<TSuccess>` Extensions

* `Result<TSuccess>.Bind<TSuccess, TNewSuccess>(Func<TSuccess, Result<TNewSuccess>> func)`
  * Executes `func`, which returns `Result<TNewSuccess>`, when the result is a success.
* `Result<TSuccess>.BindAsync<TSuccess, TNewSuccess>(Func<TSuccess, Task<Result<TNewSuccess>>> func)`
  * Executes `func`, which returns `Task<Result<TNewSuccess>>`, when the result is a success.
* `Result<TSuccess>.Filter<TSuccess>(Func<TSuccess, bool> filter, Func<TSuccess, Exception> onFalse)`
  * Filters `TSuccess` values by executing `filter`. If it returns `true`, the existing value is returned. If it returns `false`, then it executes `onFalse` to create an `Exception`.
* `Result<TSuccess>.FilterAsync<TSuccess>(Func<TSuccess, Task<bool>> filter, Func<TSuccess, Exception> onFalse)`
  * Filters `TSuccess` values by executing `filter`. If it returns `true`, the existing value is returned. If it returns `false`, then it executes `onFalse` to create an `Exception`.
* `Result<TSuccess>.FilterAsync<TSuccess>(Func<TSuccess, Task<bool>> filter, Func<TSuccess, Task<Exception>> onFalse)`
  * Filters `TSuccess` values by executing `filter`. If it returns `true`, the existing value is returned. If it returns `false`, then it executes `onFalse` to create an `Exception`.
* `Result<TSucess>.IfFailThrow<TSuccess>()`
  * Throws an `InvalidOperationException` if the `Result<TSucess>` is an `Exception`, returning `TSuccess` upon success.
* `Result<TSuccess>.Tap<TSuccess>(Action<TSuccess> onSuccess, Action<Exception> onFailure)`
  * Executes a side effect, e.g., logging, based upon the state of the result. The current value is returned unchanged.
* `Result<TSuccess>.TapAsync<TSuccess>(Func<TSuccess, Task> onSuccess, Func<Exception, Task> onFailure)`
  * Executes an asynchronous side effect, e.g., dispatching a status notification via `HttpClient`, based upon the state
    of the result. The current value is returned unchanged.
* `Result<TSuccess>.TapFailure<TSuccess>(Action<Exception> onFailure)`
  * Executes a side effect, e.g., logging, when the result is a failure. The current value is returned unchanged.
* `Result<TSuccess>.TapFailureAsync<TSuccess>(Func<Exception, Task> onFailure)`
  * Executes an asynchronous side effect, e.g., dispatching a status notification via `HttpClient`, when the result is a
    failure. The current value is returned unchanged.
* `Result<TSuccess>.TapSuccess<TSuccess>(Action<TSuccess> onSuccess)`
  * Executes a side effect, e.g., logging, when the result is a success. The current value is returned unchanged.
* `Result<TSuccess>.TapSuccessAsync<TSuccess>(Func<TSuccess, Task> onSuccess)`
  * Executes an asynchronous side effect, e.g., dispatching a status notification via `HttpClient`, when the result is a
    success. The current value is returned unchanged.
* `Result<TSuccess>.ToEither<TSuccess>()`
  * Converts the `Result<TSuccess>` into an `Either<Exception, TSuccess>`.

### `string` Extensions

* `string.TryDeserializeJson<T>(JsonSerializerOptions? options = null) where T : notnull`
  * Attempts to deserialize the `string` as a `T`, returning `Result<T>`.

[LanguageExt]: https://github.com/louthy/language-ext

[LanguageExt license]: https://github.com/louthy/language-ext/blob/main/LICENSE.md

[nuget.org]: https://www.nuget.org/packages/Jds.LanguageExt.Extras/
