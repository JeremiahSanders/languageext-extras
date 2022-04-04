# ResultExtensions.MapFailureAsync&lt;TSuccess,TFailure&gt; method

Asynchronously converts an existing Exception into a new type, using *func*, if *result* is a failure.

```csharp
public static Task<Result<TSuccess>> MapFailureAsync<TSuccess, TFailure>(
    this Result<TSuccess> result, Func<Exception, Task<TFailure>> func)
    where TFailure : Exception
```

| parameter | description |
| --- | --- |
| TSuccess | A success type. |
| TFailure | A new Exception. |
| result | A Result. |
| func | A Func |

## Return Value

A Result.

## See Also

* class [ResultExtensions](../ResultExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

<!-- DO NOT EDIT: generated by xmldocmd for LanguageExt.Extras.dll -->