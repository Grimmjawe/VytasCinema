using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VytasCinema
{
    public partial class Form1 : Form
    {
        List<string> arr = new List<string>()
        {
            "DOVOD",
            "TheNewMutants"
        };
        public Form1()
        {
            InitializeComponent();
            panel1.AutoScroll = true;

            for (int i = 0; i < arr.Count; i++)
            {
                PictureBox pictureBox = new PictureBox() {
                    Size = new Size(215, 310),
                    Location = new Point(11, i * 318),
                    Image = Image.FromFile(AppContext.BaseDirectory + arr[i] + ".jpg"),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Tag = new int[] { i }
                };
                Form2 form2;
                pictureBox.Click += (e, s) => {
                    PictureBox pictureBox = (s as PictureBox);
                    form2 = new Form2(arr[i] + "");
                    form2.Show();
                };
                panel1.Controls.Add(pictureBox);
            }
        }
    }
}
