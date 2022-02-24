using LanguageExt;
using LanguageExt.Common;

namespace Jds.LanguageExt.Extras;

public static class HttpContentExtensions
{
  public static Task<Result<string>> TryReadAsStringAsync(this HttpContent content,
    CancellationToken cancellationToken = default)
  {
    return Prelude.TryAsync(async () => await content.ReadAsStringAsync(cancellationToken)).Try();
  }
}
