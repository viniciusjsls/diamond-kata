using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondKata.Console
{
    public static class DiamondPrinter
    {
        public static void Validate(char input)
        {
            if (input < 'A' || input > 'Z')
                throw new ArgumentException("Input should be between A-Z");
        }
    }
}
