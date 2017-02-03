using System;
using System.Data;
using System.Data.SqlClient;

namespace ws_feeds_reader.data
{
    public class Procedure
    {
        public Procedure()
        {
            Ds = new DataSet();
        }

        private SqlCommand cmd;

        public string strConn()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public string ErrorMessage { get; set; }

        public DataSet Ds { get; set; }

        public bool GetData(string storeProcedure)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn()))
                {
                    conn.Open();

                    cmd = new SqlCommand();
                    cmd.CommandText = storeProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 0;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(Ds);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool GetData(string storeProcedure, params object[] parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn()))
                {
                    conn.Open();
                    cmd = new SqlCommand();
                    cmd.CommandText = storeProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 0;

                    SqlCommandBuilder.DeriveParameters(cmd);

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        cmd.Parameters[i + 1].Value = parameters[i];
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(Ds);
                    }

                    int j = Ds.Tables.Count;
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
    }
}