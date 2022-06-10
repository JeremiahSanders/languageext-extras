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

  /// <summary>
  ///   Execute a side effect and returns <paramref name="tryDelegate" /> result unchanged.
  /// </summary>
  /// <remarks>
  ///   Often used to perform logging.
  /// </remarks>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <param name="tryDelegate">A <see cref="Try{A}" />.</param>
  /// <param name="onSuccess">A side-effect for success cases.</param>
  /// <param name="onFailure">A side-effect for failure cases.</param>
  /// <returns>
  ///   <paramref name="tryDelegate" />
  /// </returns>
  public static Try<TSuccess> Tap<TSuccess>(
    this Try<TSuccess> tryDelegate,
    Action<TSuccess> onSuccess,
    Action<Exception> onFailure
  )
  {
    return Prelude.Try(() =>
    {
      var result = tryDelegate.Try();
      return result.Tap(onSuccess, onFailure).IfFail(Prelude.raise<TSuccess>);
    });
  }

  /// <summary>
  ///   Execute a side effect when <paramref name="tryDelegate" /> is a success and returns <paramref name="tryDelegate" />
  ///   result unchanged.
  /// </summary>
  /// <remarks>
  ///   Often used to perform logging.
  /// </remarks>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <param name="tryDelegate">A <see cref="Try{A}" />.</param>
  /// <param name="onSuccess">A side-effect for success cases.</param>
  /// <returns>
  ///   <paramref name="tryDelegate" />
  /// </returns>
  public static Try<TSuccess> TapSuccess<TSuccess>(
    this Try<TSuccess> tryDelegate,
    Action<TSuccess> onSuccess
  )
  {
    return Prelude.Try(() =>
    {
      var result = tryDelegate.Try();
      return result.TapSuccess(onSuccess).IfFail(Prelude.raise<TSuccess>);
    });
  }

  /// <summary>
  ///   Execute a side effect when <paramref name="tryDelegate" /> is a failure and returns
  ///   <paramref name="tryDelegate" /> result unchanged.
  /// </summary>
  /// <remarks>
  ///   Often used to perform logging.
  /// </remarks>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <param name="tryDelegate">A <see cref="Try{A}" />.</param>
  /// <param name="onFailure">A side-effect for failure cases.</param>
  /// <returns>
  ///   <paramref name="tryDelegate" />
  /// </returns>
  public static Try<TSuccess> TapFailure<TSuccess>(
    this Try<TSuccess> tryDelegate,
    Action<Exception> onFailure
  )
  {
    return Prelude.Try(() =>
    {
      var result = tryDelegate.Try();
      return result.TapFailure(onFailure).IfFail(Prelude.raise<TSuccess>);
    });
  }
}
