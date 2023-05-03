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
        List<Account> accouts = new List<Account>(100);

        public Form1()
        {
            InitializeComponent();
        }


        private void SayAboutError(string message)
        {
            MessageBox.Show(message);
        }

        // обновляет листбокс
        private void redrawAccs()
        {
            listBox1.Items.Clear();
            for(int i = 0; i < accouts.Count; ++i)
            {
                listBox1.Items.Insert(listBox1.Items.Count, $"User_{i + 1} {accouts[i].ToString()}");
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

            Account acc = new Account(name, sername, login, password, date, SayAboutError);
            if (acc.CheckIsValid())
            {
                accouts.Add(acc);
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
    }
}
