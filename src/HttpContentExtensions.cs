using LanguageExt;

namespace Jds.LanguageExt.Extras;

/// <summary>
///   Methods extending <see cref="HttpContent" />.
/// </summary>
public static class HttpContentExtensions
{
  /// <summary>
  ///   Execute <see cref="HttpContent.ReadAsStringAsync()" />, catching exceptions.
  /// </summary>
  /// <param name="content">This <see cref="HttpContent" />.</param>
  /// <param name="cancellationToken">An asynchronous operation cancellation token.</param>
  /// <returns>A <see cref="TryAsync{A}" /> of <see cref="string" />.</returns>
  public static TryAsync<string> TryReadAsStringAsync(this HttpContent content,
    CancellationToken cancellationToken = default)
  {
    return Prelude.TryAsync(() => content.ReadAsStringAsync(cancellationToken));
  }
}
