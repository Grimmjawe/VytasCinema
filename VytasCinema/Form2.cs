﻿using System;
using Microsoft.VisualBasic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Collections.Generic;

namespace VytasCinema
{
    public partial class Form2 : Form
    {
        string name;
        List<string> arr_poilet;
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
                    var breakk = true;
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (_arr[i, j].Image == img_seat_choose)
                            {
                                breakk = false;
                            }
                        }
                    }
                    if (breakk)
                        button1.Enabled = false;
                }
                else
                {
                    pic.Image = img_seat_choose;
                    button1.Enabled = true;
                }
            }
        }

        void Ask()
        {
            string text = "";
            arr_poilet = new List<string>();
            var vastus = MessageBox.Show("Kas oled kindel?", "Appolo kusib", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (vastus == DialogResult.Yes)
            {
                int t = 0;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_arr[i, j].Image == img_seat_choose)
                        {
                            t++;
                            _arr[i, j].Image = img_seat_bought;
                            StreamWriter pilet = new StreamWriter(AppContext.BaseDirectory + "Pilet" + t.ToString() + "rida" + i.ToString() + "koht" + j.ToString() + ".txt");
                            pilet.Write("Pilet: " + t.ToString() + " rida: " + i.ToString() + " koht: " + j.ToString());
                            pilet.Close();
                            arr_poilet.Add("Pilet" + t.ToString() + "rida" + i.ToString() + "koht" + j.ToString() + ".txt");
                        }
                    }
                }
                SaadaBilet();
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

        private void SaadaBilet()
        {
            try
            {
                string mailAd = Interaction.InputBox("Sisesta e-mail", "Kuhu saada", "vvytasik@gmail.com");
                MailMessage mail = new MailMessage();
                SmtpClient stmpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("mvc.programmeerimine@gmail.com", "3.Kuursus"),
                    EnableSsl = true
                };
                mail.To.Add(mailAd);
                mail.From = new MailAddress("mvc.programmeerimine@gmail.com");
                mail.Subject = "Pilet";
                mail.Body = "";
                foreach (var item in arr_poilet)
                {
                    Attachment data = new Attachment(item);
                    mail.Attachments.Add(data);
                }
                stmpClient.Send(mail);
                foreach (var item in arr_poilet)
                {
                    File.Delete(item);
                }
            }
            catch (Exception)
            {
                foreach (var item in arr_poilet)
                {
                    File.Delete(item);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ask();
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
