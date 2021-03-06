# EitherExtensions.TapRightAsync&lt;TLeft,TRight&gt; method

Execute an asynchronous side effect when *either* is a Right and returns *either* unchanged.

```csharp
public static Task<Either<TLeft, TRight>> TapRightAsync<TLeft, TRight>(
    this Either<TLeft, TRight> either, Func<TRight, Task> onSuccess)
```

| parameter | description |
| --- | --- |
| TLeft | A left type. |
| TRight | A right type. |
| either | An Either. |
| onSuccess | A side-effect for Right cases. |

## Return Value

*either*

## Remarks

Often used to dispatch an asynchronous status message, e.g., a workflow heartbeat.

## See Also

* class [EitherExtensions](../EitherExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

<!-- DO NOT EDIT: generated by xmldocmd for LanguageExt.Extras.dll -->
