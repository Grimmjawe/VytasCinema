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
            "TheNewMutants",
            "SpongBobMovie",
            "Glava2"
        };
        public Form1()
        {
            InitializeComponent();
            panel1.AutoScroll = true;

            for (int i = 0; i < arr.Count; i++)
            {
                PictureBox pictureBox = new PictureBox() {
                    Size = new Size(215, 310),
                    Location = new Point(11, i * 310),
                    Image = Image.FromFile(AppContext.BaseDirectory + arr[i] + ".jpg"),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Tag = arr[i]
                };
                Form2 form2;
                pictureBox.Click += (e, s) => {
                    PictureBox pictureBox1 = (e as PictureBox);
                    form2 = new Form2(pictureBox1.Tag.ToString() + "");
                    form2.Show();
                };
                panel1.Controls.Add(pictureBox);
            }
        }
    }
}
