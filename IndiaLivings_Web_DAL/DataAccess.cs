using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL
{
    public class DataAccess
    {
        
        const string strConnectionString = "Data Source=195.201.83.144;Initial Catalog=indialiv_IndiaLiving;Persist Security Info=True;User ID=indialivings;Password=indliv@123;Pooling=False";
        private SqlConnection _connection;
        private string _connectionString;
        private SqlCommand _command;
        public SqlConnection Connection() { return _connection; }

        public DataAccess(string strDBName)
        {
            try
            {
                _connectionString = GetConnectionString();
                _connection = new SqlConnection(_connectionString);
            }
            catch { throw; }
        }
        private string GetConnectionString()
        {
            try
            {
                return strConnectionString;
            }
            catch { throw; }
        }
        public void InitializeParameterList()
        {
            _command = new SqlCommand();
        }
        public void AddParameter(string strParameterName, object strParameterValue, ParameterDirection parameterDirection)
        {
            if (_command == null)
            {
                _command = new SqlCommand();
            }
            _command.Parameters.AddWithValue(strParameterName, strParameterValue);
            _command.Parameters[strParameterName].Direction = parameterDirection;
        }

        public object GetScalar(string strStoreProcName)
        {
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlconn = new SqlConnection(_connectionString);
            try
            {
                if (sqlconn.State != ConnectionState.Open)
                {
                    sqlconn.Open();
                }
                if (_command == null)
                {
                    sqlcmd = new SqlCommand();
                }
                else
                {
                    sqlcmd = _command;
                    _command = null;
                }
                sqlcmd.Connection = sqlconn;
                sqlcmd.CommandText = strStoreProcName;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                return sqlcmd.ExecuteScalar();
            }
            catch { throw; }
            finally
            {
                if (sqlconn.State == ConnectionState.Open)
                {
                    sqlconn.Close();
                }
                sqlcmd = null;
                sqlconn = null;
            }

        }

        public DataSet GetDataSet(string strStoreProcName)
        {
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlconn = new SqlConnection(_connectionString);
            DataSet _ds = new DataSet();
            try
            {
                if (sqlconn.State != ConnectionState.Open)
                {
                    sqlconn.Open();
                }
                if (_command == null)
                {
                    sqlcmd = new SqlCommand();
                }
                else
                {
                    sqlcmd = _command;
                    _command = null;
                }
                sqlcmd.Connection = sqlconn;
                sqlcmd.CommandText = strStoreProcName;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter _da = new SqlDataAdapter(sqlcmd);
                _da.Fill(_ds);
                _da = null;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (sqlconn.State == ConnectionState.Open)
                {
                    sqlconn.Close();
                }
                sqlcmd = null;
                sqlconn = null;
            }
            return _ds;
        }

        public SqlDataReader GetDataReader(string strStoreProcName)
        {
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlconn = new SqlConnection(_connectionString);
            try
            {
                if (sqlconn.State != ConnectionState.Open)
                {
                    sqlconn.Open();
                }
                if (_command == null)
                {
                    sqlcmd = new SqlCommand();
                }
                else
                {
                    sqlcmd = _command;
                    _command = null;
                }
                sqlcmd.Connection = sqlconn;
                sqlcmd.CommandText = strStoreProcName;
                sqlcmd.CommandType = CommandType.StoredProcedure;

                return sqlcmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException exec)
            {

                if (sqlconn.State == ConnectionState.Open)
                {
                    sqlconn.Close();
                }
                throw exec;
            }
            finally
            {
                sqlcmd = null;
            }
        }
    }
}
