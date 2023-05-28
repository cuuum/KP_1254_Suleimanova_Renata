using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kp4
{
    class DecryptContainer
    {
        List<DecryptProcessor> decryptors = new List<DecryptProcessor>();

        public List<DecryptProcessor> Decryptors
        {
            get { return decryptors; }
        }


        public DecryptContainer(string key)
        {
            foreach(var i in key)
            {
                switch (i)
                {
                    case 'I':
                        decryptors.Add(new IncrementDecrypt());
                        break;
                    case 'D':
                        decryptors.Add(new DecrementDecrypt());
                        break;
                    case 'Z':
                        decryptors.Add(new ZeroDecrypt());
                        break;

                }
            }
            
        }

    }
}
