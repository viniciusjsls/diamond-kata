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
}