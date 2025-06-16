using DiamondKata.Console;
using FluentAssertions;

namespace DiamondKata.Tests;

public class DiamondKataTests
{
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
        //yield return new object[] { 'A', new List<string> { "A" } };
        //yield return new object[] { 'B', new List<string> { "_A_", "B_B", "_A_" } };
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
    public void GivenInputCharBetweenBAndZ_ThenFirstAndLastItemsShouldBeEqual()
    {
        // Arrange
        // Act
        // Assert
    }

    [Fact]
    public void GivenInputCharBetweenBAndZ_ThenEachLineShouldHaveTwoEqualLettersInAscendingOrder()
    {
        // Arrange
        // Act
        // Assert
    }

    [Fact]
    public void GivenInputCharBetweenBAndZ_ThenSpaceBetweenLettersShouldMatchDiamondShape()
    {
        // Arrange
        // Act
        // Assert
    }

    [Fact]
    public void GivenValidInput_ThenSpaceBeforeAndAfterLettersShouldMatchDiamondShape()
    {
        // Arrange
        // Act
        // Assert
    }
}