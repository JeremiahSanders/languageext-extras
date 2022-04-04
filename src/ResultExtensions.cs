using LanguageExt;
using LanguageExt.Common;

namespace Jds.LanguageExt.Extras;

/// <summary>
///   Extension methods for <see cref="Result{A}" />.
/// </summary>
public static class ResultExtensions
{
  /// <summary>
  ///   Execute <paramref name="func" />, which returns a <see cref="Result{A}" />, when <paramref name="result" /> is
  ///   success.
  /// </summary>
  /// <remarks>Commonly used to chain <see cref="Prelude.Try{A}(System.Func{A})" /> executions in a workflow.</remarks>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="func">
  ///   A function which accepts a <typeparamref name="TSuccess" /> and returns a
  ///   <see cref="Result{A}" />.
  /// </param>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <typeparam name="TNewSuccess">A new success type.</typeparam>
  /// <returns>A <see cref="Result{A}" />.</returns>
  public static Result<TNewSuccess> Bind<TSuccess, TNewSuccess>(this Result<TSuccess> result,
    Func<TSuccess, Result<TNewSuccess>> func)
  {
    return result.Match(func, exception => new Result<TNewSuccess>(exception));
  }

  /// <summary>
  ///   Execute <paramref name="func" />, which returns a <see cref="Task" /> of a <see cref="Result{A}" />, when
  ///   <paramref name="result" /> is success.
  /// </summary>
  /// <remarks>
  ///   Commonly used to chain <see cref="Prelude.TryAsync{A}(System.Func{System.Threading.Tasks.Task{A}})" />
  ///   executions in a workflow.
  /// </remarks>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="func">
  ///   A function which accepts a <typeparamref name="TSuccess" /> and returns a
  ///   <see cref="Task" /> of <see cref="Result{A}" />.
  /// </param>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <typeparam name="TNewSuccess">A new success type.</typeparam>
  /// <returns>A <see cref="Result{A}" />.</returns>
  public static Task<Result<TNewSuccess>> BindAsync<TSuccess, TNewSuccess>(this Result<TSuccess> result,
    Func<TSuccess, Task<Result<TNewSuccess>>> func)
  {
    return result.Match(async success => await func(success),
      exception => new Result<TNewSuccess>(exception).AsTask());
  }

  /// <summary>
  ///   Execute <paramref name="func" />, which returns a <see cref="Result{A}" />, when <paramref name="result" /> is
  ///   failure.
  /// </summary>
  /// <remarks>Commonly used to retry or recover <see cref="Prelude.Try{A}(System.Func{A})" /> executions in a workflow.</remarks>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="func">
  ///   A function which accepts a <typeparamref name="TSuccess" /> and returns a
  ///   <see cref="Result{A}" />.
  /// </param>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <returns>A <see cref="Result{A}" />.</returns>
  public static Result<TSuccess> BindFailure<TSuccess>(this Result<TSuccess> result,
    Func<Exception, Result<TSuccess>> func)
  {
    return result.Match(_ => result, func);
  }

  /// <summary>
  ///   Execute <paramref name="func" />, which returns a <see cref="Task" /> of a <see cref="Result{A}" />, when
  ///   <paramref name="result" /> is failure.
  /// </summary>
  /// <remarks>
  ///   Commonly used to retry or recover <see cref="Prelude.TryAsync{A}(System.Func{System.Threading.Tasks.Task{A}})" />
  ///   executions in a workflow.
  /// </remarks>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="func">
  ///   A function which accepts a <typeparamref name="TSuccess" /> and returns a
  ///   <see cref="Task" /> of <see cref="Result{A}" />.
  /// </param>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <returns>A <see cref="Result{A}" />.</returns>
  public static Task<Result<TSuccess>> BindFailureAsync<TSuccess>(this Result<TSuccess> result,
    Func<Exception, Task<Result<TSuccess>>> func)
  {
    return result.Match(_ => result.AsTask(),
      async success => await func(success));
  }

