using System;
using Microsoft.VisualBasic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Net.Mail;

namespace VytasCinema
{
    public partial class Form2 : Form
    {
        string name;
        PictureBox[,] _arr = new PictureBox[4, 4];
        Image img_seat = Image.FromFile(AppContext.BaseDirectory + "seat.jpg"),
        img_seat_choose = Image.FromFile(AppContext.BaseDirectory + "seat_choose.jpg"),
        img_seat_bought = Image.FromFile(AppContext.BaseDirectory + "seat_bought.jpg");
        public Form2(string nAme)
        {
            name = nAme;
            this.Text = name;
            if (!File.Exists(AppContext.BaseDirectory + name + ".txt"))
            {
                string text = "";
                StreamWriter file1 = new StreamWriter(AppContext.BaseDirectory + name + ".txt");
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        text += i + "," + j + ",false;";
                    }
                    text += "\n";
                }
                file1.Write(text);
                file1.Close();
            }
            StreamReader file = new StreamReader(AppContext.BaseDirectory + name + ".txt");
            string[] ar = file.ReadToEnd().Split('\n');
            InitializeComponent();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _arr[i, j] = new PictureBox();
                    _arr[i, j].SizeMode = PictureBoxSizeMode.Zoom;


                    string[] ards = ar[i].Split(';');
                    string[] ardNum = ards[j].Split(',');
                    if (ardNum[2] == "true")
                    {
                        _arr[i, j].Image = img_seat_bought;
                    }
                    else
                    {
                        _arr[i, j].Image = img_seat;
                    }


                    _arr[i, j].Size = new Size(40, 50);
                    _arr[i, j].Location = new Point(j * 40, i * 50);
                    _arr[i, j].Click += Form2_Click;
                    this.Controls.Add(_arr[i, j]);
                }
            }
            file.Close();
        }

        private void Form2_Click(object s, EventArgs e)
        {
            PictureBox pic = s as PictureBox;
            if (pic.Image != img_seat_bought)
            {
                if (pic.Image == img_seat_choose)
                {
                    pic.Image = img_seat;
                }
                else
                {
                    pic.Image = img_seat_choose;
                }
            }
        }

        void Ask()
        {
            var vastus = MessageBox.Show("Kas oled kindel?", "Appolo kusib", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            string text = "";
            if (vastus == DialogResult.Yes)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_arr[i, j].Image == img_seat_choose)
                        {
                            _arr[i, j].Image = img_seat_bought;
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_arr[i, j].Image == img_seat_bought)
                        {
                            text += i + "," + j + ",true;";
                        }
                        else
                        {
                            text += i + "," + j + ",false;";
                        }
                    }
                    text += "\n";
                }
                using (StreamWriter file = new StreamWriter(AppContext.BaseDirectory + name + ".txt"))
                {
                    file.WriteLine(text);
                    file.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ask();
            string mailAd = Interaction.InputBox("Sisesta e-mail","Kuhu saada","vvytasik@gmail.com");
            MailMessage mail = new MailMessage();
            mail.To.Add(mailAd);
            mail.From = new MailAddress("vvytasik@gmail.com");
            mail.Subject = "Pilet";
            mail.Body = "Rida ... Koht ...";
            //StmpServer.Port = 587;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (_arr[i, j].Image == img_seat_choose)
                    {
                        Ask();
                    }
                }
            }
            this.Close();
        }
    }
}
