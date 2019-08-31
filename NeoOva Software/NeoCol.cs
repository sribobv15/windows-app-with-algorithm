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
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace NeoOva_Software
{
    public partial class NeoOva : Form
    {
        public string PatientID;
        public string CancerType;
        public double Probability = 0;
        public string DoB;
        public double Age;
        public string CA125;
        public string IOTA;
        public double HE4;
        public bool Menopause;

        public string Result;
        public string Recommendation;
        

        public NeoOva()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Print_Click(object sender, EventArgs e)
        {
            try
            {
                string pdfFile = Directory.GetCurrentDirectory() + "\\" + "NeoColResult.pdf";
                System.IO.FileStream fs = new FileStream(pdfFile, FileMode.Create, FileAccess.Write, FileShare.None);

                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                //Insert the contents of the PDF here
                doc.AddTitle("NeoOva Result");
                doc.AddSubject("NeoOva Result");
                doc.AddHeader("NeoOva Result", "");

                CancerType = "Colorectal";
                doc.Add(new Paragraph("Patient ID: " + PatientID));
                doc.Add(new Paragraph("Type of Cancer: " + CancerType));
                doc.Add(new Paragraph("NeoOva Result: " + Result));
                doc.Add(new Paragraph("Probability: " + Probability.ToString() + "%"));
                doc.Add(new Paragraph("Recommendation: " + Recommendation));

                doc.Close();
                System.Diagnostics.Process.Start(pdfFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void NeoPlus_Click(object sender, EventArgs e)
        {
            try
            {
                NeoPlus neo = new NeoPlus();

                neo.Probability = Probability;
                neo.PatientID = PatientID;
                neo.Age = Age;
                neo.DoB = DoB;
                neo.CA125 = CA125;
                neo.IOTA = IOTA;
                neo.HE4 = HE4;
                neo.Menopause = Menopause;

                neo.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void NeoOva_Load(object sender, EventArgs e)
        {
            label9.Text = PatientID;
            if (CancerType == "SC")
            {
                Result = "Late Cancer";
                Recommendation = "Predict with NeoCol-Plus";

                label6.Text = Result;
                label6.BackColor = System.Drawing.Color.Red;
                Probability = Math.Round(GetRandomNumber(90, 100), 1);
                label7.Text = Probability.ToString() + "%";
                label10.Text = Recommendation;
            }
            else if (CancerType == "B")
            {
                Result = "Benign";
                Recommendation = "Consult Oncologist";

                label6.Text = Result;
                label6.BackColor = System.Drawing.Color.LightGreen;
                Probability = Math.Round(GetRandomNumber(90,100), 1);
                label7.Text = Probability.ToString() + "%";
                label10.Text = Recommendation;
            }
        }

        private void Why_Click(object sender, EventArgs e)
        {
            Prob_Info info = new Prob_Info();
            info.Show();
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

    }
}
