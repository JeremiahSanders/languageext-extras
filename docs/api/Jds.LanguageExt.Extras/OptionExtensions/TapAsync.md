# OptionExtensions.TapAsync&lt;T&gt; method

Executes an asynchronous side effect, based upon the state of *option*, and returns *option* unchanged.

```csharp
public static Task<Option<T>> TapAsync<T>(this Option<T> option, Func<T, Task> ifSome, 
    Func<Task> ifNone)
```

| parameter | description |
| --- | --- |
| T | A Some type. |
| option | An Option |
| ifSome | A Func to execute if *option* is Some. |
| ifNone | A Func to execute if *option* is None. |

## Return Value

*option*

## See Also

* class [OptionExtensions](../OptionExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

<!-- DO NOT EDIT: generated by xmldocmd for LanguageExt.Extras.dll -->