using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kp4
{
    public class Encryptor
    {
        public delegate char EncyrptFunction(char c);

        EncyrptFunction enryptFunc;
        string name;
        string key;

        public string Name
        {
            get { return name; }
        }

        public string Key
        {
            get { return key; }
        }

        public Encryptor(EncyrptFunction func, string name, string key)
        {
            enryptFunc = func;
            this.name = name;
            this.key = key;
        }

        public char encrypt(char symbol)
        {
            return enryptFunc(symbol);
        }

        public override string ToString()
        {
            return name;
        }
    }
}
