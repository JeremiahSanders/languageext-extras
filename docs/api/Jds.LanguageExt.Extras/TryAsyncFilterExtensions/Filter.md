# TryAsyncFilterExtensions.Filter&lt;TSuccess&gt; method (1 of 2)

Returns a TryAsync which filters the result of *tryAsync*. When *filter* returns `false`, *onFalse* creates the failure result.

```csharp
public static TryAsync<TSuccess> Filter<TSuccess>(this TryAsync<TSuccess> tryAsync, 
    Func<TSuccess, bool> filter, Func<TSuccess, Exception> onFalse)
```

| parameter | description |
| --- | --- |
| TSuccess | A success type. |
| tryAsync | A TryAsync. |
| filter | A filtering function. If true, value continues as-is. If false, value is converted to an Exception with *onFalse*. |
| onFalse | A function to convert *TSuccess* to an Exception. |

## Return Value

A TryAsync. If *filter* returns true, current *TSuccess* is returned. If it returns false, the converted Exception is returned.

## See Also

* class [TryAsyncFilterExtensions](../TryAsyncFilterExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

---

# TryAsyncFilterExtensions.Filter&lt;TSuccess&gt; method (2 of 2)

Returns a TryAsync which filters the result of *tryAsync*. When *filter* returns `false`, *onFalse* creates the failure result.

```csharp
public static TryAsync<TSuccess> Filter<TSuccess>(this TryAsync<TSuccess> tryAsync, 
    Func<TSuccess, Task<bool>> filter, Func<TSuccess, Exception> onFalse)
```

| parameter | description |
| --- | --- |
| TSuccess | A success type. |
| tryAsync | A TryAsync. |
| filter | A filtering function. If true, value continues as-is. If false, value is converted to an Exception with *onFalse*. |
| onFalse | A function to convert *TSuccess* to an Exception. |

## Return Value

A TryAsync. If *filter* returns true, current *TSuccess* is returned. If it returns false, the converted Exception is returned.

## See Also

* class [TryAsyncFilterExtensions](../TryAsyncFilterExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

<!-- DO NOT EDIT: generated by xmldocmd for LanguageExt.Extras.dll -->