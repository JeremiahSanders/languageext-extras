using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class FilterAsyncTests
{
  private static async Task<bool> FilterFunc(int value)
  {
    await Task.Delay(1);
    return value % 2 == 0;
  }

  private static Exception RightToLeftConverter(int value)
  {
    return new ArgumentOutOfRangeException(nameof(value), value.ToString());
  }

  private static async Task<Exception> RightToLeftConverterAsync(int value)
  {
    await Task.Delay(1);
    return new ArgumentOutOfRangeException(nameof(value), value.ToString());
  }

  public static IEnumerable<object[]> CreateTestCases()
  {
    var goodValue = 42;
    var badValue = 41;
    return new[]
    {
      new object[] { new Result<int>(goodValue), new Result<int>(goodValue) },
      new object[] { new Result<int>(badValue), new Result<int>(RightToLeftConverter(badValue)) },
      new object[]
      {
        new Result<int>(new Exception("existing value")), new Result<int>(new Exception("existing value"))
      }
    };
  }

  [Theory]
  [MemberData(nameof(CreateTestCases))]
  public async Task GivenSyncConverter_ReturnsExpectedResults(Result<int> result, Result<int> expected)
  {
    var actual = await result.FilterAsync(FilterFunc, RightToLeftConverter);

    Assert.Equal(expected, actual);
  }

  [Theory]
  [MemberData(nameof(CreateTestCases))]
  public async Task GivenAsyncConverter_ReturnsExpectedResults(Result<int> result, Result<int> expected)
  {
    var actual = await result.FilterAsync(FilterFunc, RightToLeftConverterAsync);

    Assert.Equal(expected, actual);
  }
}
