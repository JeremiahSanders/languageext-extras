using LanguageExt;
using LanguageExt.Common;

using Xunit;

namespace Jds.LanguageExt.Extras.Tests.Unit.EitherExtensionsTests;

public class ToResultTests
{
  private const int RightValue = 42;
  private const string LeftStringValue = "someMissingPropertyName";
  private static Result<int> RightResult { get; } = new(RightValue);
  private static ArgumentNullException LeftExceptionValue { get; } = new();

  private static ArgumentException ToArgumentException(string propertyName)
  {
    return new ArgumentException(propertyName);
  }

  public static IEnumerable<object[]> CreateExceptionTestCases()
  {
    return new[]
    {
      new object[] { Prelude.Right<ArgumentNullException, int>(RightValue), RightResult },
      new object[]
      {
        Prelude.Left<ArgumentNullException, int>(LeftExceptionValue), new Result<int>(LeftExceptionValue)
      }
    };
  }

  public static IEnumerable<object[]> CreateNonExceptionTestCases()
  {
    return new[]
    {
      new object[] { Prelude.Right<string, int>(RightValue), RightResult },
      new object[]
      {
        Prelude.Left<string, int>(LeftStringValue), new Result<int>(ToArgumentException(LeftStringValue))
      }
    };
  }

  [Theory]
  [MemberData(nameof(CreateExceptionTestCases))]
  public void GivenLeftException_ReturnsExpectedValues(Either<ArgumentNullException, int> either, Result<int> expected)
  {
    var actual = either.ToResult();

    Assert.Equal(expected, actual);
  }

  [Theory]
  [MemberData(nameof(CreateNonExceptionTestCases))]
  public void GivenLeftNonException_ReturnsExpectedValues(Either<string, int> either, Result<int> expected)
  {
    var actual = either.ToResult(ToArgumentException);

    Assert.Equal(expected, actual);
  }
}