  /// <summary>
  ///   Filters right values by executing <paramref name="filter" />. If true, returns the current
  ///   <typeparamref name="TSuccess" /> value. If false, executes <paramref name="onFalse" /> to convert the filtered
  ///   <typeparamref name="TSuccess" /> to an <see cref="Exception" />.
  /// </summary>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="filter">
  ///   A filtering function. If true, value continues as-is. If false, value is converted to an <see cref="Exception" />
  ///   with <paramref name="onFalse" />.
  /// </param>
  /// <param name="onFalse">
  ///   A function to convert <typeparamref name="TSuccess" /> to an <see cref="Exception" />.
  /// </param>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <returns>
  ///   A <see cref="Result{A}" />. If <paramref name="filter" /> returns true, current <typeparamref name="TSuccess" /> is
  ///   returned. If it returns false, the converted <see cref="Exception" /> is returned.
  /// </returns>
  public static Result<TSuccess> Filter<TSuccess>(this Result<TSuccess> result,
    Func<TSuccess, bool> filter,
    Func<TSuccess, Exception> onFalse
  )
  {
    return result.Bind(value => filter(value)
      ? new Result<TSuccess>(value)
      : new Result<TSuccess>(onFalse(value)));
  }

  /// <summary>
  ///   Filters right values by executing <paramref name="filter" />. If true, returns the current
  ///   <typeparamref name="TSuccess" /> value. If false, executes <paramref name="onFalse" /> to convert the filtered
  ///   <typeparamref name="TSuccess" /> to an <see cref="Exception" />.
  /// </summary>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="filter">
  ///   A filtering function. If true, value continues as-is. If false, value is converted to an <see cref="Exception" />
  ///   with <paramref name="onFalse" />.
  /// </param>
  /// <param name="onFalse">
  ///   A function to convert <typeparamref name="TSuccess" /> to an <see cref="Exception" />.
  /// </param>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <returns>
  ///   A <see cref="Result{A}" />. If <paramref name="filter" /> returns true, current <typeparamref name="TSuccess" /> is
  ///   returned. If it returns false, the converted <see cref="Exception" /> is returned.
  /// </returns>
  public static Task<Result<TSuccess>> FilterAsync<TSuccess>(this Result<TSuccess> result,
    Func<TSuccess, Task<bool>> filter,
    Func<TSuccess, Exception> onFalse
  )
  {
    return result.BindAsync(async value => await filter(value)
      ? new Result<TSuccess>(value)
      : new Result<TSuccess>(onFalse(value)));
  }

  /// <summary>
  ///   Filters right values by executing <paramref name="filter" />. If true, returns the current
  ///   <typeparamref name="TSuccess" /> value. If false, executes <paramref name="onFalse" /> to convert the filtered
  ///   <typeparamref name="TSuccess" /> to an <see cref="Exception" />.
  /// </summary>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="filter">
  ///   A filtering function. If true, value continues as-is. If false, value is converted to an <see cref="Exception" />
  ///   with <paramref name="onFalse" />.
  /// </param>
  /// <param name="onFalse">
  ///   A function to convert <typeparamref name="TSuccess" /> to an <see cref="Exception" />.
  /// </param>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <returns>
  ///   A <see cref="Result{A}" />. If <paramref name="filter" /> returns true, current <typeparamref name="TSuccess" /> is
  ///   returned. If it returns false, the converted <see cref="Exception" /> is returned.
  /// </returns>
  public static Task<Result<TSuccess>> FilterAsync<TSuccess>(this Result<TSuccess> result,
    Func<TSuccess, Task<bool>> filter,
    Func<TSuccess, Task<Exception>> onFalse
  )
  {
    return result.BindAsync(async value => await filter(value)
      ? new Result<TSuccess>(value)
      : new Result<TSuccess>(await onFalse(value)));
  }

  /// <summary>
  ///   Gets the value of <paramref name="result" />, throwing an <see cref="InvalidOperationException" /> if it is a
  ///   failure.
  /// </summary>
  /// <remarks>
  ///   This method supports testing. Specifically, since test runners treat uncaught exceptions as test failures,
  ///   <see cref="IfFailThrow{TSuccess}" /> can be used within test methods to unpack test arrangement results and fail the
  ///   test if an <see cref="Exception" /> occurred during the arranged workflow (which resulted in
  ///   <paramref name="result" />).
  /// </remarks>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <returns>The success value in <paramref name="result" />.</returns>
  /// <exception cref="InvalidOperationException">
  ///   Thrown when <paramref name="result" /> is a failure. The failure
  ///   <see cref="Exception" /> will be the <see cref="Exception.InnerException" />.
  /// </exception>
  public static TSuccess IfFailThrow<TSuccess>(this Result<TSuccess> result)
  {
    return result.IfFail(exception => throw new InvalidOperationException("Result was a failure.", exception));
  }

