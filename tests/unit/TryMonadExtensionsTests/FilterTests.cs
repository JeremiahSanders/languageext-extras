using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.TryMonadExtensionsTests;

public class FilterTests
{
  public class GivenPassingFilter
  {
    [Fact]
    public void ReturnsSuccess()
    {
      var initialTry = Prelude.Try(() => 42);
      var filtered = initialTry.Filter(value => value % 2 == 0,
        value => new InvalidOperationException($"Expected even. Received: {value}"));

      var actual = filtered.Try();

      actual.IfFailThrow();
    }

    [Fact]
    public void MemoizesResult()
    {
      var filterExecutionCount = 0;

      var initialTry = Prelude.Try(() => 42);
      var filtered = initialTry.Filter(value =>
        {
          filterExecutionCount++;
          return value % 2 == 0;
        },
        value => new InvalidOperationException($"Expected even. Received: {value}"));

      var countBeforeTry = filterExecutionCount;

      _ = filtered.Try();
      _ = filtered.Try();
      _ = filtered.Try();

      Assert.Equal(0, countBeforeTry);
      Assert.Equal(1, filterExecutionCount);
    }
  }

  public class GivenFailingFilter
  {
    [Fact]
    public void ReturnsFailure()
    {
      var initialTry = Prelude.Try(() => 42);
      var filtered = initialTry.Filter(value => value % 2 != 0,
        value => new InvalidOperationException($"Expected odd. Received: {value}"));

      var actual = filtered.Try();
      var exception = actual.Match(_ => throw new Exception("Should have failed."), Prelude.identity);

      Assert.IsType<InvalidOperationException>(exception);
    }

    [Fact]
    public void DoesNotMemoizeFailures()
    {
      var filterExecutionCount = 0;

      var initialTry = Prelude.Try(() => 42);
      var filtered = initialTry.Filter(value =>
        {
          filterExecutionCount++;
          return value % 2 != 0;
        },
        value => new InvalidOperationException($"Expected odd. Received: {value}"));

      var countBeforeTry = filterExecutionCount;

      _ = filtered.Try();
      _ = filtered.Try();
      _ = filtered.Try();

      Assert.Equal(0, countBeforeTry);
      Assert.Equal(3, filterExecutionCount);
    }
  }
}
