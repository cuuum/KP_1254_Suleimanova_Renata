using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace kp3
{
    static class Traslitor
    {
        static Dictionary<string, string> dictTrans = new Dictionary<string, string>();



        static Traslitor()
        {
            var data = File.ReadAllText("dictionary.txt");

            foreach (var i in data.Split('\n'))
            {
                var str = i.Split('|');

                var a = str[1].Trim();
                var b = str[2].Trim();
                dictTrans[a] = b;
            }

        }

        public static string transStr(string s)
        {
            string newStr = "";
            foreach (var i in s)
            {
                if (dictTrans.TryGetValue(i + "", out string ss))
                {
                    newStr += ss;
                }
                else
                {
                    newStr += i + "";
                }
            }
            return newStr;
        }


    }
}
