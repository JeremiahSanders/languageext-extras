# Experimental Features

The following APIs are not yet part of the public API. They may be removed or completed and moved to the public API.

## `HttpClient` / Web API Helpers

### `string` Extensions

* `string.TryDeserializeJson<T>(JsonSerializerOptions? options = null) where T : notnull`
  * Attempts to deserialize the `string` as a `T`, returning `Try<T>`.
