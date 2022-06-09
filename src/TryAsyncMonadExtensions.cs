using System.Diagnostics.Contracts;

using LanguageExt;

namespace Jds.LanguageExt.Extras;

/// <summary>
///   Methods extending <see cref="TryAsync{A}" /> which filter results.
/// </summary>
public static class TryAsyncMonadExtensions
{
  /// <summary>
  ///   Returns a <see cref="TryAsync{A}" /> which filters the result of <paramref name="tryAsync" />. When
  ///   <paramref name="filter" /> returns <c>false</c>, <paramref name="onFalse" /> creates the failure result.
  /// </summary>
  /// <param name="tryAsync">A <see cref="TryAsync{A}" />.</param>
  /// <param name="filter">
  ///   A filtering function. If true, value continues as-is. If false, value is converted to an <see cref="Exception" />
  ///   with <paramref name="onFalse" />.
  /// </param>
  /// <param name="onFalse">
  ///   A function to convert <typeparamref name="TSuccess" /> to an <see cref="Exception" />.
  /// </param>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <returns>
  ///   A <see cref="TryAsync{A}" />. If <paramref name="filter" /> returns true, current <typeparamref name="TSuccess" /> is
  ///   returned. If it returns false, the converted <see cref="Exception" /> is returned.
  /// </returns>
  [Pure]
  public static TryAsync<TSuccess> Filter<TSuccess>(
    this TryAsync<TSuccess> tryAsync,
    Func<TSuccess, bool> filter,
    Func<TSuccess, Exception> onFalse
  )
  {
    return TryAsyncExtensions.Memo(async () =>
    {
      var result = await tryAsync.Try().ConfigureAwait(false);

      return result.IsFaulted ? result : result.Filter(filter, onFalse).IfFail(Prelude.raise<TSuccess>);
    });
  }

  /// <summary>
  ///   Returns a <see cref="TryAsync{A}" /> which filters the result of <paramref name="tryAsync" />. When
  ///   <paramref name="filter" /> returns <c>false</c>, <paramref name="onFalse" /> creates the failure result.
  /// </summary>
  /// <param name="tryAsync">A <see cref="TryAsync{A}" />.</param>
  /// <param name="filter">
  ///   A filtering function. If true, value continues as-is. If false, value is converted to an <see cref="Exception" />
  ///   with <paramref name="onFalse" />.
  /// </param>
  /// <param name="onFalse">
  ///   A function to convert <typeparamref name="TSuccess" /> to an <see cref="Exception" />.
  /// </param>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <returns>
  ///   A <see cref="TryAsync{A}" />. If <paramref name="filter" /> returns true, current <typeparamref name="TSuccess" /> is
  ///   returned. If it returns false, the converted <see cref="Exception" /> is returned.
  /// </returns>
  [Pure]
  public static TryAsync<TSuccess> Filter<TSuccess>(
    this TryAsync<TSuccess> tryAsync,
    Func<TSuccess, Task<bool>> filter,
    Func<TSuccess, Exception> onFalse
  )
  {
    return TryAsyncExtensions.Memo(async () =>
    {
      var result = await tryAsync.Try().ConfigureAwait(false);

      return result.IsFaulted
        ? result
        : (await result.FilterAsync(filter, onFalse).ConfigureAwait(false)).IfFail(Prelude.raise<TSuccess>);
    });
  }
}
