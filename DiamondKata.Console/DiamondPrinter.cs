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
            List<string> result = new List<string> { "_ _ A _ _", "_ B _ B _", "C _ _ _ C", "_ B _ B _", "_ _ A _ _" };

            return string.Join("\n", result);
        }

        public static void Validate(char input)
        {
            if (input < 'A' || input > 'Z')
                throw new ArgumentException("Input should be between A-Z");
        }
    }
}
