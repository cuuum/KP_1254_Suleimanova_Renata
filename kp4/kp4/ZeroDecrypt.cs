using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kp4
{
    class ZeroDecrypt : DecryptProcessor
    {
        public override char Process(char symbol)
        {
            return symbol;
        }
    }
}
