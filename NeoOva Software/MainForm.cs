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
    public partial class MainForm : Form
    {
        public string Level;
        

        public object Provider { get; private set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Level = "";

            //richTextBox1.BackColor = richTextBox1.Parent.BackColor; //Backcolor of the RTB's container
            //richTextBox1.BorderStyle = BorderStyle.None;
            //richTextBox1.Text = "NeoOvaEVAL";
            //richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, 16);
            //richTextBox1.SelectionStart = 6;
            //richTextBox1.SelectionLength = 4;
            //richTextBox1.SelectionCharOffset = 5;
            //richTextBox1.SelectionLength = 0;

            

        }

        private void Tech_Login_Click(object sender, EventArgs e)
        {
            Level = "Tech";
            LoginForm login = new LoginForm();
            login.Level = Level;
            login.Show();
            this.Hide();

        }

        private void Physician_Login_Click(object sender, EventArgs e)
        {
            Level = "Phy";
            LoginForm login = new LoginForm();
            login.Level = Level;
            login.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //string sSheetName = null;
        //string sConnection = null;
        //DataTable dtTablesList = default(DataTable);
        //System.Data.OleDb.OleDbCommand oleExcelCommand = default(System.Data.OleDb.OleDbCommand);
        //System.Data.OleDb.OleDbDataReader oleExcelReader = default(System.Data.OleDb.OleDbDataReader);
        //System.Data.OleDb.OleDbConnection oleExcelConnection = default(System.Data.OleDb.OleDbConnection);

        //sConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Test.xls;Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";

        //oleExcelConnection = new System.Data.OleDb.OleDbConnection(sConnection);
        //oleExcelConnection.Open();

        //dtTablesList = oleExcelConnection.GetSchema("Tables");

        //if (dtTablesList.Rows.Count > 0)
        //{
        //    sSheetName = dtTablesList.Rows[0]["TABLE_NAME"].ToString();
        //}

        //dtTablesList.Clear();
        //dtTablesList.Dispose();


        //if (!string.IsNullOrEmpty(sSheetName))
        //{
        //    oleExcelCommand = oleExcelConnection.CreateCommand();
        //    oleExcelCommand.CommandText = "Select * From [" + sSheetName + "]";
        //    oleExcelCommand.CommandType = CommandType.Text;
        //    oleExcelReader = oleExcelCommand.ExecuteReader();

        //    while (oleExcelReader.Read())
        //    {
        //    }
        //    oleExcelReader.Close();
        //}
        //oleExcelConnection.Close();

        //string strProvider = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\user\Desktop\NeoOva Software\Cancer_Data.accdb";
        //string strSql = "Select * from Cancer_Data";
        //System.Data.OleDb.OleDbConnection con = new System.Data.OleDb.OleDbConnection(strProvider);
        //System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(strSql, con);
        //con.Open();
        //    cmd.CommandType = CommandType.Text;
        //    System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(cmd);
        //DataTable scores = new DataTable();
        //da.Fill(scores);
    }
}
