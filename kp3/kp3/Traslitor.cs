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
            var data = File.ReadAllText("source/dictionary.txt");

            foreach (var i in data.Split('\n'))
            {
                var str = i.Split('|');

                var a = str[1].Trim();
                var b = str[2].Trim();
                dictTrans[a] = b;

                // Создаем заглавную версию алфавита

                a = Char.ToUpper(a[0]) + "";
                if (b.Count() > 0) b = Char.ToUpper(b[0]) + b.Substring(1);
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
