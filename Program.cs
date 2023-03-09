using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kp1
{
    class Program
    {
        static Random rand = new Random();

        // 0 - длина, 1 - к-во чисел, 2 - к-во симв, 3 - большие буквы, 4 - особые симв
        static List<int> flags = new List<int>(5) { -1, -1, -1, 0, 0 };

        // имя: {flag есть ли аргументы, индекс в массиве}
        static Dictionary<string, List<int>> key_words = new Dictionary<string, List<int>>()
        {
            { "--length", new List<int>() { 1, 0 } },
            { "--digits", new List<int>() { 1, 1 } },
            { "--letters", new List<int>() { 1, 2 } },
            { "--uppercase", new List<int>() { 0, 3 } },
            { "-u", new List<int>() { 0, 3 } },
            { "--special", new List<int>() { 0, 4 } },
            { "-s", new List<int>() { 0, 4 } },
            { "-us", new List<int>() { 0, 3, 4 } },

        };

        static List<char> ans = new List<char>();

        enum Type
        {
            LowLetter,
            UpperLetter,
            Digit,
            Special,
        }


        static void Main(string[] args)
        {
            for (var j = 0; j < args.Length; ++j)
            {
                var i = args[j];
                if (i.Length >= 2 && i[0] == '-')
                {
                    var q = i.Split('=');

                    bool is_right = false;
                    foreach (var l in key_words)
                    {
                        if (l.Key != q[0]) continue;

                        int val = 0;
                        if (l.Value[0] == 1 && q.Length == 2 && int.TryParse(q[1], out int k))
                        {
                            is_right = true;
                            val = k;
                        }
                        else if (l.Value[0] == 0)
                        {
                            is_right = true;
                            val = 1;
                        }

                        if (is_right)
                        {
                            for (int x = 1; x < (l.Value).Count; ++x) flags[l.Value[x]] = val;
                            break;
                        }
                    }

                    if (!is_right)
                    {
                        BadEnd();
                        return;
                    }
                }
                else if (int.TryParse(i, out int k) && j == 0)
                {
                    flags[0] = k;
                }
                else
                {
                    BadEnd();
                    return;
                }
            }

            // подгоняем длину по смыслу данных
            if (flags[0] == -1)
            {
                flags[0] = 0;
                if (flags[1] != -1) flags[0] += flags[1];
                if (flags[2] != -1) flags[0] += flags[2];
                if (flags[0] == 0) flags[0] = 16;
            }


            if (flags[0] < Math.Max(0, flags[2]) + Math.Max(0, flags[1]))
            {
                BadEnd();
                return;
            }

            foreach (var i in flags) Console.WriteLine(i);

            //а теперь генерим!!!
            ans.Capacity = flags[0];

            if (flags[1] > 0)
                Gen(Type.Digit, flags[1]); 

            if (flags[2] > 0)
            {
                int kol = 0;
                if (flags[3] != 0) {
                    kol = rand.Next(flags[2] + 1);
                    Gen(Type.UpperLetter, kol);
                }
                Gen(Type.LowLetter, flags[2] - kol);
            }

            // добираем всем подряд

            int ostatok = flags[0] - ans.Count;
            List<Type> types = new List<Type>();

            if (flags[4] != 0)
            {
                types.Add(Type.Special);
            }

            if (flags[1] == -1)
            {
                types.Add(Type.Digit);
            }

            if (flags[2] == -1)
            {
                types.Add(Type.LowLetter);
                if (flags[3] != 0)
                {
                    types.Add(Type.UpperLetter);
                }
            }

            if (ostatok != 0 && types.Count == 0)
            {
                BadEnd();
                return;
            }

            for(int i = 0; i < ostatok; ++i)
            {
                Gen(types[rand.Next(types.Count)], 1);
            }            

            //for(int i = 0; i < ans.Count; ++i)
            //{
            //    Console.Write(ans[i]);
            //}
            Console.WriteLine();

            RandomShuffle();

            for (int i = 0; i < ans.Count; ++i)
            {
                Console.Write(ans[i]);
            }


        }

        static void RandomShuffle()
        {
            int n = ans.Count;
            for(int i = 0; i < n; ++i)
            {
                int ind = rand.Next(n);
                var c = ans[i];
                ans[i] = ans[ind];
                ans[ind] = c;
            }
        }

        static void BadEnd()
        {
            Console.WriteLine("Некоректный ввод((");
        }

        static void Gen(Type a, int kol)
        {
            switch ((int)a)
            {
                case 0:
                    for (int _ = 0; _ < kol; ++_) ans.Add(GenLetterLower());
                    break;
                case 1:
                    for (int _ = 0; _ < kol; ++_) ans.Add(GenLetterUpper());
                    break;
                case 2:
                    for (int _ = 0; _ < kol; ++_) ans.Add(GenDigit());
                    break;
                default:
                    for (int _ = 0; _ < kol; ++_) ans.Add(GenSpecial());
                    break;

            }

        }



        static char GenDigit()
        {
            return (char)((int)'0' + rand.Next(10));
        }

        static char GenLetterLower()
        {
            return (char)((int)'a' + rand.Next(26));
        }

        static char GenLetterUpper()
        {
            return (char)((int)'A' + rand.Next(26));
        }

        static char GenSpecial()
        {
            List<char> v = new List<char> { '#', '-', '&', '*', '_' };
            return v[rand.Next(v.Count)];
        }


    }
}
