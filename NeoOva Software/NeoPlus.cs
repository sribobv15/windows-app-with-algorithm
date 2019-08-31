using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeoOva_Software
{
    public partial class NeoPlus : Form
    {
        public string PatientID;
        public string DoB;
        public double Age;
        public double Probability;
        public string CA125;
        public string IOTA;
        public double HE4;
        public bool Menopause;

        public NeoPlus()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Predict_Click(object sender, EventArgs e)
        {
            NeoPlusResult neoresult = new NeoPlusResult();
            neoresult.PatientID = PatientID;
            neoresult.Show();
            this.Hide();
        }

        private void NeoPlus_Load(object sender, EventArgs e)
        { 
            textBox1.Text = PatientID;
            textBox4.Text = DoB;
            textBox3.Text = Age.ToString();
            textBox2.Text = Probability.ToString() + "%";
            textBox5.Text = CA125;
            textBox6.Text = IOTA;
            textBox7.Text = HE4.ToString();

            if (Menopause)
                radioButton2.Checked = true;
            else
                radioButton1.Checked = true;
        }


    }
}
