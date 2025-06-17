using DiamondKata.Console;

Console.WriteLine("Input A-Z char");

var inputKey = Console.ReadKey();

var diamondString = DiamondPrinter.Print(inputKey.KeyChar);

Console.WriteLine();
Console.WriteLine(diamondString);