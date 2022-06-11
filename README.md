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

* `Either<TLeft, TRight>.BindLeftAsync<TLeft, TRight, TLeft2>(Func<TLeft, Task<Either<TLeft2, TRight>>> func)`
  * Executes `func`, which returns a `Task<Either<TLeft2, TRight>>`, when the either is a `TLeft`.
* `Either<TLeft, TRight>.Filter<TLeft, TRight>(Func<TRight, bool> filter, Func<TRight, TLeft> onFalse)`
  * Filters right values by executing `filter`. If it returns `true`, the existing value is returned. If it
    returns `false`, then it executes `onFalse` to create a `TLeft`.
* `Either<TLeft, TRight>.FilterAsync<TLeft, TRight>(Func<TRight, Task<bool>> filter, Func<TRight, TLeft> onFalse)`
  * Filters right values by executing `filter`. If it returns `true`, the existing value is returned. If it
    returns `false`, then it executes `onFalse` to create a `TLeft`.
* `Either<TLeft, TRight>.FilterAsync<TLeft, TRight>(Func<TRight, Task<bool>> filter, Func<TRight, Task<TLeft>> onFalse)`
  * Filters right values by executing `filter`. If it returns `true`, the existing value is returned. If it
    returns `false`, then it executes `onFalse` to create a `TLeft`.
* `Either<TLeft, TRight>.MapLeftAsync<TLeft, TRight, TLeft2>(Func<TLeft, Task<TLeft2>> func)`
  * Executes `func`, which returns a `Task<TLeft2>`, when the either is a `TLeft`.
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

### `Option<TSome>` Extensions

* `Option<TSome>.Tap(Action<TSome> ifSome, Action ifNone)`
  * Executes a side effect, e.g., logging, based upon the state of the option. The current value is returned unchanged.
* `Option<TSome>.TapAsync(Func<TSome,Task> ifSome, Func<Task> ifNone)`
  * Executes an asynchronous side effect, e.g., dispatching a status notification via `HttpClient`, based upon the state of the option. The current value is returned unchanged.
* `Option<TSome>.TapNone(Action ifNone)`
  * Executes a side effect, e.g., logging, if the option is None. The current value is returned unchanged.
* `Option<TSome>.TapNoneAsync(Func<Task> ifNone)`
  * Executes an asynchronous side effect, e.g., dispatching a status notification via `HttpClient`, if the option is None. The current value is returned unchanged.
* `Option<TSome>.TapSome(Action<TSome> ifSome)`
  * Executes a side effect, e.g., logging, if the option is Some. The current value is returned unchanged.
* `Option<TSome>.TapSomeAsync(Func<TSome,Task> ifSome)`
  * Executes an asynchronous side effect, e.g., dispatching a status notification via `HttpClient`, if the option is Some. The current value is returned unchanged.
* `Option<TSome?>`.ToNonNullable()
  * Converts the nullable `TSome` generic argument to a non-nullable `TSome` generic argument, e.g., `Option<string?>` returns `Option<string>`. The existing value is maintained unchanged.
  * **This method assumes execution within a [`#nullable` context][nullable-context] (i.e., in a project where nullable reference types are enabled).**

### `Result<TSuccess>` Extensions

* `Result<TSuccess>.Bind<TSuccess, TNewSuccess>(Func<TSuccess, Result<TNewSuccess>> func)`
  * Executes `func`, which returns `Result<TNewSuccess>`, when the result is a success.
* `Result<TSuccess>.BindAsync<TSuccess, TNewSuccess>(Func<TSuccess, Task<Result<TNewSuccess>>> func)`
  * Executes `func`, which returns `Task<Result<TNewSuccess>>`, when the result is a success.
* `Result<TSuccess>.BindFailure<TSuccess>(Func<Exception, Result<TSuccess>> func)`
  * Executes `func`, which returns `Result<TSuccess>`, when the result is a failure.
* `Result<TSuccess>.BindFailureAsync<TSuccess>(Func<Exception, Task<Result<TSuccess>>> func)`
  * Executes `func`, which returns `Task<Result<TSuccess>>`, when the result is a failure.
* `Result<TSuccess>.Filter<TSuccess>(Func<TSuccess, bool> filter, Func<TSuccess, Exception> onFalse)`
  * Filters `TSuccess` values by executing `filter`. If it returns `true`, the existing value is returned. If it returns `false`, then it executes `onFalse` to create an `Exception`.
* `Result<TSuccess>.FilterAsync<TSuccess>(Func<TSuccess, Task<bool>> filter, Func<TSuccess, Exception> onFalse)`
  * Filters `TSuccess` values by executing `filter`. If it returns `true`, the existing value is returned. If it returns `false`, then it executes `onFalse` to create an `Exception`.
