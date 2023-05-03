using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace kp3
{
    static class PasswGenerator
    {

        public static string Generate(int len, bool upper, bool special, bool isAlf, bool isNum, int alfLen, int numLen)
        {
            string args = $"{len} ";
            if (upper) args += "-u ";
            if (special) args += "-s ";
            if (isAlf) args += $"--letters={alfLen} ";
            if (isNum) args += $"--digits={numLen} ";

            Console.WriteLine(args);

            return RunApp(args);
        }


        static string RunApp(string args)
        {
            // Создание объекта для запуска внешнего приложения
            Process process = new Process();

            process.StartInfo.FileName = "kp1.exe";                  // указание пути к файлу запускаемой программы
            process.StartInfo.Arguments = args;                // передача аргументов запускаемой программы
            process.StartInfo.UseShellExecute = false;          // false требуется, чтобы можно было читать из вывода запущенного приложения
            process.StartInfo.RedirectStandardOutput = true;    // true требуется, чтобы можно было читать из стандартного вывода запущенного приложения
            process.StartInfo.RedirectStandardError = true;     // true требуется, чтобы можно было читать из ошибки запущенного приложения
            process.StartInfo.CreateNoWindow = true;            // отключаем создание окна (на всякий случай)

            // Запускаем приложение
            bool started = process.Start();
            if (!started)
            {
                Console.WriteLine("Ошибка запуска приложения!");
                return "";
            }

            // Читамем вывод приложения
            string output = process.StandardOutput.ReadToEnd();

            // Дождаемся окончания работы приложения и выходим
            process.WaitForExit();
            Console.WriteLine("Dct rhenj");
            return output;
        }
    }
}
