# OptionExtensions.ToNonNullable&lt;T&gt; method (1 of 2)

Converts type parameter *T* from nullable to non-nullable.

```csharp
public static Option<T> ToNonNullable<T>(this Option<T?> option)
    where T : class
```

| parameter | description |
| --- | --- |
| T | A Some type. |
| option | An Option. |

## Return Value

The existing *option* value, with an updated type parameter.

## See Also

* class [OptionExtensions](../OptionExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

---

# OptionExtensions.ToNonNullable&lt;T&gt; method (2 of 2)

Converts type parameter *T* from nullable to non-nullable.

```csharp
public static Option<T> ToNonNullable<T>(this Option<T?> option)
    where T : struct
```

| parameter | description |
| --- | --- |
| T | A Some type. |
| option | An Option. |

## Return Value

The existing *option* value, with an updated type parameter.

## See Also

* class [OptionExtensions](../OptionExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

<!-- DO NOT EDIT: generated by xmldocmd for LanguageExt.Extras.dll -->
