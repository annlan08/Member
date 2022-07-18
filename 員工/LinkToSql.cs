using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Windows.Forms;

namespace 員工
{
    internal class LinkToSql
    {
        private string _SqlAdress= @"Data Source=.;Initial Catalog=Member;Integrated Security=True";
        public string SqlAdress
        {
            get { return _SqlAdress; }
            set { _SqlAdress = value; }
        }
        public string SqlCode { get; set; }

        public void fn_ExecuteSQL(SqlConnection con)
        {
            if (!fn_IsConnectionWork) { return; }
            con.ConnectionString = SqlAdress;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = SqlCode;
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public void fn_ExecuteSQL(SqlConnection con, SqlParameter Para) //todo which is batter list or array?
        {
            if (!fn_IsConnectionWork) { return; }
            con.ConnectionString = SqlAdress;
            con.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add(Para);

            cmd.Connection = con;
            cmd.CommandText = SqlCode;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void fn_ExecuteSQL(SqlConnection con, SqlParameter[] ParaArray) //todo which is batter list or array?
        {
            if (!fn_IsConnectionWork) { return; }
            con.ConnectionString = SqlAdress;
            con.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.AddRange(ParaArray);

            cmd.Connection = con;
            cmd.CommandText = SqlCode;
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public SqlDataReader fn_ReadSQLData(SqlConnection con)
        {
            if (!fn_IsConnectionWork) { return null; } //can type SqlDataReader return null? using reader.Read()==false
            con.ConnectionString = SqlAdress; // Is $@"" true? true but I got better one
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = SqlCode;
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public SqlDataReader fn_ReadSQLData(SqlConnection con, SqlParameter Para)
        {
            if (!fn_IsConnectionWork) { return null; } //can type SqlDataReader return null? using reader.Read() = false
            con.ConnectionString = SqlAdress; //Is $@"" true? true but I got better one
            con.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add(Para);

            cmd.Connection = con;
            cmd.CommandText = SqlCode;
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public SqlDataReader fn_ReadSQLData(SqlConnection con, SqlParameter[] ParaArray)
        {
            if (!fn_IsConnectionWork) { return null; } //can type SqlDataReader return null? using reader.Read() = false
            con.ConnectionString = SqlAdress; //Is $@"" true? true but I got better one
            con.Open();
            SqlCommand cmd = new SqlCommand();
            /*---------------------------------*/
            foreach (SqlParameter item in ParaArray)
            {
                cmd.Parameters.Add(item);
            }
            /*--------------------------------*/
            cmd.Connection = con;
            cmd.CommandText = SqlCode;
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }



        private bool fn_IsConnectionWork
        {
            get 
            {
                if (SqlAdress == "" || SqlCode == "") { MessageBox.Show("未取得地址或編碼"); return false; }
                return true;
            }
        }
    }
}
