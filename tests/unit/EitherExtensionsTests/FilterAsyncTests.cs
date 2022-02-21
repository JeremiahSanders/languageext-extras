using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.EitherExtensionsTests;

public class FilterAsyncTests
{
  private static async Task<bool> FilterFunc(int value)
  {
    await Task.Delay(1);
    return value % 2 == 0;
  }

  private static string RightToLeftConverter(int value)
  {
    return value.ToString();
  }

  private static async Task<string> RightToLeftConverterAsync(int value)
  {
    await Task.Delay(1);
    return value.ToString();
  }

  public static IEnumerable<object[]> CreateTestCases()
  {
    var goodValue = 42;
    var badValue = 41;
    return new[]
    {
      new object[] { Prelude.Right<string, int>(goodValue), Prelude.Right<string, int>(goodValue) },
      new object[] { Prelude.Right<string, int>(badValue), Prelude.Left<string, int>(value: badValue.ToString()) },
      new object[]
      {
        Prelude.Left<string, int>(value: "existing value"), Prelude.Left<string, int>(value: "existing value")
      }
    };
  }

  [Theory]
  [MemberData(memberName: nameof(CreateTestCases))]
  public async Task GivenSyncConverter_ReturnsExpectedResults(Either<string, int> either, Either<string, int> expected)
  {
    var actual = await either.FilterAsync(FilterFunc, RightToLeftConverter);

    Assert.Equal(expected, actual);
  }

  [Theory]
  [MemberData(memberName: nameof(CreateTestCases))]
  public async Task GivenAsyncConverter_ReturnsExpectedResults(Either<string, int> either, Either<string, int> expected)
  {
    var actual = await either.FilterAsync(FilterFunc, RightToLeftConverterAsync);

    Assert.Equal(expected, actual);
  }
}
