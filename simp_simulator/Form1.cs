using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace simp_simulator
{
    public partial class Form1 : Form
    {
        int BPM = 52;
        double GSR = 1.00;

        bool BPM_up = true;
        bool GSR_up = true;

        string fileName = "values.txt";
        string csv_filename = "values.csv";
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = int.Parse(textBox3.Text);
            timer1.Enabled = true;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            
            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                if (File.Exists(csv_filename))
                {
                    File.Delete(csv_filename);
                }

                if (GSR >= double.Parse(textBox7.Text.Replace('.', ',')))
                {
                    GSR_up = false;
                }

                if (GSR <= double.Parse(textBox4.Text.Replace('.', ',')))
                {
                    GSR_up = true;
                }

                if (BPM >= double.Parse(textBox6.Text.Replace('.', ',')))
                {
                    BPM_up = false;
                }
                if (BPM <= double.Parse(textBox5.Text.Replace('.', ',')))
                {
                    BPM_up= true;
                }

                try
                {
                    if (GSR_up)
                    {
                        GSR += double.Parse(textBox1.Text.Replace('.', ','));
                    }
                    else
                    {
                        GSR -= double.Parse(textBox1.Text.Replace('.', ','));
                    }

                    if (BPM_up)
                    {
                        BPM += int.Parse(textBox2.Text.Replace('.', ','));
                    }
                    else
                    {
                        BPM -= int.Parse(textBox2.Text.Replace('.', ','));
                    }
                }
                catch
                {

                }

                Product product = new Product
                {
                    gsr = GSR,
                    bpm = BPM,
                };

                string json = JsonConvert.SerializeObject(product, Formatting.Indented);
                string csv = string.Format("GSR;{0};\r\nBPM;{1};\r\n", GSR, BPM);
                label3.Text = json;
                label11.Text = csv;

                StreamWriter Tex;
                Tex = new System.IO.StreamWriter(fileName, true);
                Tex.WriteLine(json);
                Tex.Close();
                this.Refresh();

                Tex = new System.IO.StreamWriter(csv_filename, true);
                Tex.WriteLine(csv);
                Tex.Close();
                this.Refresh();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }

        }
        internal class Product
        {
            public int bpm { get; set; }
            public double gsr { get; set; }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
