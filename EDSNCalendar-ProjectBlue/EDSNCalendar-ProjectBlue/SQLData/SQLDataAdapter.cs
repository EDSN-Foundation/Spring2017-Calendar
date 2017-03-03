using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace EDSNCalendar_ProjectBlue.SQLData
{
    public class SQLDataAdapter
    {
        private static int lastInsertedId;

        public static int LastInsertedId
        {
            get
            {
                return lastInsertedId;
            }
        }

        private static MySqlConnection Connect()
        {
            string MyConnectionString = "Server=127.0.0.1;Database=edsncalendar;Uid=root;Pwd=pass;";
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            return connection;
        }

        private static MySqlConnection ConnectUsers()
        {
            string MyConnectionString = "Server=127.0.0.1;Database=edsncalendaradmin;Uid=root;Pwd=pass;";
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            return connection;
        }

        private static void Disconnect(MySqlConnection connection)
        {
            connection.Close();
        }

        private static DataSet getDataSet(string sQuery)
        {
            MySqlConnection con = null;
            DataSet ds = new DataSet();
            try
            {
                con = Connect();         
                con.Open();
                MySqlCommand cmd;
                cmd = con.CreateCommand();
                cmd.CommandText = sQuery;
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(ds);
            }
            catch(MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return ds;
        }

        private static DataSet getDataSetUser(string sQuery)
        {
            MySqlConnection con = null;
            DataSet ds = new DataSet();
            try
            {
                con = ConnectUsers();
                con.Open();
                MySqlCommand cmd;
                cmd = con.CreateCommand();
                cmd.CommandText = sQuery;
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(ds);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return ds;
        }

        //Executes a SQL Query and returns the number of rows affected
        private static int ExecuteQuery(string sQuery)
        {
            int iRowsAffected = 0;
            MySqlConnection con = null;
            try
            {
                con = Connect();
                con.Open();
                MySqlCommand cmd;
                cmd = con.CreateCommand();
                cmd.CommandText = sQuery;
                iRowsAffected = cmd.ExecuteNonQuery();
                lastInsertedId = (int)cmd.LastInsertedId;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return iRowsAffected;
        }
        //Executes a SQL Query and returns the number of rows affected
        private static int ExecuteQueryUser(string sQuery)
        {
            int iRowsAffected = 0;
            MySqlConnection con = null;
            try
            {
                con = ConnectUsers();
                con.Open();
                MySqlCommand cmd;
                cmd = con.CreateCommand();
                cmd.CommandText = sQuery;
                iRowsAffected = cmd.ExecuteNonQuery();
                lastInsertedId = (int)cmd.LastInsertedId;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return iRowsAffected;
        }

        public static int QueryExecute(string sQuery)
        {
            int iRowsAffected = ExecuteQuery(sQuery);
            return iRowsAffected;
        }
        public static string Query4String(string sQuery)
        {
            string sResult = string.Empty;
            DataSet ds = getDataSet(sQuery);
            sResult = ds.Tables[0].Rows[0][0].ToString();
            return sResult;
        }
        public static int Query4Int(string sQuery)
        {
            int iResult;
            DataSet ds = getDataSet(sQuery);
            iResult = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            return iResult;
        }
        public static DataTable Query4DataTable(string sQuery)
        {
            DataTable dtResults = new DataTable();
            DataSet ds = getDataSet(sQuery);
            if (ds.Tables.Count > 0)
                dtResults = ds.Tables[0];
            return dtResults;
        }
        public static DataSet Query4DataSet(string sQuery)
        {
            DataSet dsResult = new DataSet();
            dsResult = getDataSet(sQuery);
            return dsResult;
        }
        public static void QueryUpdateImage(int EventId, byte[] image)
        {
            using (MySqlConnection connection = Connect())
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE calendarevent SET mbImage = (?img) WHERE iEventId = " + EventId +";";
                    MySqlParameter fileContentParameter = new MySqlParameter("?img", MySqlDbType.MediumBlob, image.Length);

                    fileContentParameter.Value = image;

                    command.Parameters.Add(fileContentParameter);

                    connection.Open();

                    command.ExecuteNonQuery();

                }
            }
        }
        public static DataTable Query4DataTableUser(string sQuery)
        {
            DataTable dtResults = new DataTable();
            DataSet ds = getDataSetUser(sQuery);
            if (ds.Tables.Count > 0)
                dtResults = ds.Tables[0];
            return dtResults;
        }
        public static int QueryExecuteUser(string sQuery)
        {
            int iRowsAffected = ExecuteQueryUser(sQuery);
            return iRowsAffected;
        }
    }
}