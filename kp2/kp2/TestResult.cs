using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;


namespace kp2
{
    class TestResult
    {
        string name, sername, age, group;
        public string Name { get{return name;} }
        public string Sername { get{return sername; } }
        public string Age { get{return age; } }
        public string Group { get{return group;} }

         
        List<string> answers;

        public List<string> Answers { get { return answers; } }

        public TestResult(string name, string sername, string age, string group, List<string> answers)
        {
            this.name = name;
            this.sername = sername;
            this.age = age;
            this.group = group;
            this.answers = answers;
        }


        public override string ToString()
        {
            var strAns = new List<string> { name, sername, age, group };
            foreach (var i in answers) strAns.Add(i);

            return string.Join(";", strAns);
        }

        private string Serialize()
        {
            string jsonStr = $"{{\n\"Name\": {name},\n" + $"\"Sername\": {sername},\n" +
                $"\"Age\": {age},\n" + $"\"Group\": {group},\n" + $"\"Answers\": [\n";

            foreach(var i in answers)
            {
                jsonStr += $"\"{i}\",\n";
            }
            jsonStr += "]\n}\n";

            return jsonStr;
        }

        public void SaveToFile(string path)
        {
            var jsonStr = Serialize();

            File.AppendAllText(path, jsonStr);
        }



        private static TestResult Deserialize(string jsonStr)
        {
            var helpList = new List<string>(jsonStr.Split(':'));


            // приводим массив еще не распаршенных ответов на вопросы лучший вид
            string strAns = helpList[helpList.Count() - 1];

            strAns = strAns.Remove(strAns.Length - 1, 1).Remove(0, 1);
            var helpAns = new List<string>(strAns.Split('\"'));
            // находим и записываем ответы на вопросы
            var realAns = new List<string>(5);
            for (int i = 1; i < helpAns.Count; i += 2)
            {
                realAns.Add(helpAns[i]);
            }


            // удаляем ответы из массива для персональных данных
            helpList.Remove(helpList[helpList.Count() - 1]);
            helpList.Remove(helpList[0]);

            // находим имя фамилию возраст и группу
            var realPersonalData = new List<string>(5);
            foreach (var i in helpList)
            {
                realPersonalData.Add(i.Split(',')[0].Remove(0, 1));
            }


            return new TestResult(realPersonalData[0], realPersonalData[1], realPersonalData[2], realPersonalData[3], realAns);
        }

        public static List<TestResult> ReadFile(string path)
        {
            var jsonStrs = new List<string>(File.ReadAllText(path).Split('}'));
            jsonStrs.Remove(jsonStrs[jsonStrs.Count - 1]);

            List<TestResult> resList = new List<TestResult>(30);

            foreach (var s in jsonStrs)
            {
                resList.Add(Deserialize(s.Remove(0, 1)));
            }

            return resList;
        }

        public static void Clear_File(string path)
        {
            File.Delete(path);
            File.Create(path).Close();
        }





    }
}
