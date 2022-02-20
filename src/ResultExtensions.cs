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
