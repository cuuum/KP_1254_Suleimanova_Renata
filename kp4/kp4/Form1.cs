using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kp4
{
    public partial class Form1 : Form
    {
        List<Encryptor> allEncryptors = new List<Encryptor>();
        List<Encryptor> totalEncryptors = new List<Encryptor>();

        Dictionary<string, Encryptor> encryptorDict = new Dictionary<string, Encryptor>();


        public Form1()
        {
            InitializeComponent();
            listBox1.AllowDrop = true;
            listBox2.AllowDrop = true;

            fillEncryptors();
            fillBaseListBoxAndDict();
        }

        private char IncrementSymbolCode(char symbolCode)
        {
            return (char)((int)symbolCode + 1);
        }

        private char DecrementSymbolCode(char symbolCode)
        {
            return (char)((int)symbolCode - 1);
        }

        private char ZeroSymbolCode(char symbolCode)
        {
            return symbolCode;
        }

        void fillEncryptors()
        {
            allEncryptors.Add(new Encryptor(IncrementSymbolCode, "Increment", "I"));
            allEncryptors.Add(new Encryptor(DecrementSymbolCode, "Decrement", "D"));
            allEncryptors.Add(new Encryptor(ZeroSymbolCode, "Zero", "Z"));
        }

        void fillBaseListBoxAndDict()
        {
            foreach (var i in allEncryptors)
            {
                listBox1.Items.Add(i);
                encryptorDict[i.Name] = i;
            }
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            listBox2.DoDragDrop(listBox1.SelectedItem.ToString(), DragDropEffects.Copy);
        }

        private void listBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void listBox2_DragDrop(object sender, DragEventArgs e)
        {
            listBox2.Items.Add(e.Data.GetData(DataFormats.Text));
        }

        // нажата кнопка шифровать
        private void button5_Click(object sender, EventArgs e)
        {
            GetDataFromListBox2();

            // шифровка строки

            char[] textArr = richTextBox1.Text.ToArray<char>();
            string text = "";
            string key = "";
            foreach(Encryptor enc in totalEncryptors)
            {
                key += enc.Key;
                text = "";
                for (int i = 0; i < textArr.Length; ++i)
                {
                    textArr[i] = enc.encrypt(textArr[i]);
                    text += textArr[i];
                }
            }

            richTextBox2.Text = text;

            richTextBox4.Text = key;
        }

        void GetDataFromListBox2()
        {
            totalEncryptors.Clear();
            foreach(var i in listBox2.Items)
            {
                totalEncryptors.Add(encryptorDict[i.ToString()]);
            }
        }

        // расшифровка
        private void button6_Click(object sender, EventArgs e)
        {
            string text = richTextBox2.Text;
            string key = richTextBox4.Text;

            richTextBox3.Text = Decryptor.Decrypt(key, text);
        }
    }
}
