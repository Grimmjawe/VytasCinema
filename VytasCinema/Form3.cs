using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace VytasCinema
{
    public partial class Form3 : Form
    {
        string name;
        Random rnd = new Random();
        List<string> arr = new List<string>();
        public Form3(string nAme)
        {
            InToBD(1, 1, 1);
            name = nAme;
            InitializeComponent();
            this.Text = name + " - Сеансы";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (!File.Exists(AppContext.BaseDirectory + name + "Seanses.txt"))
            {
                int A = 0;
                string text = "";
                StreamWriter file1 = new StreamWriter(AppContext.BaseDirectory + name + "Seanses.txt");
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int ind = rnd.Next(59);
                        text += rnd.Next(23).ToString() + ":" + ((ind < 10) ? "0"+ind.ToString() : ind.ToString()) + ",";
                        text += A + ",";
                        text += rnd.Next(5,10) + ",";
                        text += rnd.Next(5,10) + ";";
                        A++;
                    }
                    text = text.Substring(0, text.Length - 1);
                    text += "\n";
                }
                file1.Write(text);
                file1.Close();
            }
            StreamReader file = new StreamReader(AppContext.BaseDirectory + name + "Seanses.txt");
            string[] ar = file.ReadToEnd().Split('\n');
            foreach (var item in ar)
            {
                arr.Add(item);
            }
            foreach (var item in arr[0].Split(';'))
            {
                comboBox2.Items.Insert(0, item.Split(',')[0]);
            }
            comboBox2.SelectedIndex = 0;

            comboBox1.Items.Insert(0, "ВС");
            comboBox1.Items.Insert(0, "СБ");
            comboBox1.Items.Insert(0, "ПТ");
            comboBox1.Items.Insert(0, "ЧТ");
            comboBox1.Items.Insert(0, "СР");
            comboBox1.Items.Insert(0, "ВТ");
            comboBox1.Items.Insert(0, "ПН");
            comboBox1.SelectedIndex = 0;

            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            foreach (var item in arr[comboBox1.SelectedIndex].Split(';'))
            {
                comboBox2.Items.Insert(0, item.Split(',')[0]);
            }
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4;
            form4 = new Form4(
                name, 
                Convert.ToInt32(arr[comboBox1.SelectedIndex].Split(';')[comboBox2.SelectedIndex].Split(',')[2]), 
                Convert.ToInt32(arr[comboBox1.SelectedIndex].Split(';')[comboBox2.SelectedIndex].Split(',')[3]), 
                Convert.ToInt32(arr[comboBox1.SelectedIndex].Split(';')[comboBox2.SelectedIndex].Split(',')[1])
            );
            form4.Show();
        }

        void InToBD(int t, int i, int j) {
            string connString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=aspnet-MvcMovie;Integrated Security=SSPI;AttachDbFilename=|DataDirectory|\AppData\Database1.mdf",
            sql = "INSERT INTO Piletid(Id,Rida,Koht) VALUES (" + t + "," + i + "," + j + ")";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(sql, conn);
            sqlCommand.ExecuteNonQuery();
            sqlCommand.Dispose();
            conn.Close();
        }
    }
}
