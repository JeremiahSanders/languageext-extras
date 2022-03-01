using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.ResultExtensionsTests;

public class FilterTests
{
  private static bool FilterFunc(int value)
  {
    return value % 2 == 0;
  }

  private static Exception RightToLeftConverter(int value)
  {
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
  public void ReturnsExpectedResults(Result<int> result, Result<int> expected)
  {
    var actual = result.Filter(FilterFunc, RightToLeftConverter);

    Assert.Equal(expected, actual);
  }
}
