using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace kp2
{
    public partial class Form1 : Form
    {
        List<string> bufferAns;
        TestResult autoTestRes;

        List<TestResult> results = new List<TestResult>(100);

        // разделитель для ответов на 2 вопрос
        char sep = '$';

        public Form1()
        {
            InitializeComponent();

            // задаем авто - ответы
            List<string> autoAns = new List<string> { radioButton1.Text, checkBox1.Text + sep + checkBox2.Text, comboBox2.Items[0].ToString(), "10", "Здесь могла бы быть ваша реклама" };
            autoTestRes = new TestResult("Василий", "Смагин", "0", "1254", autoAns);

            bufferAns = autoAns;
        }

        // вытягиваем ответы из формы и сохраняем в класс
        private TestResult Get_Ans()
        {

            string name = textBox1.Text;
            string sername = textBox2.Text;
            string age = numericUpDown1.Value.ToString();
            string group = comboBox1.Text;

            bufferAns = Enumerable.Repeat("", bufferAns.Count).ToList();

            // 1
            foreach (var i in new List<RadioButton> { radioButton1, radioButton2, radioButton3 })
            {
                if (i.Checked)
                {
                    bufferAns[0] = i.Text;
                    break;
                }
            }

            // 2
            foreach (var i in new List<CheckBox> { checkBox1, checkBox2, checkBox3, checkBox4 })
            {


                //Console.WriteLine(i.Text);
                if (i.Checked)
                {
                    bufferAns[1] += i.Text + sep;
                }
            }

            // 3
            bufferAns[2] = comboBox2.Text;

            // 4
            bufferAns[3] = numericUpDown2.Value.ToString();

            // 5
            bufferAns[4] = richTextBox1.Text;

            return new TestResult(name, sername, age, group, bufferAns);

        }

        // устанавливаем ответы в форму
        private void Set_Ans(TestResult ans)
        {

            textBox1.Text = ans.Name;
            textBox2.Text = ans.Sername;

            if (Int32.TryParse(ans.Age, out int answ))
                numericUpDown1.Value = answ;

            comboBox1.Text = ans.Group;

            bufferAns = Enumerable.Repeat("", bufferAns.Count).ToList();

            // 1
            foreach (var i in new List<RadioButton> { radioButton1, radioButton2, radioButton3 })
            {
                i.Checked = false;
                if (i.Text == ans.Answers[0])
                {
                    i.Checked = true;
                }
            }

            // 2

            var ansSecondQuest = ans.Answers[1].Split(sep);

            foreach (var box in new List<CheckBox> { checkBox1, checkBox2, checkBox3, checkBox4 })
            {
                box.Checked = false;
                foreach (var str in ansSecondQuest)
                {
                    if (box.Text == str)
                    {
                        box.Checked = true;
                    }
                }
            }

            // 3
            comboBox2.Text = ans.Answers[2];

            // 4
            if (Int32.TryParse(ans.Answers[3], out int answ1))
                numericUpDown2.Value = answ1;

            // 5
            richTextBox1.Text = ans.Answers[4];

        }


        private void Update_ListBox()
        {
            listBox1.Items.Clear();

            for (int i = 0; i < results.Count; ++i)
            {
                listBox1.Items.Add((i + 1).ToString() + ". " + results[i].ToString());
            }
        }

        private void Clear_Form()
        {
            TestResult emptyAns = new TestResult("", "", "0", "", new List<string> { "", "", "", "0", "" });
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            Set_Ans(emptyAns);
        }



        // нажали сохранить(не в файл)
        private void button2_Click(object sender, EventArgs e)
        {
            TestResult ans = Get_Ans();

            results.Add(ans);
            Update_ListBox();
        }

        // автозаполнение
        private void button3_Click(object sender, EventArgs e)
        {
            Set_Ans(autoTestRes);
        }

        // сброс
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Очистить форму?(((((", "",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Exclamation);

            if (dialogResult == DialogResult.Yes)
            {
                Clear_Form();
            }
        }

        // изменился выбор в листбоксе
        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            Set_Ans(results[listBox1.SelectedIndex]);
        }

        // нажали сохранить в файл
        private void button4_Click(object sender, EventArgs e)
        {
            TestResult.Clear_File("out.json");
            foreach (var i in results) i.SaveToFile("out.json");
        }

        // нажали загрузить файл
        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog1.FileName;

                foreach (var i in TestResult.ReadFile(filePath))
                {
                    results.Add(i);
                }

                Update_ListBox();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            results.Clear();
            Update_ListBox();
        }
    }
}
