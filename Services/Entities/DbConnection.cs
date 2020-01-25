using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.Entities
{
    public class DbConnection : IDbConnections
    {
       // public string strConnectionString = "Data Source =DESKTOP-2OFQSQK\\SQLEXPRESS;initial catalog=JMSDB;User Id=sa;Password=vipl$123;persist security info=True;Integrated Security=SSPI;";

        SqlConnection conn = new SqlConnection();
        SqlCommand comm = new SqlCommand();
        SqlDataAdapter adapter = new SqlDataAdapter();

        public DbConnection()
        {
            conn = new SqlConnection(strConnectionString());
        }
        public string strConnectionString()
        {
            //return "Data Source =DESKTOP-2OFQSQK\\SQLEXPRESS;initial catalog=JMSDB;User Id=sa;Password=vipl$123;persist security info=True;Integrated Security=SSPI;";
            return "Data Source =LAPTOP-L7469IMM;initial catalog=JMSDB;persist security info=True;Integrated Security=SSPI;";
        }
        public bool openConnection()
        {
            try
            {
                if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                    conn.OpenAsync();
                if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
               // EventLogger.LogEvent(ex.ToString(), EventLogEntryType.Error);
                return false;
            }

        }
        public void closeConnection()
        {
            try
            {
                conn.Close();
            }
            catch (Exception ex)
            {
               // EventLogger.LogEvent(ex.ToString(), EventLogEntryType.Error);
            }

        }
        public object ExecuteScalar(string query)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (openConnection())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    return cmd.ExecuteScalar();
                }
                else return null;
            }
            catch (Exception ex)
            {
              //  EventLogger.LogEvent(ex.ToString(), EventLogEntryType.Error);
                return null;
            }
            finally { closeConnection(); }
        }
        public object ExecuteProcedure(string procName, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (openConnection())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = procName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
                else return null;
            }
            catch (Exception ex)
            {
               // EventLogger.LogEvent(ex.ToString(), EventLogEntryType.Error);
                return null;
            }
            finally { closeConnection(); }
        }
        public object ExecuteProcedureWithOpenConnection(string procName, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = procName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
               // EventLogger.LogEvent(ex.ToString(), EventLogEntryType.Error);
                return null;
            }
            //finally { closeConnection(); }
        }


        public int ExecuteNonQuery(string query)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (openConnection())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    return cmd.ExecuteNonQuery();
                }
                else return -1;
            }
            catch (Exception ex)
            {
               // EventLogger.LogEvent(ex.ToString(), EventLogEntryType.Error);
                return -1;
            }
            finally { closeConnection(); }
        }
        public DataTable ExecuteProcedureForDataTable(string procName, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();

            try
            {
                if (openConnection())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = procName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
               // EventLogger.LogEvent(ex.ToString(), EventLogEntryType.Error);
                return null;
            }
            finally { closeConnection(); }
        }
        public DataSet ExecuteProcedureForDataSet(string procName, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();

            try
            {
                if (openConnection())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = procName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
               // EventLogger.LogEvent(ex.ToString(), EventLogEntryType.Error);
                return null;
            }
            finally { closeConnection(); }
        }
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

    }
    //public class EventLogger
    //{
    //    public static void LogEvent(string message, EventLogEntryType EntryType)
    //    {
    //        using (EventLog eventLog = new EventLog("Application"))
    //        {
    //            eventLog.Source = "Application";
    //            eventLog.WriteEntry(message, EntryType, 2020);
    //        }
    //    }
    //}


}


