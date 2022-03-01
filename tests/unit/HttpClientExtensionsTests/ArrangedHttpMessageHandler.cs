namespace Jds.LanguageExt.Extras.Tests.Unit.HttpClientExtensionsTests;

internal class ArrangedHttpMessageHandler : HttpMessageHandler
{
  public ArrangedHttpMessageHandler()
  {
    ArrangedHandlers = new Dictionary<string, ArrangedHandler>();
  }

  private Dictionary<string, ArrangedHandler> ArrangedHandlers { get; }

  public void Add(string id, ArrangedHandler handler)
  {
    ArrangedHandlers[id] = handler;
  }

  public void Add(ArrangedHandler handler)
  {
    Add(
      Guid.NewGuid().ToString(),
      handler
    );
  }

  /// <summary>
  ///   Removes a handler, identified by <paramref name="id" />.
  /// </summary>
  /// <param name="id">A handler identifier.</param>
  /// <returns>
  ///   true if the element is successfully found and removed; otherwise, false. This method returns false if no
  ///   handler was previously registered for the <paramref name="id" />.
  /// </returns>
  public bool Remove(string id)
  {
    return ArrangedHandlers.Remove(id);
  }

  public void Clear()
  {
    ArrangedHandlers.Clear();
  }

  protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
    CancellationToken cancellationToken)
  {
    var handler = await TryGetHandler(request) ??
                  throw new InvalidOperationException(
                    $"No handlers registered for request. {request.Method} {request.RequestUri}");

    return await handler(request, cancellationToken);
  }

  private async Task<Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>>?> TryGetHandler(
    HttpRequestMessage request)
  {
    foreach (var arrangedHandler in ArrangedHandlers)
    {
      if (await arrangedHandler.Value.DoesHandleMessage(request))
      {
        return arrangedHandler.Value.HandleMessage;
      }
    }

    return null;
  }
}
