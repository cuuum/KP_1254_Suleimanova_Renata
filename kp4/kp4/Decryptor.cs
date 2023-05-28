using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kp4
{
    static class Decryptor
    {
        public static string Decrypt(string key, string textToDecrypt)
        {
            DecryptContainer container = new DecryptContainer(key);

            string text = "";
            char[] textArr = textToDecrypt.ToArray<char>();

            foreach(var dec in container.Decryptors)
            {
                text = "";
                for(int i = 0; i < textArr.Length; ++i)
                {
                    textArr[i] = dec.Process(textArr[i]);
                    text += textArr[i];
                }
            }

            return text;
        }
    }
}
