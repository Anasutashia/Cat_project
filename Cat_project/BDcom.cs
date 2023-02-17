using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cat_project
{
    class BDcom
    {

        public static SqlConnection GetConnection()
        {

            string pathFile = Application.StartupPath + "\\" + "dbCat.mdf";
            string connSql = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=" + pathFile + ";Integrated Security=True";
            SqlConnection conectar = new SqlConnection(connSql);
            try
            {
                conectar.Open();
            }
            catch (SqlException ex)
            {
                int errorecode = ex.Number;
                MessageBox.Show(ex.Message + "№ ошибки " + errorecode);
            }
            return conectar;

        }

        public static SqlDataReader reader(string _string, SqlConnection connection)

        {
            SqlCommand _commando = new SqlCommand(_string, connection);
            SqlDataReader _reader = _commando.ExecuteReader();
            return _reader;
        }

        public static string GetSqlString(string _string)
        {
            SqlConnection connection = GetConnection();
            SqlDataReader _reader = reader(_string, connection);
            string _return = "0";
            try
            {
                while (_reader.Read())
                {
                    _return = _reader.IsDBNull(0) ? _string = "0" : _reader.GetString(0);
                }
                connection.Close();
            }
            catch (SqlException ex)
            {
                int errorecode = ex.Number;
                MessageBox.Show(ex.Message + "№ ошибки" + errorecode);
            }
            return _return;

        }

        public static int GetsqlInt(string _string)
        { return Convert.ToInt32(GetSqlString(_string)); }
        public static int SendComandMSql(string _string)
        {
            int retur = 0;
            SqlConnection connection = BDcom.GetConnection();
            SqlCommand command = new SqlCommand(_string, connection);
            try
            {
                retur = command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException ex)
            {

                int errorecode = ex.Number;
                MessageBox.Show(ex.Message + "№ ошибки" + errorecode);
            }
            return retur;

        }
        public static DataTable GetSqlTable(string _string)
        {
            SqlConnection connection = GetConnection();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(_string, connection);
            SqlCommandBuilder commanBuilder = new SqlCommandBuilder(dataAdapter);

            DataTable reporte = new DataTable();
            try
            {
                reporte.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(reporte);
                connection.Close();
            }
            catch (SqlException ex)
            {
                int errorecode = ex.Number;
                MessageBox.Show(ex.Message + "№ ошибки" + errorecode);
            }
            return reporte;
        }
    }
}
