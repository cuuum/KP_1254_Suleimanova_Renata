using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kp3
{
    public partial class Form1 : Form
    {
        Account.Permissions baseUserType;

        string mode = "Make";
        int indRedAcc = -1;

        Account baseAcc;


        public Form1(Account acc)
        {
            baseAcc = acc;
            if (acc == null)
            {
                baseUserType = Account.Permissions.Guest;
            }
            else baseUserType = baseAcc.UserType;
            InitializeComponent();


            if ((baseUserType & Account.Permissions.EditOther | (baseUserType & Account.Permissions.EditSelf)) == 0)
            {
                // спрятали кнопки редактирования
                button3.Visible = false;
                button4.Visible = false;
            }

            redrawAccs();
        }


        private void SayAboutError(string message)
        {
            MessageBox.Show(message);
        }

        // обновляет листбокс
        private void redrawAccs()
        {
            listBox1.Items.Clear();


            if ((baseUserType & Account.Permissions.ViewUsers) == 0) return ;

            int ind = 0;
            

            Console.WriteLine(baseUserType.ToString());

            foreach(var i  in AccountsData.accounts)
            {
                ++ind;
                if (((i.UserType & Account.Permissions.Admin) == 0) || (((i.UserType & Account.Permissions.Admin) != 0) && ((baseUserType & Account.Permissions.ViewAdmins) != 0)))
                {
                    var data = $"{ind}. {i.ToString()}";

                    if ((baseUserType & Account.Permissions.ViewLogins) != 0) data += " " + i.Login;

                    if ((baseUserType & Account.Permissions.ViewPasswords) != 0) data += " " + i.Password;

                    listBox1.Items.Add(data);
                }
            }
        }


        // нажата кнопка сохранить
        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string sername = textBox2.Text;
            string login = textBox3.Text;
            string password = textBox4.Text;
            DateTime date = dateTimePicker1.Value;

            Account acc = new Account(name, sername, login, password, date, Account.Permissions.CommonUser, SayAboutError);

            if (acc.CheckIsValid())
            {
                if (mode == "Make") AccountsData.accounts.Add(acc);
                else  AccountsData.accounts[indRedAcc] = acc;

                AccountsData.Update();
                redrawAccs();
            }

        }

        // пользователь заполнил имя
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = Traslitor.transStr(textBox1.Text);
        }

        // пользователь хочет сгенерировать пароль
        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = PasswGenerator.Generate((int)numericUpDown1.Value, checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked, (int)numericUpDown2.Value, (int)numericUpDown3.Value);
        }


        // при закрытии формы хотим сохранить данные в файл
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AccountsData.Save();
        }


        // хотим изменить аккаунт
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;


            indRedAcc = int.Parse(listBox1.SelectedItem.ToString().Split('.')[0]) - 1;

            if ((baseUserType & Account.Permissions.EditOther) == 0 && AccountsData.accounts[indRedAcc] != baseAcc)
            {
                MessageBox.Show("Недостаточно прав");
                return;
            }

            mode = "Red";

            SetAns();
        }

        private void SetAns()
        {
            if (indRedAcc == -1) return ;
            Account acc = AccountsData.accounts[indRedAcc];
            textBox1.Text = acc.Name;
            textBox2.Text = acc.Sername;
            textBox3.Text = acc.Login;
            textBox4.Text = acc.Password;
            dateTimePicker1.Value = acc.DateOfBirth;
        }

        // удалить
        private void button4_Click(object sender, EventArgs e)
        {
            int checkedInd = int.Parse(listBox1.SelectedItem.ToString().Split('.')[0]) - 1;

            if ((baseUserType & Account.Permissions.EditOther) == 0)
            {
                if (AccountsData.accounts[indRedAcc] != baseAcc)
                {
                    MessageBox.Show("Недостаточно прав");
                    return;
                }
                else
                {
                    MessageBox.Show("Вы уверены, что хотите удалить свой аккаунт?....");
                }
            }

            AccountsData.accounts.Remove(AccountsData.accounts[checkedInd]);
            redrawAccs();
        }

        // добавление пользователя
        private void button5_Click(object sender, EventArgs e)
        {
            mode = "Make";
            indRedAcc = -1;
            SetZeroAns();
        }


        private void SetZeroAns()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }
    }
}
