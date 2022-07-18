using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 員工
{
    public partial class Form登入視窗 : Form
    {
        private bool _IsPassOK = false;
        public  bool IsPassOK
        {
            get { return _IsPassOK; }
        }
        public string phone { get; set; }
        public string name { get; set; }

        public Form登入視窗()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(SqlConnection con=new SqlConnection())   
            {
                LinkToSql SQL = new LinkToSql();
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter("@K_Phone", txtPhone.Text);  //"@K" or "K"
                SQL.SqlCode = "SELECT * FROM [dbo].[Member_Info] WHERE [Member_Phone]=@K_Phone";
                SqlDataReader reader = SQL.fn_ReadSQLData(con, para);

                if (!reader.Read()) { MessageBox.Show("無會員資料"); return; }
                if (reader[5].ToString() != txtPassword.Text) { MessageBox.Show("密碼錯誤"); return; }//傳回值有空白 nchar

                phone = txtPhone.Text;
                name = reader[1].ToString();
                _IsPassOK = true;
                this.Close();
            }
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8)))
            {
                e.Handled = true;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') ||
                  (e.KeyChar >= 'a' && e.KeyChar <= 'z') ||
                  (e.KeyChar >= 'A' && e.KeyChar <= 'Z') ||
                  (e.KeyChar == 8)))
            {
                e.Handled = true;
            }
        }
    }
}
