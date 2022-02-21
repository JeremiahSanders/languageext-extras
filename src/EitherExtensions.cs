using LanguageExt;
using LanguageExt.Common;

namespace Jds.LanguageExt.Extras;

/// <summary>
///   Extension methods for <see cref="Either{L,R}" />.
/// </summary>
public static class EitherExtensions
{
  /// <summary>
  ///   Filters right values by executing <paramref name="filter" />. If true, returns the current
  ///   <typeparamref name="TRight" /> value. If false, executes <paramref name="onFalse" /> to convert the filtered
  ///   <typeparamref name="TRight" /> to a <typeparamref name="TLeft" />.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     This overload of <see cref="Either{L,R}.Filter" /> avoids returning <see cref="Either{L,R}.Bottom" />.
  ///   </para>
  /// </remarks>
  /// <typeparam name="TLeft">A left type.</typeparam>
  /// <typeparam name="TRight">A right type.</typeparam>
  /// <param name="either">An <see cref="Either{L,R}" />.</param>
  /// <param name="filter">
  ///   A filtering function. If true, value continues as-is. If false, value is converted to a
  ///   <typeparamref name="TLeft" />.
  /// </param>
  /// <param name="onFalse">
  ///   A function to convert <typeparamref name="TRight" /> to a <typeparamref name="TLeft" />.
  /// </param>
  /// <returns>
  ///   If <paramref name="filter" /> returns true, current <typeparamref name="TRight" /> is returned. If it returns
  ///   false, the converted <typeparamref name="TLeft" /> is returned.
  /// </returns>
  public static Either<TLeft, TRight> Filter<TLeft, TRight>(this Either<TLeft, TRight> either,
    Func<TRight, bool> filter,
    Func<TRight, TLeft> onFalse
  )
  {
    return either.Bind(right =>
      filter(right)
        ? Prelude.Right<TLeft, TRight>(right)
        : Prelude.Left<TLeft, TRight>(value: onFalse(right)));
  }

  /// <summary>
  ///   Filters right values by executing <paramref name="filter" />. If true, returns the current
  ///   <typeparamref name="TRight" /> value. If false, executes <paramref name="onFalse" /> to convert the filtered
  ///   <typeparamref name="TRight" /> to a <typeparamref name="TLeft" />.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     This overload of <see cref="Either{L,R}.Filter" /> avoids returning <see cref="Either{L,R}.Bottom" />.
  ///   </para>
  /// </remarks>
  /// <typeparam name="TLeft">A left type.</typeparam>
  /// <typeparam name="TRight">A right type.</typeparam>
  /// <param name="either">An <see cref="Either{L,R}" />.</param>
  /// <param name="filter">
  ///   A filtering function. If true, value continues as-is. If false, value is converted to a
  ///   <typeparamref name="TLeft" />.
  /// </param>
  /// <param name="onFalse">
  ///   A function to convert <typeparamref name="TRight" /> to a <typeparamref name="TLeft" />.
  /// </param>
  /// <returns>
  ///   If <paramref name="filter" /> returns true, current <typeparamref name="TRight" /> is returned. If it returns
  ///   false, the converted <typeparamref name="TLeft" /> is returned.
  /// </returns>
  public static Task<Either<TLeft, TRight>> FilterAsync<TLeft, TRight>(this Either<TLeft, TRight> either,
    Func<TRight, Task<bool>> filter,
    Func<TRight, TLeft> onFalse
  )
  {
    return either.BindAsync(async right =>
      await filter(right)
        ? Prelude.Right<TLeft, TRight>(right)
        : Prelude.Left<TLeft, TRight>(value: onFalse(right)));
  }

