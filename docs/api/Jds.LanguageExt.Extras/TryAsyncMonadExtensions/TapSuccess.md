# TryAsyncMonadExtensions.TapSuccess&lt;TSuccess&gt; method (1 of 2)

Execute a side effect when *tryAsync* succeeds and returns *tryAsync* result unchanged.

```csharp
public static TryAsync<TSuccess> TapSuccess<TSuccess>(this TryAsync<TSuccess> tryAsync, 
    Action<TSuccess> onSuccess)
```

| parameter | description |
| --- | --- |
| TSuccess | A success type. |
| tryAsync | A TryAsync. |
| onSuccess | A side-effect for success cases. |

## Return Value

*tryAsync*

## Remarks

Often used to perform logging.

## See Also

* class [TryAsyncMonadExtensions](../TryAsyncMonadExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

---

# TryAsyncMonadExtensions.TapSuccess&lt;TSuccess&gt; method (2 of 2)

Execute a side effect when *tryAsync* succeeds and returns *tryAsync* result unchanged.

```csharp
public static TryAsync<TSuccess> TapSuccess<TSuccess>(this TryAsync<TSuccess> tryAsync, 
    Func<TSuccess, Task> onSuccess)
```

| parameter | description |
| --- | --- |
| TSuccess | A success type. |
| tryAsync | A TryAsync. |
| onSuccess | A side-effect for success cases. |

## Return Value

*tryAsync*

## Remarks

Often used to perform logging.

## See Also

* class [TryAsyncMonadExtensions](../TryAsyncMonadExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

<!-- DO NOT EDIT: generated by xmldocmd for LanguageExt.Extras.dll -->
