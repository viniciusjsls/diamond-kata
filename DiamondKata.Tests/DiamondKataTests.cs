using DiamondKata.Console;
using FluentAssertions;
using System.Text.RegularExpressions;

namespace DiamondKata.Tests;

public class DiamondKataTests
{
    private readonly Regex _containsRegex = new("[A-Z]_*[A-Z]");
    private const int MAX_LETTER_COUNT = 2;

    [Fact]
    public void GivenInputCharLowerThanA_ThenThrowException()
    {
        //Arrange
        // A index is 65, so any char below it should fail
        Action validateAction = () => DiamondPrinter.Validate((char)64);

        // Assert
        validateAction.Should().ThrowExactly<ArgumentException>("Inputs different than A to Z are out of scope");
    }

    [Fact]
    public void GivenInputCharBiggerThanA_ThenThrowException()
    {
        //Arrange
        // Z index is 90, so any char above it should fail
        Action validateAction = () => DiamondPrinter.Validate((char)91);

        // Assert
        validateAction.Should().ThrowExactly<ArgumentException>("Inputs different than A to Z are out of scope");
    }

    [Fact]
    public void GivenInputCharBetweenAAndZ_ThenNoException()
    {
        for (int inputIndex = 'A'; inputIndex <= 'Z'; inputIndex++)
        {
            //Arrange
            Action validateAction = () => DiamondPrinter.Validate((char)inputIndex);

            // Assert
            validateAction.Should().NotThrow<ArgumentException>("A to Z are valid inputs");
        }
    }

    public static IEnumerable<object[]> ValidInputsAndExpectedResult()
    {
        // Arrange
        yield return new object[] { 'A', new List<string> { "A" } };
        yield return new object[] { 'B', new List<string> { "_A_", "B_B", "_A_" } };
        yield return new object[] { 'C', new List<string> { "__A__", "_B_B_", "C___C", "_B_B_", "__A__" } };
    }

    private IEnumerable<string> TrimAndSplitDiamondString(string diamond)
    {
        return diamond.Replace(" ", string.Empty).Split("\n");
    }

    [Theory]
    [MemberData(nameof(ValidInputsAndExpectedResult))]
    public void GivenValidInput_ThenShouldMatchExactlyString(char inputChar, IEnumerable<string> expectedResult)
    {
        // Act
        var diamondLines = TrimAndSplitDiamondString(DiamondPrinter.Print(inputChar));

        // Assert
        expectedResult.Should().HaveSameCount(diamondLines);
        expectedResult.SequenceEqual(diamondLines).Should().BeTrue("It should match template in order and value");
    }

    [Fact]
    public void GivenInputCharIsA_ThenReturnA()
    {
        // Act
        var diamondLines = TrimAndSplitDiamondString(DiamondPrinter.Print('A'));

        // Assert
        diamondLines.Should().HaveCount(1).And.Contain("A");
    }

    [Fact]
    public void GivenValidInputChar_ThenFirstLineShouldAlwaysHaveOneLetter()
    {
        // Act
        var diamondLines = TrimAndSplitDiamondString(DiamondPrinter.Print('C'));

        var diamondFirstLine = diamondLines.First();

        // Assert
        diamondFirstLine.Trim('_').Should().HaveLength(1).And.Be("A", "It always begins with A");
    }

    [Fact]
    public void GivenInputCharBetweenBAndZ_ThenEachLineShouldHaveTwoEqualLettersInAscendingOrder()
    {
        // Arrange
        char inputChar = 'C';
        int inputLength = inputChar - 'B';

        // Act
        var diamondLines = TrimAndSplitDiamondString(DiamondPrinter.Print(inputChar));

        for (int lineIndex = 1; lineIndex <= inputLength; lineIndex++)
        {
            var diamondLine = diamondLines.ElementAt(lineIndex).Replace("_", string.Empty);

            // Assert
            diamondLine.Should().HaveLength(MAX_LETTER_COUNT, "Each line should have two letters");
            diamondLine.First().Should().Be(diamondLine.Last(), "Each line should have two equal letters");
            diamondLine.First().Should().Be((char)('A' + lineIndex), "Should sort ascending");
        }
    }

    [Fact]
    public void GivenInputCharBetweenBAndZ_ThenSpaceBetweenLettersShouldMatchDiamondShape()
    {
        // Arrange
        char inputChar = 'C';
        int inputLength = inputChar - 'B';

        // Act
        var diamondLines = TrimAndSplitDiamondString(DiamondPrinter.Print(inputChar));

        for (int lineIndex = 1; lineIndex <= inputLength; lineIndex++)
        {
            var diamondLine = _containsRegex.Match(diamondLines.ElementAt(lineIndex).Replace("_", string.Empty)).Value;

            // Assert
            diamondLine.Count(x => x == '_').Should().Be(diamondLine.Length - MAX_LETTER_COUNT, "Underscore count should match string length minus maximum allowed letters");
        }
    }

    [Fact]
    public void GivenValidInput_ThenSpaceBeforeAndAfterLettersShouldMatchDiamondShape()
    {
        // Arrange
        char inputChar = 'C';
        int inputLength = inputChar - 'A';

        // Act
        var diamondLines = TrimAndSplitDiamondString(DiamondPrinter.Print(inputChar));

        for (int lineIndex = 0; lineIndex <= inputLength; lineIndex++)
        {
            var diamondLine = diamondLines.ElementAt(lineIndex);

            // Assert
            // The amount of space to shape a diagonal, input length has to be reduced by the lineIndex as it increases
            int spacesToReachInputLetter = inputLength - lineIndex;
            // remaining length represents the amount of characters left after the space used to shape diagonal is removed
            int remainingStringLength = diamondLine.Length - spacesToReachInputLetter;
            diamondLine.TrimStart('_').Should().HaveLength(remainingStringLength);
            diamondLine.TrimEnd('_').Should().HaveLength(remainingStringLength);
        }
    }

    [Fact]
    ///The reversed list should be equal to the original, so all the checks done in previous tests ensure it has a diamond shape
    public void GivenValidInput_ThenDiamondStringTopHalfEqualBottomHalf()
    {
        // Act
        var diamondLines = TrimAndSplitDiamondString(DiamondPrinter.Print('C'));

        // Assert
        diamondLines.SequenceEqual(diamondLines.Reverse()).Should().BeTrue();
    }
}