  /// <summary>
  ///   Converts an existing <see cref="Exception" /> into a new type, using <paramref name="func" />, if
  ///   <paramref name="result" /> is a failure.
  /// </summary>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="func">A <see cref="Func{T,TResult}" /></param>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <typeparam name="TFailure">A new <see cref="Exception" />.</typeparam>
  /// <returns>A <see cref="Result{A}" />.</returns>
  public static Result<TSuccess> MapFailure<TSuccess, TFailure>(this Result<TSuccess> result,
    Func<Exception, TFailure> func)
    where TFailure : Exception
  {
    return result.Match(_ => result, exception => new Result<TSuccess>(func(exception)));
  }

  /// <summary>
  ///   Asynchronously converts an existing <see cref="Exception" /> into a new type, using <paramref name="func" />, if
  ///   <paramref name="result" /> is a failure.
  /// </summary>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="func">A <see cref="Func{T,TResult}" /></param>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <typeparam name="TFailure">A new <see cref="Exception" />.</typeparam>
  /// <returns>A <see cref="Result{A}" />.</returns>
  public static Task<Result<TSuccess>> MapFailureAsync<TSuccess, TFailure>(this Result<TSuccess> result,
    Func<Exception, Task<TFailure>> func)
    where TFailure : Exception
  {
    return result.Match(_ => result.AsTask(), async exception => new Result<TSuccess>(await func(exception)));
  }

  /// <summary>
  ///   Execute <paramref name="func" />, which returns a <typeparamref name="TNewSuccess" />, when
  ///   <paramref name="result" /> is success.
  ///   Exceptions are caught and returned as a failure. (I.e., simplifies <c>Bind(Try(func))</c>)
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     Executes <see cref="Bind{TSuccess,TNewSuccess}" />, using <paramref name="func" /> within
  ///     <see cref="Prelude.Try{A}(System.Func{A})" />.
  ///   </para>
  ///   <para>
  ///     Commonly used to chain steps in a workflow without requiring each function be explicitly wrapped in a
  ///     <see cref="Prelude.Try{A}(System.Func{A})" />.
  ///   </para>
  /// </remarks>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="func">
  ///   A function which accepts a <typeparamref name="TSuccess" /> and returns a <typeparamref name="TNewSuccess" />.
  /// </param>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <typeparam name="TNewSuccess">A new success type.</typeparam>
  /// <returns>A <see cref="Result{A}" />.</returns>
  public static Result<TNewSuccess> MapSafe<TSuccess, TNewSuccess>(this Result<TSuccess> result,
    Func<TSuccess, TNewSuccess> func)
  {
    return result.Bind(value => Prelude.Try(() => func(value)).Try());
  }

  /// <summary>
  ///   Execute <paramref name="func" />, which returns a <see cref="Task" /> of a <typeparamref name="TNewSuccess" />, when
  ///   <paramref name="result" /> is success.
  ///   Exceptions are caught and returned as a failure. (I.e., simplifies <c>BindAsync(TryAsync(func))</c>)
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     Executes <see cref="Bind{TSuccess,TNewSuccess}" />, using <paramref name="func" /> within
  ///     <see cref="Prelude.Try{A}(System.Func{A})" />.
  ///   </para>
  ///   <para>
  ///     Commonly used to chain steps in a workflow without requiring each function be explicitly wrapped in a
  ///     <see cref="Prelude.Try{A}(System.Func{A})" />.
  ///   </para>
  /// </remarks>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="func">
  ///   A function which accepts a <typeparamref name="TSuccess" /> and returns a <typeparamref name="TNewSuccess" />.
  /// </param>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <typeparam name="TNewSuccess">A new success type.</typeparam>
  /// <returns>A <see cref="Result{A}" />.</returns>
  public static Task<Result<TNewSuccess>> MapSafeAsync<TSuccess, TNewSuccess>(this Result<TSuccess> result,
    Func<TSuccess, Task<TNewSuccess>> func)
  {
    return result.BindAsync(value => Prelude.TryAsync(() => func(value)).Try());
  }

  /// <summary>
  ///   Execute a side effect and returns <paramref name="result" /> unchanged.
  /// </summary>
  /// <remarks>
  ///   Often used to perform logging.
  /// </remarks>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="onSuccess">A side-effect for success cases.</param>
  /// <param name="onFailure">A side-effect for failure cases.</param>
  /// <returns>
  ///   <paramref name="result" />
  /// </returns>
  public static Result<TSuccess> Tap<TSuccess>(this Result<TSuccess> result, Action<TSuccess> onSuccess,
    Action<Exception> onFailure)
  {
    return result.Match(success =>
    {
      onSuccess(success);
      return result;
    }, exception =>
    {
      onFailure(exception);
      return result;
    });
  }

