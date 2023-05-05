using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kp3
{
    public class Account
    {
        public delegate void LogError(string message);

        [Flags]
        public enum Permissions
        {
            None = 0,
            ViewUsers = 1,
            ViewAdmins = 2,
            EditSelf = 4,
            EditOther = 8,
            ViewLogins = 16,
            ViewPasswords = 32,

            Guest = ViewUsers,
            CommonUser = ViewAdmins | ViewUsers | ViewLogins | EditSelf,
            Admin = CommonUser | EditOther | ViewPasswords
        }

        string name, sername, login, password;
        DateTime dateOfBirth;
        Permissions userType;

        List<char> allowedSpecial = new List<char> { '-', '_', '@' };


        public Permissions UserType
        {
            get { return userType; }
        }

        public string Login
        {
            get { return login; }
        }

        public string Password
        {
            get { return password; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Sername
        {
            get { return sername; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
        }



        public Account(string name, string sername, string login1, string password, DateTime dateOfBirth, Permissions userType, LogError logErrorFunk)
        {
            if (!CheckLogin(login1))
            {
                logErrorFunk("Логин содержит недопустимые символы");
                // поставили значение -заглушку, по которому мы поймем нужно ли сохранять аккаунт
                login = "-1";
            }
            else
            {
                this.name = name;
                this.sername = sername;
                this.login = login1;
                this.password = password;
                this.dateOfBirth = dateOfBirth;
                this.userType = userType;
            }
        }

        // конструктор для уже проверенной информации(которую мы читаем из файла)
        public Account(string name, string sername, string login, string password, DateTime dateOfBirth, Permissions userType)
        {
            this.name = name;
            this.sername = sername;
            this.login = login;
            this.password = password;
            this.dateOfBirth = dateOfBirth;
            this.userType = userType;            
        }



        private bool CheckLogin(string login)
        {
            foreach(var i in login)
            {
                if (!(('A' <= i && i <= 'Z') || ('a' <= i && i <= 'z') || allowedSpecial.Contains(i)))
                {
                    return false;
                }
            }
            return true;
        }
                

        public bool CheckIsValid()
        {
            return (login != "-1");
        }

        public override string ToString()
        {
            return $"{name} {sername} { dateOfBirth.Date.ToString().Remove(10) }";
        }

    }
}