* `Result<TSuccess>.FilterAsync<TSuccess>(Func<TSuccess, Task<bool>> filter, Func<TSuccess, Task<Exception>> onFalse)`
  * Filters `TSuccess` values by executing `filter`. If it returns `true`, the existing value is returned. If it returns `false`, then it executes `onFalse` to create an `Exception`.
* `Result<TSucess>.IfFailThrow<TSuccess>()`
  * Throws an `InvalidOperationException` if the `Result<TSucess>` is an `Exception`, returning `TSuccess` upon success.
* `Result<TSuccess>.MapFailure<TSuccess, TFailure>(Func<Exception, TFailure> func) where TFailure : Exception`
  * Executes `func`, which returns an `Exception`, when the result is a failure.
* `Result<TSuccess>.MapFailureAsync<TSuccess, TFailure>(Func<Exception, Task<TFailure>> func) where TFailure : Exception`
  * Executes `func`, which returns a `Task<Exception>`, when the result is a failure.
* `Result<TSuccess>.MapSafe<TSuccess, TNewSuccess>(Func<TSuccess, TNewSuccess> func)`
  * Executes `func`, which returns `TNewSuccess`, when the result is a success. Exceptions are caught and returned as a failure. (I.e., this simplifies `result.Bind(value => Prelude.Try(() => func(value)).Try())`)
* `Result<TSuccess>.MapSafeAsync<TSuccess, TNewSuccess>(Func<TSuccess, Task<TNewSuccess>> func)`
  * Executes `func`, which returns `Task<TNewSuccess>`, when the result is a success. Exceptions are caught and returned as a failure. (I.e., this simplifies `result.BindAsync(value => Prelude.TryAsync(() => func(value)).Try())`)
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

### `Try<TSuccess>` Extensions

* `Try<TSuccess>.Filter<TSuccess>(Func<TSuccess, bool> filter, Func<TSuccess, Exception> onFalse)`
  * Filters `TSuccess` results by executing `filter`. If it returns `true`, the existing value is returned. If it returns `false`, then it executes `onFalse` to create an `Exception`. Memoizes successful results.
* `Try<TSuccess>.Tap<TSuccess>(Action<TSuccess> onSuccess, Action<Exception> onFailure)`
  * Executes a side effect, e.g., logging, based upon the state of the result. The current value is returned unchanged.
* `Try<TSuccess>.TapFailure<TSuccess>(Action<Exception> onFailure)`
  * Executes a side effect, e.g., logging, when the result is a failure. The current value is returned unchanged.
* `Try<TSuccess>.TapSuccess<TSuccess>(Action<TSuccess> onSuccess)`
  * Executes a side effect, e.g., logging, when the result is a success. The current value is returned unchanged.

### `TryAsync<TSuccess>` Extensions

* `TryAsync<TSuccess>.Filter<TSuccess>(Func<TSuccess, bool> filter, Func<TSuccess, Exception> onFalse)`
  * Filters `TSuccess` results by executing `filter`. If it returns `true`, the existing value is returned. If it returns `false`, then it executes `onFalse` to create an `Exception`. Memoizes successful results.
* `TryAsync<TSuccess>.FilterAsync<TSuccess>(Func<TSuccess, Task<bool>> filter, Func<TSuccess, Exception> onFalse)`
  * Filters `TSuccess` results by executing `filter`. If it returns `true`, the existing value is returned. If it returns `false`, then it executes `onFalse` to create an `Exception`. Memoizes successful results.
* `TryAsync<TSuccess>.Tap<TSuccess>(Action<TSuccess> onSuccess, Action<Exception> onFailure)`
  * Executes a side effect, e.g., logging, based upon the state of the result. The current value is returned unchanged.
* `TryAsync<TSuccess>.Tap<TSuccess>(Func<TSuccess, Task> onSuccess, Func<Exception, Task> onFailure)`
  * Executes an asynchronous side effect, e.g., dispatching a status notification via `HttpClient`, based upon the state
    of the result. The current value is returned unchanged.
* `TryAsync<TSuccess>.TapFailure<TSuccess>(Action<Exception> onFailure)`
  * Executes a side effect, e.g., logging, when the result is a failure. The current value is returned unchanged.
* `TryAsync<TSuccess>.TapFailure<TSuccess>(Func<Exception, Task> onFailure)`
  * Executes an asynchronous side effect, e.g., dispatching a status notification via `HttpClient`, when the result is a
    failure. The current value is returned unchanged.
* `TryAsync<TSuccess>.TapSuccess<TSuccess>(Action<TSuccess> onSuccess)`
  * Executes a side effect, e.g., logging, when the result is a success. The current value is returned unchanged.
