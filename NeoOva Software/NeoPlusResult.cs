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
    public partial class NeoPlusResult : Form
    {
        public string PatientID;
        public NeoPlusResult()
        {
            InitializeComponent();
        }

        private void Print_Click(object sender, EventArgs e)
        {
            try
            {
                string pdfFile = Directory.GetCurrentDirectory() + "\\" + "NeoColPlusResult.pdf";
                System.IO.FileStream fs = new FileStream(pdfFile, FileMode.Create, FileAccess.Write, FileShare.None);

                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                //Insert the contents of the PDF here
                //doc.AddTitle("NeoOva Result");
                //doc.AddSubject("NeoOva Result");
                //doc.AddHeader("NeoOva Result", "");


                doc.Add(new Paragraph("Patient ID: " + PatientID));
                doc.Add(new Paragraph("Type of Cancer: " + "Colorectal"));
                doc.Add(new Paragraph("NeoOva Result: " + "Late Cancer"));
                doc.Add(new Paragraph("Risk Score: " + "95%"));
                doc.Add(new Paragraph("Recommendation: " + "Consult Oncologist"));

                doc.Close();
                System.Diagnostics.Process.Start(pdfFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NeoPlusResult_Load(object sender, EventArgs e)
        {
            label10.Text = PatientID;
            //richTextBox7.BackColor = richTextBox7.Parent.BackColor; //Backcolor of the RTB's container
            //richTextBox7.BorderStyle = BorderStyle.None;
            //richTextBox7.Text = "NeoOvaEVAL + Result";
            //richTextBox7.Font = new Font(richTextBox7.Font.FontFamily, 16);
            //richTextBox7.SelectionStart = 6;
            //richTextBox7.SelectionLength = 4;
            //richTextBox7.SelectionCharOffset = 5;
            //richTextBox7.SelectionLength = 0;
        }
    }
}
