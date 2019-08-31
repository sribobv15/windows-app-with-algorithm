using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace NeoOva_Software
{
    public partial class TechView : Form
    {
        public string ExcelDataPath = @"C:\Users\user\Desktop\NeoOva Software\Cancer_Data1.xlsx";

        private static Microsoft.Office.Interop.Excel.Workbook mWorkBook;
        private static Microsoft.Office.Interop.Excel.Sheets mWorkSheets;
        private static Microsoft.Office.Interop.Excel.Worksheet mWSheet1;
        private static Microsoft.Office.Interop.Excel.Application oXL;
        public string SampleID = "";
        public DateTime SampleDate;
        public string PatientID = "";
        public double Age = 0;
        public double[] BioData = new double[6];

        public DataTable dt;
        public int rowCount = 0;

        public TechView()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Sample ID!", "Sample ID missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //if (textBox2.Text.Trim() == "")
            //{
            //    MessageBox.Show("Please Enter Sample Date!");
            //    return;
            //}

            //if (textBox3.Text.Trim() == "")
            //{
            //    MessageBox.Show("Please Enter Patient AGE!");
            //    return;
            //}

            if (!Double.TryParse(textBox3.Text, out Age))
            {
                MessageBox.Show("Enter AGE value in number!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox4.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Patient ID!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox5.Text.Trim() == "" || textBox6.Text.Trim() == "" || textBox7.Text.Trim() == "" || textBox8.Text.Trim() == "" || textBox9.Text.Trim() == "" || textBox10.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter All the BioMarkers Values!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SampleID = textBox1.Text.Trim().ToUpper();
            SampleDate = dateTimePicker1.Value.Date;
            PatientID = textBox4.Text.Trim();

            if (!Double.TryParse(textBox5.Text, out BioData[0]))
            {
                MessageBox.Show("Enter BioMarker1 value in number!");
                return;
            }

            if (!Double.TryParse(textBox6.Text, out BioData[1]))
            {
                MessageBox.Show("Enter BioMarker2 value in number!");
                return;
            }

            if (!Double.TryParse(textBox7.Text, out BioData[2]))
            {
                MessageBox.Show("Enter BioMarker3 value in number!");
                return;
            }

            if (!Double.TryParse(textBox8.Text, out BioData[3]))
            {
                MessageBox.Show("Enter BioMarker4 value in number!");
                return;
            }

            if (!Double.TryParse(textBox9.Text, out BioData[4]))
            {
                MessageBox.Show("Enter BioMarker5 value in number!");
                return;
            }

            if (!Double.TryParse(textBox10.Text, out BioData[5]))
            {
                MessageBox.Show("Enter BioMarker6 value in number!");
                return;
            }

            try
            {
                AddDataToExcel(SampleID, SampleDate, PatientID, Age, BioData, rowCount, ExcelDataPath);
                MessageBox.Show("The Sample data has been saved succesfully in the database!");
                textBox1.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public static void AddDataToExcel(string SampleID, DateTime SampleDate, string PatientID, double Age, double[] BioData, int rowCount, string FilePath)
        {
            string path = FilePath;

            //xlApp = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel.Workbook excelWorkbook = xlApp.Workbooks.Open(path);
            //Microsoft.Office.Interop.Excel._Worksheet sheet = excelWorkbook.Sheets[1];
            //TrimRows(sheet);
            //var LastRow = sheet.UsedRange.Rows.Count;
            //LastRow = LastRow + sheet.UsedRange.Row - 1;
            //for (int i = 1; i <= LastRow; i++)
            //{
            //    //if (application.WorksheetFunction.CountA(sheet.Rows[i]) == 0)
            //    //    (sheet.Rows[i] as Microsoft.Office.Interop.Excel.Range).Delete();
            //}

            oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = false;
            oXL.DisplayAlerts = false;
            mWorkBook = oXL.Workbooks.Open(path, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            //Get all the sheets in the workbook
            mWorkSheets = mWorkBook.Worksheets;
            //Get the allready exists sheet
            mWSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)mWorkSheets.get_Item("biomarkers + added parameters");

            //TrimRows(mWSheet1);

            //Microsoft.Office.Interop.Excel.Range range = mWSheet1.UsedRange;
            //int colCount = range.Columns.Count;
            //int rowCount = range.Rows.Count;
            rowCount = rowCount + 1;

            mWSheet1.Cells[rowCount + 1, 1] = rowCount;       //DigiWest ID
            mWSheet1.Cells[rowCount + 1, 2] = SampleID;       //SampleID
            mWSheet1.Cells[rowCount + 1, 3] = "";             //Patient ID IOTA
            mWSheet1.Cells[rowCount + 1, 4] = "";             //IOTA Score
            mWSheet1.Cells[rowCount + 1, 5] = "";             //Certain or Uncertain
            mWSheet1.Cells[rowCount + 1, 6] = SampleDate;     //SampleDate
            mWSheet1.Cells[rowCount + 1, 7] = "";             //DateOfBirth
            mWSheet1.Cells[rowCount + 1, 8] = Age;            //Age
            mWSheet1.Cells[rowCount + 1, 9] = "";             //CA-125
            mWSheet1.Cells[rowCount + 1, 10] = "";            //NeoOva Score
            mWSheet1.Cells[rowCount + 1, 11] = BioData[0];    //Neopro1
            mWSheet1.Cells[rowCount + 1, 12] = BioData[1];    //Neopro2
            mWSheet1.Cells[rowCount + 1, 13] = BioData[2];    //Neopro3
            mWSheet1.Cells[rowCount + 1, 14] = BioData[3];    //Neopro4
            mWSheet1.Cells[rowCount + 1, 15] = BioData[4];    //Neopro5
            mWSheet1.Cells[rowCount + 1, 16] = BioData[5];    //Neopro6
            mWSheet1.Cells[rowCount + 1, 17] = PatientID;     //PatientID


            mWorkBook.SaveAs(path, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook,
            Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive,
            Missing.Value, Missing.Value, Missing.Value,
            Missing.Value, Missing.Value);
            mWorkBook.Close(Missing.Value, Missing.Value, Missing.Value);
            mWSheet1 = null;
            mWorkBook = null;
            oXL.Quit();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public static void TrimRows(Excel.Worksheet worksheet)
        {
            Excel.Range range = worksheet.UsedRange;
            while (worksheet.Application.WorksheetFunction.CountA(range.Rows[range.Rows.Count]) == 0)
                (range.Rows[range.Rows.Count] as Excel.Range).Delete();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TechView_Load(object sender, EventArgs e)
        {
            //dateTimePicker1.Size = new Size(148, 30);
            //richTextBox1.BackColor = richTextBox1.Parent.BackColor; //Backcolor of the RTB's container
            //richTextBox1.BorderStyle = BorderStyle.None;
            //richTextBox1.Text = "NeoOvaBM1 :";
            //richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, 8);
            //richTextBox1.SelectionStart = 6;
            //richTextBox1.SelectionLength = 3;
            //richTextBox1.SelectionCharOffset = 5;
            //richTextBox1.SelectionLength = 0;

            //richTextBox2.BackColor = richTextBox2.Parent.BackColor; //Backcolor of the RTB's container
            //richTextBox2.BorderStyle = BorderStyle.None;
            //richTextBox2.Text = "NeoOvaBM2 :";
            //richTextBox2.Font = new Font(richTextBox2.Font.FontFamily, 8);
            //richTextBox2.SelectionStart = 6;
            //richTextBox2.SelectionLength = 3;
            //richTextBox2.SelectionCharOffset = 5;
            //richTextBox2.SelectionLength = 0;

            //richTextBox3.BackColor = richTextBox3.Parent.BackColor; //Backcolor of the RTB's container
            //richTextBox3.BorderStyle = BorderStyle.None;
            //richTextBox3.Text = "NeoOvaBM3 :";
            //richTextBox3.Font = new Font(richTextBox3.Font.FontFamily, 8);
            //richTextBox3.SelectionStart = 6;
            //richTextBox3.SelectionLength = 3;
            //richTextBox3.SelectionCharOffset = 5;
            //richTextBox3.SelectionLength = 0;

            //richTextBox4.BackColor = richTextBox4.Parent.BackColor; //Backcolor of the RTB's container
            //richTextBox4.BorderStyle = BorderStyle.None;
            //richTextBox4.Text = "NeoOvaBM4 :";
            //richTextBox4.Font = new Font(richTextBox4.Font.FontFamily, 8);
            //richTextBox4.SelectionStart = 6;
            //richTextBox4.SelectionLength = 3;
            //richTextBox4.SelectionCharOffset = 5;
            //richTextBox4.SelectionLength = 0;

            //richTextBox5.BackColor = richTextBox5.Parent.BackColor; //Backcolor of the RTB's container
            //richTextBox5.BorderStyle = BorderStyle.None;
            //richTextBox5.Text = "NeoOvaBM5 :";
            //richTextBox5.Font = new Font(richTextBox5.Font.FontFamily, 8);
            //richTextBox5.SelectionStart = 6;
            //richTextBox5.SelectionLength = 3;
            //richTextBox5.SelectionCharOffset = 5;
            //richTextBox5.SelectionLength = 0;

            //richTextBox6.BackColor = richTextBox6.Parent.BackColor; //Backcolor of the RTB's container
            //richTextBox6.BorderStyle = BorderStyle.None;
            //richTextBox6.Text = "NeoOvaBM6 :";
            //richTextBox6.Font = new Font(richTextBox6.Font.FontFamily, 8);
            //richTextBox6.SelectionStart = 6;
            //richTextBox6.SelectionLength = 3;
            //richTextBox6.SelectionCharOffset = 5;
            //richTextBox6.SelectionLength = 0;

            //richTextBox7.BackColor = richTextBox7.Parent.BackColor; //Backcolor of the RTB's container
            //richTextBox7.BorderStyle = BorderStyle.None;
            //richTextBox7.Text = "NeoOvaEVAL";
            //richTextBox7.Font = new Font(richTextBox7.Font.FontFamily, 16);
            //richTextBox7.SelectionStart = 6;
            //richTextBox7.SelectionLength = 4;
            //richTextBox7.SelectionCharOffset = 5;
            //richTextBox7.SelectionLength = 0;


            dt = ConvertExcelToDataTable(ExcelDataPath);

            dt = dt.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is DBNull ||
                         string.IsNullOrWhiteSpace(field as string))).CopyToDataTable();

            rowCount = dt.Rows.Count;
        }

        public static DataTable ConvertExcelToDataTable(string FileName)
        {
            DataTable dtResult = null;
            int totalSheet = 0; //No of sheets on excel file  
            using (System.Data.OleDb.OleDbConnection objConn = new System.Data.OleDb.OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';"))
            {
                objConn.Open();
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
                System.Data.OleDb.OleDbDataAdapter oleda = new System.Data.OleDb.OleDbDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = objConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
                if (dt != null)
                {
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                                         where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                         select dataRow).CopyToDataTable();
                    dt = tempDataTable;
                    totalSheet = dt.Rows.Count;
                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                }
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                oleda = new System.Data.OleDb.OleDbDataAdapter(cmd);
                oleda.Fill(ds, "excelData");
                dtResult = ds.Tables["excelData"];
                objConn.Close();
                return dtResult;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
    }
