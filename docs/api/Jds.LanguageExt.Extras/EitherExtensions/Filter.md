# EitherExtensions.Filter&lt;TLeft,TRight&gt; method

Filters right values by executing *filter*. If true, returns the current *TRight* value. If false, executes *onFalse* to convert the filtered *TRight* to a *TLeft*.

```csharp
public static Either<TLeft, TRight> Filter<TLeft, TRight>(this Either<TLeft, TRight> either, 
    Func<TRight, bool> filter, Func<TRight, TLeft> onFalse)
```

| parameter | description |
| --- | --- |
| TLeft | A left type. |
| TRight | A right type. |
| either | An Either. |
| filter | A filtering function. If true, value continues as-is. If false, value is converted to a *TLeft*. |
| onFalse | A function to convert *TRight* to a *TLeft*. |

## Return Value

If *filter* returns true, current *TRight* is returned. If it returns false, the converted *TLeft* is returned.

## Remarks

This overload of Boolean}) avoids returning Bottom.

## See Also

* class [EitherExtensions](../EitherExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

<!-- DO NOT EDIT: generated by xmldocmd for LanguageExt.Extras.dll -->