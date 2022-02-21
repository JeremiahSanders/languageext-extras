using LanguageExt;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.EitherExtensionsTests;

public class FilterTests
{
  private static bool FilterFunc(int value)
  {
    return value % 2 == 0;
  }

  private static string RightToLeftConverter(int value)
  {
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
  public void ReturnsExpectedResults(Either<string, int> either, Either<string, int> expected)
  {
    var actual = either.Filter(FilterFunc, RightToLeftConverter);

    Assert.Equal(expected, actual);
  }
}