  /// <summary>
  ///   Execute <paramref name="filter" />. If true, returns the current <typeparamref name="TRight" /> value. If false,
  ///   executes <paramref name="onFalse" /> to convert the filtered <typeparamref name="TRight" /> to a
  ///   <typeparamref name="TLeft" />.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     This overload of <see cref="Either{L,R}.Filter" /> avoids returning <see cref="Either{L,R}.Bottom" />.
  ///   </para>
  /// </remarks>
  /// <typeparam name="TLeft">A left type.</typeparam>
  /// <typeparam name="TRight">A right type.</typeparam>
  /// <param name="either">An <see cref="Either{L,R}" />.</param>
  /// <param name="filter">
  ///   A filtering function. If true, value continues as-is. If false, value is converted to a
  ///   <typeparamref name="TLeft" />.
  /// </param>
  /// <param name="onFalse">
  ///   A function to convert <typeparamref name="TRight" /> to a <typeparamref name="TLeft" />.
  /// </param>
  /// <returns>
  ///   If <paramref name="filter" /> returns true, current <typeparamref name="TRight" /> is returned. If it returns
  ///   false, the converted <typeparamref name="TLeft" /> is returned.
  /// </returns>
  public static Task<Either<TLeft, TRight>> FilterAsync<TLeft, TRight>(this Either<TLeft, TRight> either,
    Func<TRight, Task<bool>> filter,
    Func<TRight, Task<TLeft>> onFalse
  )
  {
    return either.BindAsync(async right =>
      await filter(right)
        ? Prelude.Right<TLeft, TRight>(right)
        : Prelude.Left<TLeft, TRight>(value: await onFalse(right)));
  }

  /// <summary>
  ///   Execute a side effect and returns <paramref name="either" /> unchanged.
  /// </summary>
  /// <remarks>
  ///   Often used to perform logging.
  /// </remarks>
  /// <typeparam name="TLeft">A left type.</typeparam>
  /// <typeparam name="TRight">A right type.</typeparam>
  /// <param name="either">An <see cref="Either{L,R}" />.</param>
  /// <param name="onRight">A side-effect for right cases.</param>
  /// <param name="onLeft">A side-effect for left cases.</param>
  /// <returns>
  ///   <paramref name="either" />
  /// </returns>
  public static Either<TLeft, TRight> Tap<TLeft, TRight>(this Either<TLeft, TRight> either, Action<TRight> onRight,
    Action<TLeft> onLeft)
  {
    return either.Match(right =>
    {
      onRight(right);
      return either;
    }, left =>
    {
      onLeft(left);
      return either;
    });
  }

  /// <summary>
  ///   Execute an asynchronous side effect and returns <paramref name="either" /> unchanged.
  /// </summary>
  /// <remarks>
  ///   Often used to dispatch an asynchronous status message, e.g., a workflow heartbeat.
  /// </remarks>
  /// <typeparam name="TLeft">A left type.</typeparam>
  /// <typeparam name="TRight">A right type.</typeparam>
  /// <param name="either">An <see cref="Either{L,R}" />.</param>
  /// <param name="onSuccess">A side-effect for Right cases.</param>
  /// <param name="onFailure">A side-effect for Left cases.</param>
  /// <returns>
  ///   <paramref name="either" />
  /// </returns>
  public static Task<Either<TLeft, TRight>> TapAsync<TLeft, TRight>(this Either<TLeft, TRight> either,
    Func<TRight, Task> onSuccess,
    Func<TLeft, Task> onFailure
  )
  {
    return either.Match(async right =>
    {
      await onSuccess(right);
      return either;
    }, async left =>
    {
      await onFailure(left);
      return either;
    });
  }

  /// <summary>
  ///   Execute a side effect when <paramref name="either" /> is a Left and returns
  ///   <paramref name="either" /> unchanged.
  /// </summary>
  /// <remarks>
  ///   Often used to perform logging.
  /// </remarks>
  /// <typeparam name="TLeft">A left type.</typeparam>
  /// <typeparam name="TRight">A right type.</typeparam>
  /// <param name="either">An <see cref="Either{L,R}" />.</param>
  /// <param name="onFailure">A side-effect for Left cases.</param>
  /// <returns>
  ///   <paramref name="either" />
  /// </returns>
  public static Either<TLeft, TRight> TapLeft<TLeft, TRight>(this Either<TLeft, TRight> either,
    Action<TLeft> onFailure)
  {
    return either.Match(right => either, left =>
    {
      onFailure(left);
      return either;
    });
  }

