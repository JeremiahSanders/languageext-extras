# LanguageExt.Extras

A collection of extension methods and helpers which extend the use of [`LanguageExt`][LanguageExt].

> This project is **not** affiliated with [LanguageExt][] and asserts no claims upon its intellectual property.

## Installation

Install LanguageExt.Extras as a NuGet package, via an IDE package manager, or using the command-line instructions at [nuget.org][].

## Use / API

### Using Statements

LanguageExt.Extras extends existing [LanguageExt][] types. As such, you'll need _both_ [LanguageExt][] and [LanguageExt.Extras][nuget.org] in scope.

All API examples shown below assume the following `using` statements:

```c#
using LanguageExt;
using LanguageExt.Common;
using Jds.LanguageExt.Extras;
```

## `Result<TSuccess>` Extensions

* `Result<TSuccess>.Bind<TSuccess, TNewSuccess>(Func<TSuccess, Result<TNewSuccess>> func)`
  * Executes `func`, which returns `Result<TNewSuccess>`, when the result is a success.
* `Result<TSuccess>.BindAsync<TSuccess, TNewSuccess>(Func<TSuccess, Task<Result<TNewSuccess>>> func)`
  * Executes `func`, which returns `Task<Result<TNewSuccess>>`, when the result is a success.
* `Result<TSuccess>.Tap<TSuccess>(Action<TSuccess> onSuccess, Action<Exception> onFailure)`
  * Executes a side effect, e.g., logging, based upon the state of the result. The current value is returned unchanged.
* `Result<TSuccess>.TapAsync<TSuccess>(Func<TSuccess, Task> onSuccess, Func<Exception, Task> onFailure)`
  * Executes an asynchronous side effect, e.g., dispatching a status notification via `HttpClient`, based upon the state of the result. The current value is returned unchanged.
* `Result<TSuccess>.TapFailure<TSuccess>(Action<Exception> onFailure)`
  * Executes a side effect, e.g., logging, when the result is a failure. The current value is returned unchanged.
* `Result<TSuccess>.TapFailureAsync<TSuccess>(Func<Exception, Task> onFailure)`
  * Executes an asynchronous side effect, e.g., dispatching a status notification via `HttpClient`, when the result is a failure. The current value is returned unchanged.
* `Result<TSuccess>.TapSuccess<TSuccess>(Action<TSuccess> onSuccess)`
  * Executes a side effect, e.g., logging, when the result is a success. The current value is returned unchanged.
* `Result<TSuccess>.TapSuccessAsync<TSuccess>(Func<TSuccess, Task> onSuccess)`
  * Executes an asynchronous side effect, e.g., dispatching a status notification via `HttpClient`, when the result is a success. The current value is returned unchanged.
* `Result<TSuccess>.ToEither<TSuccess>()`
  * Converts the `Result<TSuccess>` into an `Either<Exception, TSuccess>`.

[LanguageExt]: https://github.com/louthy/language-ext
[LanguageExt license]: https://github.com/louthy/language-ext/blob/main/LICENSE.md
[nuget.org]: https://www.nuget.org/packages/Jds.LanguageExt.Extras/