* `TryAsync<TSuccess>.TapSuccess<TSuccess>(Func<TSuccess, Task> onSuccess)`
  * Executes an asynchronous side effect, e.g., dispatching a status notification via `HttpClient`, when the result is a
    success. The current value is returned unchanged.

## `HttpClient` / Web API Helpers

### `HttpClient` Extensions

* `HttpClient.TryDeleteAsync(Uri? uri, CancellationToken cancellationToken = default)`
  * Tries to asynchronously execute `.DeleteAsync()`, returning a `TryAsync<HttpResponseMessage>`.
* `HttpClient.TryGetAsync(Uri? uri, HttpCompletionOption completionOption, CancellationToken cancellationToken = default)`
* `HttpClient.TryGetAsync(Uri? uri, CancellationToken cancellationToken = default)`
  * Tries to asynchronously execute `.GetAsync()`, returning a `TryAsync<HttpResponseMessage>`.
* `HttpClient.TryGetForJsonAsync(Uri? uri, JsonSerializerOptions? jsonSerializerOptions = null, CancellationToken cancellationToken = default)`
  * Tries to asynchronously execute `.GetAsync()` and deserialize the response body as JSON, returning a `TryAsync<DeserializedHttpResponseMessage<T>>`.
* `HttpClient.TryPostAsync(Uri? uri, HttpContent? content, CancellationToken cancellationToken = default)`
  * Tries to asynchronously execute `.PostAsync()`, returning a `TryAsync<HttpResponseMessage>`.
* `HttpClient.TryPostForJsonAsync(Uri? uri, HttpContent? content, JsonSerializerOptions? jsonSerializerOptions = null, CancellationToken cancellationToken = default)`
  * Tries to asynchronously execute `.PostAsync()` and deserialize the response body as JSON, returning a `TryAsync<DeserializedHttpResponseMessage<T>>`.
* `HttpClient.TryPutAsync(Uri? uri, HttpContent? content, CancellationToken cancellationToken = default)`
  * Tries to asynchronously execute `.PutAsync()`, returning a `TryAsync<HttpResponseMessage>`.
* `HttpClient.TryPutForJsonAsync<T>(Uri? uri, HttpContent? content, JsonSerializerOptions? jsonSerializerOptions = null, CancellationToken cancellationToken = default)`
  * Tries to asynchronously execute `.PutAsync()` and deserialize the response body as JSON, returning a `TryAsync<DeserializedHttpResponseMessage<T>>`.
* `HttpClient.TrySendAsync(HttpRequestMessage requestMessage, HttpCompletionOption completionOption, CancellationToken cancellationToken = default)`
* `HttpClient.TrySendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken = default)`
  * Tries to asynchronously execute `.SendAsync()`, returning a `TryAsync<HttpResponseMessage>`.
* `HttpClient.TrySendForJsonAsync<T>(HttpRequestMessage requestMessage, JsonSerializerOptions? jsonSerializerOptions = null, CancellationToken cancellationToken = default)`
  * Tries to asynchronously execute `.SendAsync()` and deserialize the response body as JSON, returning a `TryAsync<DeserializedHttpResponseMessage<T>>`.

### `HttpContent` Extensions

* `HttpContent.TryReadAsStringAsync(CancellationToken cancellationToken = default)`
  * Tries to asynchronously read the `HttpContent` as a `string`, returning a `TryAsync<string>`

### `HttpResponseMessage` Extensions

* `HttpResponseMessage.TryReadContentAsStringAsync(CancellationToken cancellationToken = default)`
  * Tries to asynchronously read the message's `.Content` as a `string`, returning a `TryAsync<string>`.
* `HttpResponseMessage.TryDeserializeContentAsJsonAsync<T>(JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) where T : notnull`
  * Tries to asynchronously read the message's `.Content` as a JSON `string` and deserialize it as `T`, returning a `TryAsync<T>`.
* `HttpResponseMessage.TryReadContentAsJsonAsync<T>(JsonSerializerOptions? jsonSerializerOptions = null, CancellationToken cancellationToken = default) where T : notnull`
  * Tries to asynchronously read the message's `.Content` as a JSON `string` and deserialize it as `T`, returning all components, `TryAsync<(HttpResponseMessage Message, Result<string> Content, Result<T> Body)>`.
  * This method is extremely useful in ASP.NET integration tests. It succinctly parses API responses and maintains full access to the `HttpResponseMessage` and any `Exception`s, supporting thorough testing of success and failure paths.

[LanguageExt]: https://github.com/louthy/language-ext

[LanguageExt license]: https://github.com/louthy/language-ext/blob/main/LICENSE.md

[nuget.org]: https://www.nuget.org/packages/Jds.LanguageExt.Extras/

[nullable-context]: https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references#nullable-contexts