  /// <summary>
  ///   Execute an asynchronous side effect when <paramref name="either" /> is a Left and returns
  ///   <paramref name="either" /> unchanged.
  /// </summary>
  /// <remarks>
  ///   Often used to dispatch an asynchronous status message, e.g., a workflow heartbeat.
  /// </remarks>
  /// <typeparam name="TLeft">A left type.</typeparam>
  /// <typeparam name="TRight">A right type.</typeparam>
  /// <param name="either">An <see cref="Either{L,R}" />.</param>
  /// <param name="onFailure">A side-effect for Left cases.</param>
  /// <returns>
  ///   <paramref name="either" />
  /// </returns>
  public static Task<Either<TLeft, TRight>> TapLeftAsync<TLeft, TRight>(this Either<TLeft, TRight> either,
    Func<TLeft, Task> onFailure)
  {
    return either.Match(right => either.AsTask(), async left =>
    {
      await onFailure(left);
      return either;
    });
  }

  /// <summary>
  ///   Execute a side effect when <paramref name="either" /> is a Right and returns <paramref name="either" /> unchanged.
  /// </summary>
  /// <remarks>
  ///   Often used to perform logging.
  /// </remarks>
  /// <typeparam name="TLeft">A left type.</typeparam>
  /// <typeparam name="TRight">A right type.</typeparam>
  /// <param name="either">An <see cref="Either{L,R}" />.</param>
  /// <param name="onSuccess">A side-effect for Right cases.</param>
  /// <returns>
  ///   <paramref name="either" />
  /// </returns>
  public static Either<TLeft, TRight> TapRight<TLeft, TRight>(this Either<TLeft, TRight> either,
    Action<TRight> onSuccess)
  {
    return either.Match(right =>
    {
      onSuccess(right);
      return either;
    }, _ => either);
  }

  /// <summary>
  ///   Execute an asynchronous side effect when <paramref name="either" /> is a Right and returns
  ///   <paramref name="either" /> unchanged.
  /// </summary>
  /// <remarks>
  ///   Often used to dispatch an asynchronous status message, e.g., a workflow heartbeat.
  /// </remarks>
  /// <typeparam name="TLeft">A left type.</typeparam>
  /// <typeparam name="TRight">A right type.</typeparam>
  /// <param name="either">An <see cref="Either{L,R}" />.</param>
  /// <param name="onSuccess">A side-effect for Right cases.</param>
  /// <returns>
  ///   <paramref name="either" />
  /// </returns>
  public static Task<Either<TLeft, TRight>> TapRightAsync<TLeft, TRight>(this Either<TLeft, TRight> either,
    Func<TRight, Task> onSuccess
  )
  {
    return either.Match(async right =>
    {
      await onSuccess(right);
      return either;
    }, _ => either.AsTask());
  }

  /// <summary>
  ///   Convert <paramref name="either" /> to a <see cref="Result{A}" />.
  /// </summary>
  /// <typeparam name="TLeft">A left type.</typeparam>
  /// <typeparam name="TRight">A right type.</typeparam>
  /// <param name="either">An <see cref="Either{L,R}" />.</param>
  /// <param name="ifLeft">
  ///   A function which will convert <typeparamref name="TLeft" /> values into <see cref="Exception" />
  ///   values.
  /// </param>
  /// <returns>A <see cref="Result{A}" />.</returns>
  public static Result<TRight> ToResult<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, Exception> ifLeft)
  {
    return either.Match(right => new Result<TRight>(right), left => new Result<TRight>(e: ifLeft(left)));
  }

  /// <summary>
  ///   Convert <paramref name="either" /> to a <see cref="Result{A}" />.
  /// </summary>
  /// <typeparam name="TLeft">A left type.</typeparam>
  /// <typeparam name="TRight">A right type.</typeparam>
  /// <param name="either">An <see cref="Either{L,R}" />.</param>
  /// <returns>A <see cref="Result{A}" />.</returns>
  public static Result<TRight> ToResult<TLeft, TRight>(this Either<TLeft, TRight> either)
    where TLeft : Exception
  {
    return either.Match(right => new Result<TRight>(right), left => new Result<TRight>(left));
  }
}
