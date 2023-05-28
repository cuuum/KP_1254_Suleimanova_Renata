using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kp4
{
    class DecrementDecrypt : DecryptProcessor
    {
        public override char Process(char symbol)
        {
            return (char)((int)symbol + 1);
        }
    }
}
