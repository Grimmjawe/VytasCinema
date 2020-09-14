using System;
using System.Windows.Forms;

namespace VytasCinema
{
    public partial class Form1 : Form
    {
        Form2 form2;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Click += pictureBox1_Click;

            ScrollBar vScrollBar1 = new VScrollBar();
            vScrollBar1.Dock = DockStyle.Right;
            vScrollBar1.Scroll += (sender, e) => { this.VerticalScroll.Value = vScrollBar1.Value; };
            this.Controls.Add(vScrollBar1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            form2 = new Form2("DOVOD");
            form2.Show();
        }
    }
}