  /// <summary>
  ///   Execute an asynchronous side effect and returns <paramref name="result" /> unchanged.
  /// </summary>
  /// <remarks>
  ///   Often used to dispatch an asynchronous status message, e.g., a workflow heartbeat.
  /// </remarks>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="onSuccess">A side-effect for success cases.</param>
  /// <param name="onFailure">A side-effect for failure cases.</param>
  /// <returns>
  ///   <paramref name="result" />
  /// </returns>
  public static Task<Result<TSuccess>> TapAsync<TSuccess>(this Result<TSuccess> result,
    Func<TSuccess, Task> onSuccess,
    Func<Exception, Task> onFailure
  )
  {
    return result.Match(async success =>
    {
      await onSuccess(success);
      return result;
    }, async exception =>
    {
      await onFailure(exception);
      return result;
    });
  }

  /// <summary>
  ///   Execute a side effect when <paramref name="result" /> is a failure and returns
  ///   <paramref name="result" /> unchanged.
  /// </summary>
  /// <remarks>
  ///   Often used to perform logging.
  /// </remarks>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="onFailure">A side-effect for failure cases.</param>
  /// <returns>
  ///   <paramref name="result" />
  /// </returns>
  public static Result<TSuccess> TapFailure<TSuccess>(this Result<TSuccess> result,
    Action<Exception> onFailure)
  {
    return result.Match(success => result, exception =>
    {
      onFailure(exception);
      return result;
    });
  }

  /// <summary>
  ///   Execute an asynchronous side effect when <paramref name="result" /> is a failure and returns
  ///   <paramref name="result" /> unchanged.
  /// </summary>
  /// <remarks>
  ///   Often used to dispatch an asynchronous status message, e.g., a workflow heartbeat.
  /// </remarks>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="onFailure">A side-effect for failure cases.</param>
  /// <returns>
  ///   <paramref name="result" />
  /// </returns>
  public static Task<Result<TSuccess>> TapFailureAsync<TSuccess>(this Result<TSuccess> result,
    Func<Exception, Task> onFailure)
  {
    return result.Match(success => result.AsTask(), async exception =>
    {
      await onFailure(exception);
      return result;
    });
  }

  /// <summary>
  ///   Execute a side effect when <paramref name="result" /> is a success and returns <paramref name="result" /> unchanged.
  /// </summary>
  /// <remarks>
  ///   Often used to perform logging.
  /// </remarks>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="onSuccess">A side-effect for success cases.</param>
  /// <returns>
  ///   <paramref name="result" />
  /// </returns>
  public static Result<TSuccess> TapSuccess<TSuccess>(this Result<TSuccess> result, Action<TSuccess> onSuccess)
  {
    return result.Match(success =>
    {
      onSuccess(success);
      return result;
    }, _ => result);
  }

  /// <summary>
  ///   Execute an asynchronous side effect when <paramref name="result" /> is a success and returns
  ///   <paramref name="result" /> unchanged.
  /// </summary>
  /// <remarks>
  ///   Often used to dispatch an asynchronous status message, e.g., a workflow heartbeat.
  /// </remarks>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <param name="onSuccess">A side-effect for success cases.</param>
  /// <returns>
  ///   <paramref name="result" />
  /// </returns>
  public static Task<Result<TSuccess>> TapSuccessAsync<TSuccess>(this Result<TSuccess> result,
    Func<TSuccess, Task> onSuccess
  )
  {
    return result.Match(async success =>
    {
      await onSuccess(success);
      return result;
    }, _ => result.AsTask());
  }


  /// <summary>
  ///   Convert <paramref name="result" /> to an <see cref="Either{L,R}" />.
  /// </summary>
  /// <param name="result">A <see cref="Result{A}" />.</param>
  /// <typeparam name="TSuccess">A success type.</typeparam>
  /// <returns>An <see cref="Either{L,R}" />.</returns>
  public static Either<Exception, TSuccess> ToEither<TSuccess>(this Result<TSuccess> result)
  {
    return result.Match(Prelude.Right<Exception, TSuccess>, Prelude.Left<Exception, TSuccess>);
  }
}
