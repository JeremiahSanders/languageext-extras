using System.Diagnostics.Contracts;

using LanguageExt;

namespace Jds.LanguageExt.Extras;

/// <summary>
///   Methods extending <see cref="Try{A}" /> which filter results.
/// </summary>
public static class TryMonadExtensions
{
  /// <summary>
  ///   Returns a <see cref="Try{A}" /> which filters the result of <paramref name="tryDelegate" />. When
  ///   <paramref name="filter" /> returns <c>false</c>, <paramref name="onFalse" /> creates the failure result.
  /// </summary>
  /// <param name="tryDelegate">A <see cref="Try{A}" />.</param>
  /// <param name="filter">
  ///   A filtering function. If true, value continues as-is. If false, value is converted to an <see cref="Exception" />
  ///   with <paramref name="onFalse" />.
  /// </param>
  /// <param name="onFalse">
  ///   A function to convert <typeparamref name="TSuccess" /> to an <see cref="Exception" />.
  /// </param>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <returns>
  ///   A <see cref="Try{A}" />. If <paramref name="filter" /> returns true, current <typeparamref name="TSuccess" /> is
  ///   returned. If it returns false, the converted <see cref="Exception" /> is returned.
  /// </returns>
  [Pure]
  public static Try<TSuccess> Filter<TSuccess>(
    this Try<TSuccess> tryDelegate,
    Func<TSuccess, bool> filter,
    Func<TSuccess, Exception> onFalse
  )
  {
    return TryExtensions.Memo(() =>
    {
      var result = tryDelegate.Try();

      return result.IsFaulted ? result : result.Filter(filter, onFalse).IfFail(Prelude.raise<TSuccess>);
    });
  }
}
