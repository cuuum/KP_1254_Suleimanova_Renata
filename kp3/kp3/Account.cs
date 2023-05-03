using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kp3
{
    class Account
    {
        public delegate void LogError(string message);

        string name, sername, login, password;
        DateTime dateOfBirth;

        List<char> allowedSpecial = new List<char> { '-', '_', '@' };

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

        public Account(string name, string sername, string login, string password, DateTime dateOfBirth, LogError logErrorFunk)
        {
            if (!CheckLogin(login))
            {
                logErrorFunk("Пароль содержит недопустимые символы");
                // поставили значение -заглушку, по которому мы поймем нужно ли сохранять аккаунт
                login = "-1";
            }
            else
            {
                this.name = name;
                this.sername = sername;
                this.login = login;
                this.password = password;
                this.dateOfBirth = dateOfBirth;
            }
        }

        public bool CheckIsValid()
        {
            return (login != "-1");
        }

        public override string ToString()
        {
            return $"({name} {sername})";
        }
    }
}
