# HttpClientExtensions.TrySendAsync method (1 of 2)

Executes CancellationToken), with ResponseContentRead, catching exceptions.

```csharp
public static TryAsync<HttpResponseMessage> TrySendAsync(this HttpClient httpClient, 
    HttpRequestMessage requestMessage, CancellationToken cancellationToken = default)
```

| parameter | description |
| --- | --- |
| httpClient | This HttpClient. |
| requestMessage | A HttpRequestMessage to send. |
| cancellationToken | An asynchronous operation cancellation token. |

## Return Value

A TryAsync of HttpResponseMessage.

## See Also

* class [HttpClientExtensions](../HttpClientExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

---

# HttpClientExtensions.TrySendAsync method (2 of 2)

Executes CancellationToken), catching exceptions.

```csharp
public static TryAsync<HttpResponseMessage> TrySendAsync(this HttpClient httpClient, 
    HttpRequestMessage requestMessage, HttpCompletionOption completionOption, 
    CancellationToken cancellationToken = default)
```

| parameter | description |
| --- | --- |
| httpClient | This HttpClient. |
| requestMessage | A HttpRequestMessage to send. |
| completionOption | When the operation should complete. |
| cancellationToken | An asynchronous operation cancellation token. |

## Return Value

A TryAsync of HttpResponseMessage.

## See Also

* class [HttpClientExtensions](../HttpClientExtensions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

<!-- DO NOT EDIT: generated by xmldocmd for LanguageExt.Extras.dll -->