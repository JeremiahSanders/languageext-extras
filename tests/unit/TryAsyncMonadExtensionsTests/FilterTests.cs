using System.Diagnostics.CodeAnalysis;

using FluentAssertions;

using LanguageExt;
using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.TryAsyncMonadExtensionsTests;

[SuppressMessage("ReSharper", "AccessToStaticMemberViaDerivedType",
  Justification = "Use of ExpectedException.New conveys intent")]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "xUnit constructs instances")]
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

        actual.Invoking(result => result.IfFailThrow()).Should().NotThrow();
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

        countBeforeTry.Should().Be(0);
        filterExecutionCount.Should().Be(1);
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

        countBeforeTry.Should().Be(0);
        filterExecutionCount.Should().Be(3);
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

        _ = actual.IfFailThrow();
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

        countBeforeTry.Should().Be(0);
        filterExecutionCount.Should().Be(1);
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

  public class CancellationTokenTests
  {
    private static TryAsync<int> CreateSuccessfulTryAsync(int value = 42)
    {
      return Prelude.TryAsync(async () =>
      {
        await Task.Delay(1); // Include a non-0 delay in an attempt to ensure async invocation
        return value;
      });
    }

    public class WithExceptionCreator
    {
      public class GivenUncanceledToken
      {
        [Fact]
        public async Task ReturnsSuccess()
        {
          const int ArrangedValue = 1;
          (await CreateSuccessfulTryAsync(ArrangedValue)
              .Filter(_ => ExpectedException.New("should not get here"), CancellationToken.None)
              .IfFailThrow())
            .Should().Be(ArrangedValue);
        }
      }

      public class GivenCanceledToken
      {
        [Fact]
        public async Task CoercesTryToExceptionCreatorResponse()
        {
          var canceledToken = new CancellationToken(true);
          const string ExpectedExceptionMessage = "Arranged exception";

          var abortedIfCanceled = CreateSuccessfulTryAsync()
            .Filter(
              _ => ExpectedException.New(ExpectedExceptionMessage),
              canceledToken
            );

          await abortedIfCanceled.Invoking(tryAsync => tryAsync.IfFailThrow()).Should().ThrowAsync<ExpectedException>()
            .WithMessage(ExpectedExceptionMessage);
        }
      }

      public class WhenCanceledBetweenSteps
      {
        [Fact]
        public async Task FailsAtCorrectLocation()
        {
          const int InitialValue = 12;
          const int ExpectedValue = InitialValue * InitialValue;
          var expectedExceptionMessage = $"Cancellation was requested. Value at time of abort = {ExpectedValue}";
          var tokenSource = new CancellationTokenSource();
          bool? isCanceledBeforeCancellation = null;
          int? tappedValue = null;

          var token = tokenSource
            .Token; // This token represents an incoming CancellationToken, e.g., in a Controller action.

          var workflow = CreateSuccessfulTryAsync(InitialValue)
            .Filter(
              value => new InvalidOperationException(
                $"Cancellation token was supposed to be non-canceled. Value: {value}"), token)
            .TapSuccess(value =>
            {
              isCanceledBeforeCancellation = token.IsCancellationRequested;
              tappedValue = value;
              tokenSource.Cancel();
            })
            .Map(value => value * value)
            .Filter(
              value => ExpectedException.New($"Cancellation was requested. Value at time of abort = {value}"), token)
            .Map(value => Math.Sqrt(value)) // Alter the value _after_ filtration
            .Filter(_ => false,
              value => new Exception(
                $"TryAsync should have been filtered to a Failure before reaching this point. Received: {value}"
              )
            );

          await workflow.Invoking(tryAsync => tryAsync.IfFailThrow()).Should().ThrowAsync<ExpectedException>()
            .WithMessage(expectedExceptionMessage);
          isCanceledBeforeCancellation.Should().Be(false);
          tappedValue.Should().Be(InitialValue,
            "because squaring occurs in the .Map after token cancellation and before filtering, and just after capturing .Tap");
        }
      }
    }

    public class WithToken
    {
      public class GivenUncanceledToken
      {
        [Fact]
        public async Task ReturnsSuccess()
        {
          const int ArrangedValue = 1;
          (await CreateSuccessfulTryAsync(ArrangedValue)
              .Filter(CancellationToken.None)
              .IfFailThrow())
            .Should().Be(ArrangedValue);
        }
      }

      public class GivenCanceledToken
      {
        [Fact]
        public async Task CoercesTryToExceptionCreatorResponse()
        {
          var canceledToken = new CancellationToken(true);

          var abortedIfCanceled = CreateSuccessfulTryAsync()
            .Filter(canceledToken);

          await abortedIfCanceled.Invoking(tryAsync => tryAsync.IfFailThrow()).Should()
            .ThrowAsync<OperationCanceledException>();
        }
      }

      public class WhenCanceledBetweenSteps
      {
        [Fact]
        public async Task FailsAtCorrectLocation()
        {
          const int InitialValue = 12;
          var tokenSource = new CancellationTokenSource();
          bool? isCanceledBeforeCancellation = null;
          int? tappedValue = null;

          var token = tokenSource
            .Token; // This token represents an incoming CancellationToken, e.g., in a Controller action.

          var workflow = CreateSuccessfulTryAsync(InitialValue)
            .Filter(token)
            .TapSuccess(value =>
            {
              isCanceledBeforeCancellation = token.IsCancellationRequested;
              tappedValue = value;
              tokenSource.Cancel();
            })
            .Map(value => value * value)
            .Filter(token)
            .Map(value => Math.Sqrt(value)) // Alter the value _after_ filtration
            .Filter(_ => false,
              value => new Exception(
                $"TryAsync should have been filtered to a Failure before reaching this point. Received: {value}"
              )
            );

          await workflow.Invoking(tryAsync => tryAsync.IfFailThrow()).Should().ThrowAsync<OperationCanceledException>();
          isCanceledBeforeCancellation.Should().Be(false);
          tappedValue.Should().Be(InitialValue,
            "because squaring occurs in the .Map after token cancellation and before filtering, and just after capturing .Tap");
        }
      }
    }
  }
}
