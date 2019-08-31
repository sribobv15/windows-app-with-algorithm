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
    public partial class PhyView : Form
    {
        public string ExcelDataPath = @"C:\Users\user\Desktop\NeoOva Software\Cancer_Data1.xlsx";

        public string PatientID = "";
        public string CancerType = "";
        public string DoB = "";
        public double Age = 0;
        public string CA125 = "";
        public string IOTA = "";
        public double HE4 = 0;
        public bool Menopause;

        public DataTable dt;
        public List<string> sampleIDs;
        public List<DataRow> data;

        public PhyView()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            if (!(radioButton1.Checked || radioButton2.Checked))
            {
                MessageBox.Show("Please choose the patient Gender!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Enter Patient ID!");
                return;
            }

            if (radioButton1.Checked)
                Menopause = false;
            if (radioButton2.Checked)
                Menopause = true;
           
            NeoOva neo = new NeoOva();

            neo.PatientID = PatientID;
            neo.CancerType = CancerType;
            neo.Age = Age;
            neo.DoB = DoB;
            neo.CA125 = CA125;
            neo.IOTA = IOTA;
            neo.HE4 = HE4;
            neo.Menopause = Menopause;

            neo.Show();
            this.Hide();
        }

        private void PhyView_Load(object sender, EventArgs e)
        {
            //richTextBox7.BackColor = richTextBox7.Parent.BackColor; //Backcolor of the RTB's container
            //richTextBox7.BorderStyle = BorderStyle.None;
            //richTextBox7.Text = "NeoOvaEVAL";
            //richTextBox7.Font = new Font(richTextBox7.Font.FontFamily, 16);
            //richTextBox7.SelectionStart = 6;
            //richTextBox7.SelectionLength = 4;
            //richTextBox7.SelectionCharOffset = 5;
            //richTextBox7.SelectionLength = 0;

            try
            {
                dt = ConvertExcelToDataTable(ExcelDataPath);

                dt = dt.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is DBNull ||
                             string.IsNullOrWhiteSpace(field as string))).CopyToDataTable();

                data = dt.AsEnumerable().ToList();

                sampleIDs = data.Select(row => row["Sample Id"] as string).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            //IEnumerable<string> query = from dt1 in dt.AsEnumerable()
            //            select dt1.Field<string>("Sample Id");
            //sampleIDs = query.ToList();

            textBox1.KeyDown += new KeyEventHandler(tb_KeyDown);

        }

        void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //enter key is down
            {
                if (textBox1.Text == "")
                    return;

                PatientID = textBox1.Text.Trim().ToUpper();

                if (!sampleIDs.Contains(PatientID))
                {
                    MessageBox.Show("The database doesn't have this patient's data", "Data Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox3.Text = "";
                    textBox4.Text = "";
                    return;
                }

                try
                {
                    DataRow dr = dt.AsEnumerable()
                    .SingleOrDefault(r => r.Field<string>("Sample Id") == PatientID);

                    CancerType = dr.Field<string>("NeoOva score");
                    Age = dr.Field<double>("AGE");
                    DoB = dr.Field<DateTime>("Date of birth").ToString("dd/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    CA125 = dr.Field<string>("CA-125");
                    IOTA = dr.Field<string>("IOTA Score");
                    HE4 = dr.Field<double>("HE4");

                    textBox3.Text = Age.ToString();
                    textBox4.Text = DoB;

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
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

    }
}
