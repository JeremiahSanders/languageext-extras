using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.TryAsyncMonadExtensionsTests;

public class FilterTests
{
  public class AsynchronousFilter
  {
    public class GivenPassingFilter
    {
      [Fact]
      public async Task ReturnsSuccess()
      {
        var initialTry = Prelude.TryAsync(() => 42.AsTask());
        var filtered = initialTry.Filter(value => (value % 2 == 0).AsTask(),
          value => new InvalidOperationException($"Expected even. Received: {value}"));

        var actual = await filtered.Try();

        actual.IfFailThrow();
      }

      [Fact]
      public async Task MemoizesResult()
      {
        var filterExecutionCount = 0;

        var initialTry = Prelude.TryAsync(() => 42.AsTask());
        var filtered = initialTry.Filter(value =>
          {
            filterExecutionCount++;
            return (value % 2 == 0).AsTask();
          },
          value => new InvalidOperationException($"Expected even. Received: {value}"));

        var countBeforeTry = filterExecutionCount;

        _ = await filtered.Try();
        _ = await filtered.Try();
        _ = await filtered.Try();

        Assert.Equal(0, countBeforeTry);
        Assert.Equal(1, filterExecutionCount);
      }
    }

    public class GivenFailingFilter
    {
      [Fact]
      public async Task ReturnsFailure()
      {
        var initialTry = Prelude.TryAsync(() => 42.AsTask());
        var filtered = initialTry.Filter(value => (value % 2 != 0).AsTask(),
          value => new InvalidOperationException($"Expected odd. Received: {value}"));

        var actual = await filtered.Try();
        var exception = actual.Match(_ => throw new Exception("Should have failed."), Prelude.identity);

        Assert.IsType<InvalidOperationException>(exception);
      }

      [Fact]
      public async Task DoesNotMemoizeFailures()
      {
        var filterExecutionCount = 0;

        var initialTry = Prelude.TryAsync(() => 42.AsTask());
        var filtered = initialTry.Filter(value =>
          {
            filterExecutionCount++;
            return (value % 2 != 0).AsTask();
          },
          value => new InvalidOperationException($"Expected odd. Received: {value}"));

        var countBeforeTry = filterExecutionCount;

        _ = await filtered.Try();
        _ = await filtered.Try();
        _ = await filtered.Try();

        Assert.Equal(0, countBeforeTry);
        Assert.Equal(3, filterExecutionCount);
      }
    }
  }

  public class SynchronousFilter
  {
    public class GivenPassingFilter
    {
      [Fact]
      public async Task ReturnsSuccess()
      {
        var initialTry = Prelude.TryAsync(() => 42.AsTask());
        var filtered = initialTry.Filter(value => value % 2 == 0,
          value => new InvalidOperationException($"Expected even. Received: {value}"));

        var actual = await filtered.Try();

        actual.IfFailThrow();
      }

      [Fact]
      public async Task MemoizesResult()
      {
        var filterExecutionCount = 0;

        var initialTry = Prelude.TryAsync(() => 42.AsTask());
        var filtered = initialTry.Filter(value =>
          {
            filterExecutionCount++;
            return value % 2 == 0;
          },
          value => new InvalidOperationException($"Expected even. Received: {value}"));

        var countBeforeTry = filterExecutionCount;

        _ = await filtered.Try();
        _ = await filtered.Try();
        _ = await filtered.Try();

        Assert.Equal(0, countBeforeTry);
        Assert.Equal(1, filterExecutionCount);
      }
    }

    public class GivenFailingFilter
    {
      [Fact]
      public async Task ReturnsFailure()
      {
        var initialTry = Prelude.TryAsync(() => 42.AsTask());
        var filtered = initialTry.Filter(value => value % 2 != 0,
          value => new InvalidOperationException($"Expected odd. Received: {value}"));

        var actual = await filtered.Try();
        var exception = actual.Match(_ => throw new Exception("Should have failed."), Prelude.identity);

        Assert.IsType<InvalidOperationException>(exception);
      }

      [Fact]
      public async Task DoesNotMemoizeFailures()
      {
        var filterExecutionCount = 0;

        var initialTry = Prelude.TryAsync(() => 42.AsTask());
        var filtered = initialTry.Filter(value =>
          {
            filterExecutionCount++;
            return value % 2 != 0;
          },
          value => new InvalidOperationException($"Expected odd. Received: {value}"));

        var countBeforeTry = filterExecutionCount;

        _ = await filtered.Try();
        _ = await filtered.Try();
        _ = await filtered.Try();

        Assert.Equal(0, countBeforeTry);
        Assert.Equal(3, filterExecutionCount);
      }
    }
  }
}
