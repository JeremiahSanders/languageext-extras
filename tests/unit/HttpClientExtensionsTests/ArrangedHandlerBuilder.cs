using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

using LanguageExt;

namespace Jds.LanguageExt.Extras.Tests.Unit.HttpClientExtensionsTests;

internal class ArrangedHandlerBuilder
{
  private ArrangedHandlerBuilder()
  {
    AcceptRules = new List<Func<HttpRequestMessage, Task<bool>>>();
    ResponseHandler = (_, _) => throw new InvalidOperationException("Response not configured.");
  }

  private List<Func<HttpRequestMessage, Task<bool>>> AcceptRules { get; }

  private Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> ResponseHandler { get; set; }

  public static ArrangedHandlerBuilder CreateInstance()
  {
    return new ArrangedHandlerBuilder();
  }

  /// <summary>
  ///   Accepts all requests.
  /// </summary>
  /// <returns>This instance.</returns>
  public ArrangedHandlerBuilder AcceptAll()
  {
    AcceptRules.Add(_ => true.AsTask());

    return this;
  }

  /// <summary>
  ///   Accepts all requests matching <paramref name="httpMethod" />.
  /// </summary>
  /// <param name="httpMethod">An accepted <see cref="HttpMethod" />.</param>
  /// <returns>This instance.</returns>
  public ArrangedHandlerBuilder AcceptMethod(HttpMethod httpMethod)
  {
    return WithAcceptRule(message => (message.Method == httpMethod).AsTask());
  }

  public ArrangedHandlerBuilder AcceptRoute(Func<HttpMethod, Uri?, bool> matcher)
  {
    return WithAcceptRule(message => matcher(message.Method, message.RequestUri).AsTask());
  }

  public ArrangedHandlerBuilder AcceptRoute(HttpMethod httpMethod, Uri uri)
  {
    return AcceptRoute((requestMethod, requestUri) => requestMethod == httpMethod && requestUri == uri);
  }

  public ArrangedHandlerBuilder AcceptUri(Func<Uri?, bool> matcher)
  {
    return WithAcceptRule(message => matcher(message.RequestUri).AsTask());
  }

  public ArrangedHandlerBuilder AcceptUri(Uri uriToAccept)
  {
    return AcceptUri(uri => uri == uriToAccept);
  }

  public ArrangedHandlerBuilder WithAcceptRule(Func<HttpRequestMessage, Task<bool>> acceptRule)
  {
    AcceptRules.Add(acceptRule);

    return this;
  }

  public ArrangedHandlerBuilder RespondContentJson<T>(HttpStatusCode httpStatusCode, T bodyObject,
    JsonSerializerOptions? jsonSerializerOptions = null)
  {
    SetResponseHandler((_, _) =>
      new HttpResponseMessage(httpStatusCode)
      {
        Content = new StringContent(JsonSerializer.Serialize(bodyObject, jsonSerializerOptions), Encoding.UTF8,
          MediaTypeNames.Application.Json)
      }.AsTask()
    );

    return this;
  }

  public ArrangedHandlerBuilder RespondContent(HttpStatusCode httpStatusCode, HttpContent httpContent)
  {
    SetResponseHandler((_, _) =>
      new HttpResponseMessage(httpStatusCode) { Content = httpContent }.AsTask()
    );

    return this;
  }

  public ArrangedHandlerBuilder RespondStatusCode(HttpStatusCode httpStatusCode)
  {
    SetResponseHandler((_, _) =>
      new HttpResponseMessage(httpStatusCode).AsTask()
    );

    return this;
  }

  public ArrangedHandlerBuilder SetResponseHandler(
    Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> returnHandler
  )
  {
    ResponseHandler = returnHandler;

    return this;
  }

  public ArrangedHandler Build()
  {
    return new ArrangedHandler(CreateDoesHandleMessage(), ResponseHandler);
  }

  private Func<HttpRequestMessage, Task<bool>> CreateDoesHandleMessage()
  {
    async Task<bool> Handler(HttpRequestMessage message)
    {
      foreach (var acceptRule in AcceptRules)
      {
        if (await acceptRule(message))
        {
          return true;
        }
      }

      return false;
    }

    return Handler;
  }
}
