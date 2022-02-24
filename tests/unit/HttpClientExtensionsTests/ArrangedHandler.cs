namespace Jds.LanguageExt.Extras.Tests.Unit.HttpClientExtensionsTests;

internal record ArrangedHandler(Func<HttpRequestMessage, Task<bool>> DoesHandleMessage,
  Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> HandleMessage);
