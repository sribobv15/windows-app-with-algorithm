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
    public partial class LoginForm : Form
    {
        public string LoginFilePath = @"C:\Users\user\Desktop\NeoOva Software\Login.xlsx";

        public string Level;
        public DataTable dt;

        public LoginForm()
        {
            InitializeComponent();
        }        

        private void Submit_Click(object sender, EventArgs e)
        {
            string Username = textBox1.Text.Trim();
            string Password = textBox2.Text.Trim();
            string Designation = Level == "Tech" ? "Technician" : "Physician";

            bool validLogin = false;

            foreach (DataRow row in dt.Rows)
            {
                if (row.Field<string>("Designation") == Designation)
                {
                    if (row.Field<string>("Username") == Username)
                    {
                        if (row.Field<string>("Password") == Password)
                        {
                            validLogin = true;
                            break;
                        }                            
                    }
                }
            }

            if (!validLogin)
            {
                MessageBox.Show("Authorization Failed!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Level == "Tech")
            {
                TechView tech = new TechView();
                tech.Show();
                this.Hide();
            }
            else
            {
                PhyView phy = new PhyView();
                phy.Show();
                this.Hide();
            }
           
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            //this.Size = new System.Drawing.Size(400, 400);
            if (Level == "Tech")
                label1.Text = "Technician Login";
            else
                label1.Text = "Physician Login";

            try
            {
                dt = ConvertExcelToDataTable(LoginFilePath);

                dt = dt.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is DBNull ||
                             string.IsNullOrWhiteSpace(field as string))).CopyToDataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
