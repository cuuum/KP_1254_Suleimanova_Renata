using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;

namespace kp3
{
    static class AccountsData
    {
        public static List<Account> accounts = new List<Account>(1000);
        static Dictionary<string, string> logPasw = new Dictionary<string, string>(100);

        static AccountsData()
        {
            if (!File.Exists("source/accountsData.txt"))
                File.Create("source/accountsData.txt");

            var accountsData = File.ReadAllText("source/accountsData.txt");



            if (!File.Exists("source/accountsPrivateData.txt"))
                File.Create("source/accountsPrivateData.txt");

            var accountsPrivateData = File.ReadAllText("source/accountsPrivateData.txt");

            


            foreach (var i in accountsPrivateData.Split('\n'))
            {
                var data = i.Split('\n')[0].Split(' ');
                if (data.Count() < 2) continue;

                logPasw[data[0]] = data[1];
            }


            foreach (var i in accountsData.Split('\n'))
            {
                var data = i.Split(' ');

                if (data.Count() < 5) continue;

                string name = data[0], sername = data[1], dateStr = data[2], login = data[3], userTypeStr = data[4];

                string password = logPasw[login];


                // разбираемя с датой
                int day = int.Parse(dateStr.Split('.')[0]);
                int month = int.Parse(dateStr.Split('.')[1]);
                int year = int.Parse(dateStr.Split('.')[2]);

                // разбираемся с типом доступа
                List<Account.Permissions> types = new List<Account.Permissions> { Account.Permissions.Guest, Account.Permissions.CommonUser, Account.Permissions.Admin };

                Account.Permissions userType = Account.Permissions.None;

                foreach(var j in types)
                {
                    if (j.ToString() == userTypeStr)
                        userType = j;
                }


                accounts.Add(new Account(name, sername, login, password, new DateTime(year, month, day), userType));
            }



        }

        public static void Update()
        {
            logPasw.Clear();
            foreach(var i in accounts)
            {
                logPasw[i.Login] = i.Password;
            }
        }



        public static Account GetAcc(string login, string passw, Account.LogError logFunk)
        {
            if (!logPasw.Keys.Contains(login))
            {
                logFunk("Нет такого аккаунта");
                return null;
            }
            if (logPasw[login] != passw)
            {
                logFunk("Неверный пароль((");
                return null;
            }

            foreach(var i in accounts)
            {
                if (i.Login == login)
                {
                    logFunk($"Здравствуйте, {i.Name}!!))!!)!))!");
                    return i;
                }
            }
            return null;
        }


        public static void Save()
        {
            string allData = "";
            string privateData = "";

            foreach(var i in accounts)
            {
                allData += i.ToString() + " " + i.Login + " " + i.UserType + "\n";
                privateData += i.Login + " " + i.Password + "\n";
            }

            File.WriteAllText("source/accountsData.txt", allData);
            File.WriteAllText("source/accountsPrivateData.txt", privateData);
        }

    }
}
