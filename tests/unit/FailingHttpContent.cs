using System.Net;

namespace Jds.LanguageExt.Extras.Tests.Unit;

/// <summary>
///   A <see cref="HttpContent" /> that throws when attempting to serialize to a stream.
/// </summary>
internal class FailingHttpContent : HttpContent
{
  protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context)
  {
    throw new NotSupportedException("Intentionally not supported. Intended to facilitate testing.");
  }

  protected override bool TryComputeLength(out long length)
  {
    throw new NotSupportedException("Intentionally not supported. Intended to facilitate testing.");
  }
}
