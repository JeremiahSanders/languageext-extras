namespace Jds.LanguageExt.Extras.Tests.Unit.HttpClientExtensionsTests;

internal class ArrangedHttpMessageHandlerBuilder
{
  private ArrangedHttpMessageHandlerBuilder()
  {
    MessageHandlers = new Dictionary<string, ArrangedHandler>();
  }

  private Dictionary<string, ArrangedHandler> MessageHandlers { get; }

  public static ArrangedHttpMessageHandlerBuilder CreateInstance()
  {
    return new ArrangedHttpMessageHandlerBuilder();
  }

  public ArrangedHttpMessageHandlerBuilder WithArrangedHandler(string id, ArrangedHandler handler)
  {
    MessageHandlers[id] = handler;

    return this;
  }

  public ArrangedHttpMessageHandlerBuilder WithArrangedHandler(ArrangedHandler handler)
  {
    return WithArrangedHandler(Guid.NewGuid().ToString(), handler);
  }

  public ArrangedHttpMessageHandlerBuilder WithArrangedHandler(
    string id,
    Func<ArrangedHandlerBuilder, ArrangedHandlerBuilder> fluentArrangement)
  {
    return WithArrangedHandler(id, fluentArrangement(ArrangedHandlerBuilder.CreateInstance()).Build());
  }

  public ArrangedHttpMessageHandlerBuilder WithArrangedHandler(
    Func<ArrangedHandlerBuilder, ArrangedHandlerBuilder> fluentArrangement)
  {
    return WithArrangedHandler(Guid.NewGuid().ToString(), fluentArrangement);
  }

  public ArrangedHttpMessageHandler Build()
  {
    var handler = new ArrangedHttpMessageHandler();

    foreach (var messageHandler in MessageHandlers)
    {
      handler.Add(messageHandler.Key, messageHandler.Value);
    }

    return handler;
  }

  public HttpClient CreateHttpClient()
  {
    return new HttpClient(Build(), true);
  }
}
