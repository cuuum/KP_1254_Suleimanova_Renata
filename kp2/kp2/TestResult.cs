using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;


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
            string jsonSrt = $"{{\n\"Name\": {name},\n" + $"\"Sername\": {sername},\n" +
                $"\"Age\": {age},\n" + $"\"Group\": {group},\n" + $"\"Answers\": [\n";

            foreach(var i in answers)
            {
                jsonSrt += $"\"{i}\",\n";
            }

            return jsonSrt;
        }

        public void SaveToFile()
        {
            var jsonStr = Serialize();
            using (FileStream fstream = new FileStream("", FileMode.OpenOrCreate))
            {
                
            }

        }






    }
}
