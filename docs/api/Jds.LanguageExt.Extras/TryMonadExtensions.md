# TryMonadExtensions class

Methods extending Try which filter results.

```csharp
public static class TryMonadExtensions
```

## Public Members

| name | description |
| --- | --- |
| static [Filter&lt;TSuccess&gt;](TryMonadExtensions/Filter.md)(…) | Returns a Try which filters the result of *tryDelegate*. When *filter* returns `false`, *onFalse* creates the failure result. |

## See Also

* namespace [Jds.LanguageExt.Extras](../LanguageExt.Extras.md)
* [TryMonadExtensions.cs](https://github.com/JeremiahSanders/languageext-extras/tree/main/src/TryMonadExtensions.cs)

<!-- DO NOT EDIT: generated by xmldocmd for LanguageExt.Extras.dll -->