# HttpClientExtensions.TryPostAsync method

Executes CancellationToken), catching exceptions.

```csharp
public static TryAsync<HttpResponseMessage> TryPostAsync(this HttpClient httpClient, Uri? uri, 
    HttpContent? content, CancellationToken cancellationToken = default)
```

| parameter | description |
| --- | --- |
| httpClient | This HttpClient. |
| uri | A Uri the request is sent to. |
| content | A HttpContent sent to the server. |
| cancellationToken | An asynchronous operation cancellation token. |

## Return Value

A TryAsync of HttpResponseMessage.

## See Also

* class [HttpClientExtensions](../HttpClientExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

<!-- DO NOT EDIT: generated by xmldocmd for LanguageExt.Extras.dll -->