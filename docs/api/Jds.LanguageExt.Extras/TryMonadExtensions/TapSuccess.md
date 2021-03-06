# TryMonadExtensions.TapSuccess&lt;TSuccess&gt; method

Execute a side effect when *tryDelegate* is a success and returns *tryDelegate* result unchanged.

```csharp
public static Try<TSuccess> TapSuccess<TSuccess>(this Try<TSuccess> tryDelegate, 
    Action<TSuccess> onSuccess)
```

| parameter | description |
| --- | --- |
| TSuccess | A success type. |
| tryDelegate | A Try. |
| onSuccess | A side-effect for success cases. |

## Return Value

*tryDelegate*

## Remarks

Often used to perform logging.

## See Also

* class [TryMonadExtensions](../TryMonadExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

<!-- DO NOT EDIT: generated by xmldocmd for LanguageExt.Extras.dll -->
