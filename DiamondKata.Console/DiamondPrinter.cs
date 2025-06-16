using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondKata.Console
{
    public static class DiamondPrinter
    {
        public static string Print(char selectedChar)
        {
            Validate(selectedChar);

            int selectedCharIndex = selectedChar - 'A';
            List<string> result = [];

            for (var currIndex = 0; currIndex <= selectedCharIndex; currIndex++)
            {
                char currChar = (char)(currIndex + 'A');

                string outerDiamond = new string('_', selectedCharIndex - currIndex);
                string innerDiamond = currIndex > 0 ? new string('_', currIndex + currIndex - 1) + currChar : string.Empty;

                string line = $"{outerDiamond}{currChar}{innerDiamond}{outerDiamond}";

                result.Add(string.Join(' ', line.ToCharArray()));
            }

            return string.Join("\n", result.Concat(result.AsEnumerable().Reverse().Skip(1)));
        }

        public static void Validate(char input)
        {
            if (input < 'A' || input > 'Z')
                throw new ArgumentException("Input should be between A-Z");
        }
    }
}
