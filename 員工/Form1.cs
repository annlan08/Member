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
    public partial class Form1 : Form
    {
        List<string> Pk_phone = new List<string>(); 

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            MemberData M = new MemberData();
            fn_EnterValue(M);
            if (!fn_CheckPhoneIsEmpty(M)) { MessageBox.Show("會員電話已被使用"); return; }//檢查號碼重複
            
            using (SqlConnection con = new SqlConnection())
            {
                LinkToSql SQL = new LinkToSql();
                SQL.SqlCode = $"INSERT INTO [dbo].[Member_Info](" +
                              $"[Member_Name]," +
                              $"[Member_BirthDate]," +
                              $"[Member_Phone]," +
                              $"[Member_Email]," +
                              $"[Member_Password]) VALUES ";
                SQL.SqlCode += "(@K_NAME,@K_BirthDate,@K_Phone,@K_Email,@K_Password)";
                SqlParameter[] para = new SqlParameter[5];
                para[0] = new SqlParameter("@K_NAME", M.Name);
                para[1] = new SqlParameter("@K_BirthDate", M.BirthDate);
                para[2] = new SqlParameter("@K_Phone", M.Phone);
                para[3] = new SqlParameter("@K_Email", M.Email);
                para[4] = new SqlParameter("@K_Password", M.Password);
                SQL.fn_ExecuteSQL(con, para);
                MessageBox.Show("新增成功");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Form登入視窗 F = new Form登入視窗();
            F.ShowDialog();
            if (!(F.IsPassOK)) { return; }   // bool何時寫入 after F.Close()
            using (SqlConnection con = new SqlConnection())
            {
                LinkToSql SQL = new LinkToSql();
                SQL.SqlCode = $"DELETE FROM [dbo].[Member_Info]WHERE [Member_Phone]='{F.phone}'";
                SQL.fn_ExecuteSQL(con);
                MessageBox.Show($"會員{F.name}已刪除");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Form登入視窗 F = new Form登入視窗();
            F.ShowDialog();
            if (!(F.IsPassOK)) { return; }
            using (SqlConnection con = new SqlConnection())
            {
                LinkToSql SQL = new LinkToSql();
                SQL.SqlCode = $"SELECT * FROM [dbo].[Member_Info] WHERE [Member_Phone]='{F.phone}'";
                SqlDataReader reader = SQL.fn_ReadSQLData(con);
                if(reader.Read())
                {
                    MemberID = reader[0].ToString();
                    txtName.Text = reader[1].ToString();
                    txtBirthDate.Text = reader[2].ToString();
                    txtPhone.Text = reader[3].ToString();
                    txtEmail.Text = reader[4].ToString();
                    txtPassword.Text=reader[5].ToString();
                    MessageBox.Show("請輸入新資料");
                }
            }
            /*------------------------------------------*///todo #08 新增一個按鈕 其餘關閉(事件)
            foreach(Control item in this.panel1.Controls)
            {
                if(item is Button) { item.Enabled = false; item.Visible = false; }
            }
            Button btn_Update = new Button();
            ButtonSave = btn_Update;
            btn_Update.Text = "更新";
            btn_Update.Location = new Point(45, 363);
            btn_Update.Size = new Size(121, 48);
            btn_Update.Click += Btn_Update_Click;
            this.panel1.Controls.Add(btn_Update);
            /*------------------------------------------*/
        }

        private string MemberID;    //  //todo 全域變數很邪惡
        private Button ButtonSave = null;

        private void Btn_Update_Click(object sender, EventArgs e)
        {
            MemberData M = new MemberData();
            fn_EnterValue(M);
            if (!fn_CheckPhoneIsEmpty(M)) { MessageBox.Show("會員電話已被使用"); return; }//檢查號碼重複
            using (SqlConnection con=new SqlConnection())
            {
                LinkToSql SQL = new LinkToSql();
                SQL.SqlCode = $"UPDATE [dbo].[Member_Info] " +
                              $"SET [Member_Name] = @K_NAME ," +
                              $"[Member_BirthDate] = @K_BirthDate," +
                              $"[Member_Phone] = @K_Phone," +
                              $"[Member_Email] =@K_Email," +
                              $"[Member_Password] = @K_Password" +
                              $" WHERE [Member_ID]={MemberID}";
                SqlParameter[] para = new SqlParameter[5];
                para[0] = new SqlParameter("@K_NAME", M.Name);
                para[1] = new SqlParameter("@K_BirthDate", M.BirthDate);
                para[2] = new SqlParameter("@K_Phone", M.Phone);
                para[3] = new SqlParameter("@K_Email", M.Email);
                para[4] = new SqlParameter("@K_Password", M.Password);
                SQL.fn_ExecuteSQL(con, para);
                MessageBox.Show("更新成功");
            }
            foreach (Control item in this.panel1.Controls)
            {
                if (item is Button) { item.Enabled = true; item.Visible = true; }
            }
            this.panel1.Controls.Remove(ButtonSave);
        }

        private void button4_Click(object sender, EventArgs e)
        {           
            listBox1.Items.Clear();
            Pk_phone.Clear();
            using (SqlConnection con = new SqlConnection())
            {
                LinkToSql SQL = new LinkToSql();
                SQL.SqlCode= $"SELECT * FROM [dbo].[Member_Info] ";
                SqlDataReader reader = SQL.fn_ReadSQLData(con);
                while(reader.Read())
                {
                    listBox1.Items.Add(reader[1].ToString());
                    Pk_phone.Add(reader[3].ToString());
                }
            }
                
           


        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Pk_phone.Clear();
            Form_搜尋視窗 F = new Form_搜尋視窗();
            F.ShowDialog();
            if (!F.IsOkClick) { return; }
            using (SqlConnection con=new SqlConnection())
            {
                LinkToSql SQL = new LinkToSql();
                SQL.SqlCode = $"SELECT * FROM [dbo].[Member_Info] " +
                              $"WHERE ([Member_Name] LIKE @K_KeyWord )" +
                              $" OR   ([Member_Email] LIKE @K_KeyWord )";
                SqlParameter[] para = new SqlParameter[1] { new SqlParameter("@K_KeyWord",$"%{F.KeyWord}%") };
                SqlDataReader reader = SQL.fn_ReadSQLData(con,para);
                while(reader.Read())
                {
                    listBox1.Items.Add(reader[1].ToString());
                    Pk_phone.Add(reader[3].ToString());
                }
            }         
        }

        private void fn_EnterValue(MemberData M)
        {
            M.Name = txtName.Text;
            M.BirthDate = txtBirthDate.Text;
            M.Phone = txtPhone.Text;
            M.Email = txtEmail.Text;
            M.Password = txtPassword.Text;          
        }

        private bool fn_CheckPhoneIsEmpty(MemberData M)//檢查號碼重複
        {
            using (SqlConnection con = new SqlConnection())
            {
                LinkToSql SQL = new LinkToSql();
                SQL.SqlCode = $"SELECT * FROM [dbo].[Member_Info] WHERE [Member_Phone] =@K_Phone";
                SqlParameter para = new SqlParameter("@K_Phone", M.Phone);
                SqlDataReader reader = SQL.fn_ReadSQLData(con, para);
                if (reader.Read()) { return false; }
                else { return true; }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = listBox1.SelectedIndex;           
            using (SqlConnection con=new SqlConnection())
            {
                LinkToSql SQL = new LinkToSql();
                SQL.SqlCode= $"SELECT * FROM [dbo].[Member_Info] WHERE [Member_Phone]='{Pk_phone[Index]}'";
                SqlDataReader reader = SQL.fn_ReadSQLData(con);
                reader.Read();
                txtName.Text = reader[1].ToString();
                txtBirthDate.Text = reader[2].ToString();
                txtPhone.Text = reader[3].ToString();
                txtEmail.Text = reader[4].ToString();
                txtPassword.Text = reader[5].ToString();
                con.Close();
            }
        }
    }
}
