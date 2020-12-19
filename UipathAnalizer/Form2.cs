using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UipathAnalizer
{
    public partial class Form2 : Form
    {
        int[] rainSpeeds = { 8, 7, 6, 5, 5, 6, 7, 8 };
        int loadingSpeed = 10;
        float initialPercentage = 0;
        bool change = true;
        public Form2()
        {
            InitializeComponent();
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                switch(i)
                {
                    case 0:
                        pictureBox3.Location = new Point(pictureBox3.Location.X, pictureBox3.Location.Y + rainSpeeds[i]);
                        if (pictureBox3.Location.Y > panel1.Size.Height + pictureBox3.Size.Height)
                        {
                            pictureBox3.Location = new Point(pictureBox3.Location.X, 0 - pictureBox3.Size.Height);
                        }
                        break;
                    case 1:
                        pictureBox4.Location = new Point(pictureBox4.Location.X, pictureBox4.Location.Y + rainSpeeds[i]);
                        if (pictureBox4.Location.Y > panel1.Size.Height + pictureBox4.Size.Height)
                        {
                            pictureBox4.Location = new Point(pictureBox4.Location.X, 0 - pictureBox4.Size.Height);
                        }
                        break;
                    case 2:
                        pictureBox5.Location = new Point(pictureBox5.Location.X, pictureBox5.Location.Y + rainSpeeds[i]);
                        if (pictureBox5.Location.Y > panel1.Size.Height + pictureBox5.Size.Height)
                        {
                            pictureBox5.Location = new Point(pictureBox5.Location.X, 0 - pictureBox5.Size.Height);
                        }
                        break;
                    case 3:
                        pictureBox6.Location = new Point(pictureBox6.Location.X, pictureBox6.Location.Y + rainSpeeds[i]);
                        if (pictureBox6.Location.Y > panel1.Size.Height + pictureBox6.Size.Height)
                        {
                            pictureBox6.Location = new Point(pictureBox6.Location.X, 0 - pictureBox6.Size.Height);
                        }
                        break;
                    case 4:
                        pictureBox7.Location = new Point(pictureBox7.Location.X, pictureBox7.Location.Y + rainSpeeds[i]);
                        if (pictureBox7.Location.Y > panel1.Size.Height + pictureBox7.Size.Height)
                        {
                            pictureBox7.Location = new Point(pictureBox7.Location.X, 0 - pictureBox7.Size.Height);
                        }
                        break;
                    case 5:
                        pictureBox8.Location = new Point(pictureBox8.Location.X, pictureBox8.Location.Y + rainSpeeds[i]);
                        if (pictureBox8.Location.Y > panel1.Size.Height + pictureBox8.Size.Height)
                        {
                            pictureBox8.Location = new Point(pictureBox8.Location.X, 0 - pictureBox8.Size.Height);
                        }
                        break;
                    case 6:
                        pictureBox9.Location = new Point(pictureBox9.Location.X, pictureBox9.Location.Y + rainSpeeds[i]);
                        if (pictureBox9.Location.Y > panel1.Size.Height + pictureBox9.Size.Height)
                        {
                            pictureBox9.Location = new Point(pictureBox9.Location.X, 0 - pictureBox9.Size.Height);
                        }
                        break;
                    case 7:
                        pictureBox10.Location = new Point(pictureBox10.Location.X, pictureBox10.Location.Y + rainSpeeds[i]);
                        if (pictureBox10.Location.Y > panel1.Size.Height + pictureBox10.Size.Height)
                        {
                            pictureBox10.Location = new Point(pictureBox10.Location.X, 0 - pictureBox10.Size.Height);
                        }
                        break;
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
        }

        public void timer2_Tick(object sender, EventArgs e)
        {
            initialPercentage += loadingSpeed;
            float percentage = initialPercentage / pictureBox2.Height * 100;

            label1.Text = (int)percentage + " %";
            panel2.Location = new Point(panel2.Location.X, panel2.Location.Y + loadingSpeed);
            if (panel2.Location.Y > pictureBox2.Location.Y + pictureBox2.Height)
            {
                label1.Text = "100 %";
                this.timer2.Stop();
            }
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
           
        }

        public void Form2_Activated(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(2000);
                Animation();
                //timer1.Enabled = true;
                //timer2.Enabled = true;
                //timer1.Start();
                //timer2.Start();
            }

        }

        public void Animation()
        {

            initialPercentage += loadingSpeed;
            float percentage = initialPercentage / pictureBox2.Height * 100;

            label1.Text = (int)percentage + " %";
            panel2.Location = new Point(panel2.Location.X, panel2.Location.Y + loadingSpeed);
            if (panel2.Location.Y > pictureBox2.Location.Y + pictureBox2.Height)
            {
                label1.Text = "100 %";
        
            }
        }
    }
}
