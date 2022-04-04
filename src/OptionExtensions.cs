using LanguageExt;

namespace Jds.LanguageExt.Extras;

/// <summary>
///   Extension methods for <see cref="Option{A}" />.
/// </summary>
public static class OptionExtensions
{
  /// <summary>
  ///   Executes a side effect, based upon the state of <paramref name="option" />, and returns <paramref name="option" />
  ///   unchanged.
  /// </summary>
  /// <param name="option">An <see cref="Option{A}" /></param>
  /// <param name="ifSome">An <see cref="Action{T}" /> to execute if <paramref name="option" /> is Some.</param>
  /// <param name="ifNone">An <see cref="Action" /> to execute if <paramref name="option" /> is None.</param>
  /// <typeparam name="T">A <see cref="Option{A}.Some(A)" /> type.</typeparam>
  /// <returns>
  ///   <paramref name="option" />
  /// </returns>
  public static Option<T> Tap<T>(this Option<T> option, Action<T> ifSome, Action ifNone)
  {
    return option.Match(value =>
    {
      ifSome(value);
      return option;
    }, () =>
    {
      ifNone();
      return option;
    });
  }

  /// <summary>
  ///   Executes an asynchronous side effect, based upon the state of <paramref name="option" />, and returns
  ///   <paramref name="option" /> unchanged.
  /// </summary>
  /// <param name="option">An <see cref="Option{A}" /></param>
  /// <param name="ifSome">A <see cref="Func{T,TResult}" /> to execute if <paramref name="option" /> is Some.</param>
  /// <param name="ifNone">A <see cref="Func{T}" /> to execute if <paramref name="option" /> is None.</param>
  /// <typeparam name="T">A <see cref="Option{A}.Some(A)" /> type.</typeparam>
  /// <returns>
  ///   <paramref name="option" />
  /// </returns>
  public static Task<Option<T>> TapAsync<T>(this Option<T> option, Func<T, Task> ifSome, Func<Task> ifNone)
  {
    return option.Match(async value =>
    {
      await ifSome(value);
      return option;
    }, async () =>
    {
      await ifNone();
      return option;
    });
  }

  /// <summary>
  ///   Executes a side effect if <paramref name="option" /> is Some, and returns <paramref name="option" />
  ///   unchanged.
  /// </summary>
  /// <param name="option">An <see cref="Option{A}" /></param>
  /// <param name="ifSome">An <see cref="Action{T}" /> to execute if <paramref name="option" /> is Some.</param>
  /// <typeparam name="T">A <see cref="Option{A}.Some(A)" /> type.</typeparam>
  /// <returns>
  ///   <paramref name="option" />
  /// </returns>
  public static Option<T> TapSome<T>(this Option<T> option, Action<T> ifSome)
  {
    return option.Match(value =>
    {
      ifSome(value);
      return option;
    }, () => option);
  }

  /// <summary>
  ///   Executes an asynchronous side effect if <paramref name="option" /> is Some, and returns
  ///   <paramref name="option" /> unchanged.
  /// </summary>
  /// <param name="option">An <see cref="Option{A}" /></param>
  /// <param name="ifSome">A <see cref="Func{T,TResult}" /> to execute if <paramref name="option" /> is Some.</param>
  /// <typeparam name="T">A <see cref="Option{A}.Some(A)" /> type.</typeparam>
  /// <returns>
  ///   <paramref name="option" />
  /// </returns>
  public static Task<Option<T>> TapSomeAsync<T>(this Option<T> option, Func<T, Task> ifSome)
  {
    return option.Match(async value =>
    {
      await ifSome(value);
      return option;
    }, () => option.AsTask());
  }

  /// <summary>
  ///   Executes a side effect if <paramref name="option" /> is None, and returns <paramref name="option" />
  ///   unchanged.
  /// </summary>
  /// <param name="option">An <see cref="Option{A}" /></param>
  /// <param name="ifNone">An <see cref="Action" /> to execute if <paramref name="option" /> is None.</param>
  /// <typeparam name="T">A <see cref="Option{A}.Some(A)" /> type.</typeparam>
  /// <returns>
  ///   <paramref name="option" />
  /// </returns>
  public static Option<T> TapNone<T>(this Option<T> option, Action ifNone)
  {
    return option.Match(value => option,
      () =>
      {
        ifNone();
        return option;
      });
  }

  /// <summary>
  ///   Executes an asynchronous side effect if <paramref name="option" /> is None, and returns
  ///   <paramref name="option" /> unchanged.
  /// </summary>
  /// <param name="option">An <see cref="Option{A}" /></param>
  /// <param name="ifNone">A <see cref="Func{T}" /> to execute if <paramref name="option" /> is None.</param>
  /// <typeparam name="T">A <see cref="Option{A}.Some(A)" /> type.</typeparam>
  /// <returns>
  ///   <paramref name="option" />
  /// </returns>
  public static Task<Option<T>> TapNoneAsync<T>(this Option<T> option, Func<Task> ifNone)
  {
    return option.Match(value => option.AsTask(),
      async () =>
      {
        await ifNone();
        return option;
      });
  }

  /// <summary>
  ///   Converts type parameter <typeparamref name="T" /> from nullable to non-nullable.
  /// </summary>
  /// <param name="option">An <see cref="Option{A}" />.</param>
  /// <typeparam name="T">A <see cref="Option{A}.Some(A)" /> type.</typeparam>
  /// <returns>The existing <paramref name="option" /> value, with an updated type parameter.</returns>
  public static Option<T> ToNonNullable<T>(this Option<T?> option) where T : class
  {
    // The non-null assertion should be safe because Option<> ensures all null values are None.
    return option.Map(value => value!);
  }

  /// <summary>
  ///   Converts type parameter <typeparamref name="T" /> from nullable to non-nullable.
  /// </summary>
  /// <param name="option">An <see cref="Option{A}" />.</param>
  /// <typeparam name="T">A <see cref="Option{A}.Some(A)" /> type.</typeparam>
  /// <returns>The existing <paramref name="option" /> value, with an updated type parameter.</returns>
  public static Option<T> ToNonNullable<T>(this Option<T?> option) where T : struct
  {
    // The non-null assertion should be safe because Option<> ensures all null values are None.
    return option.Map(value => value!.Value);
  }
}
