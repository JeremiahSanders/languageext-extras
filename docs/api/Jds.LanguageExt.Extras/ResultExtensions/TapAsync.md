# ResultExtensions.TapAsync&lt;TSuccess&gt; method

Execute an asynchronous side effect and returns *result* unchanged.

```csharp
public static Task<Result<TSuccess>> TapAsync<TSuccess>(this Result<TSuccess> result, 
    Func<TSuccess, Task> onSuccess, Func<Exception, Task> onFailure)
```

| parameter | description |
| --- | --- |
| TSuccess | A success type. |
| result | A Result. |
| onSuccess | A side-effect for success cases. |
| onFailure | A side-effect for failure cases. |

## Return Value

*result*

## Remarks

Often used to dispatch an asynchronous status message, e.g., a workflow heartbeat.

## See Also

* class [ResultExtensions](../ResultExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

<!-- DO NOT EDIT: generated by xmldocmd for LanguageExt.Extras.dll -->
