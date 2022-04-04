# ResultExtensions.FilterAsync&lt;TSuccess&gt; method (1 of 2)

Filters right values by executing *filter*. If true, returns the current *TSuccess* value. If false, executes *onFalse* to convert the filtered *TSuccess* to an Exception.

```csharp
public static Task<Result<TSuccess>> FilterAsync<TSuccess>(this Result<TSuccess> result, 
    Func<TSuccess, Task<bool>> filter, Func<TSuccess, Exception> onFalse)
```

| parameter | description |
| --- | --- |
| TSuccess | A success type. |
| result | A Result. |
| filter | A filtering function. If true, value continues as-is. If false, value is converted to an Exception with *onFalse*. |
| onFalse | A function to convert *TSuccess* to an Exception. |

## Return Value

A Result. If *filter* returns true, current *TSuccess* is returned. If it returns false, the converted Exception is returned.

## See Also

* class [ResultExtensions](../ResultExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

---

# ResultExtensions.FilterAsync&lt;TSuccess&gt; method (2 of 2)

Filters right values by executing *filter*. If true, returns the current *TSuccess* value. If false, executes *onFalse* to convert the filtered *TSuccess* to an Exception.

```csharp
public static Task<Result<TSuccess>> FilterAsync<TSuccess>(this Result<TSuccess> result, 
    Func<TSuccess, Task<bool>> filter, Func<TSuccess, Task<Exception>> onFalse)
```

| parameter | description |
| --- | --- |
| TSuccess | A success type. |
| result | A Result. |
| filter | A filtering function. If true, value continues as-is. If false, value is converted to an Exception with *onFalse*. |
| onFalse | A function to convert *TSuccess* to an Exception. |

## Return Value

A Result. If *filter* returns true, current *TSuccess* is returned. If it returns false, the converted Exception is returned.

## See Also

* class [ResultExtensions](../ResultExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

<!-- DO NOT EDIT: generated by xmldocmd for LanguageExt.Extras.dll -->