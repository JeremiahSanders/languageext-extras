# HttpCompositions.TrySendForJsonAsync&lt;T&gt; method

Asynchronously sends the *httpRequestMessage* and attempts to parse the response Content as a String and deserialize it as JSON, catching exceptions.

```csharp
public static TryAsync<DeserializedHttpResponseMessage<T>> TrySendForJsonAsync<T>(
    this HttpClient httpClient, HttpRequestMessage httpRequestMessage, 
    JsonSerializerOptions? jsonSerializerOptions = null, 
    CancellationToken cancellationToken = default)
```

| parameter | description |
| --- | --- |
| T | A response Content deserialization type. |
| httpClient | This HttpClient. |
| httpRequestMessage | A HttpRequestMessage. |
| jsonSerializerOptions | Optional JsonSerializerOptions. |
| cancellationToken | An asynchronous operation cancellation token. |

## Return Value

A TryAsync of [`DeserializedHttpResponseMessage`](../DeserializedHttpResponseMessage-1.md).

## See Also

* record [DeserializedHttpResponseMessage&lt;TBody&gt;](../DeserializedHttpResponseMessage-1.md)
* class [HttpCompositions](../HttpCompositions.md)
* namespace [Jds.LanguageExt.Extras](../../LanguageExt.Extras.md)

<!-- DO NOT EDIT: generated by xmldocmd for LanguageExt.Extras.dll -->