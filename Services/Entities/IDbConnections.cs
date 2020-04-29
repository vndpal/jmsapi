using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Services.Entities
{
    public interface IDbConnections
    {
        string strConnectionString();
        bool openConnection();
        void closeConnection();
        object ExecuteScalar(string query);
        object ExecuteProcedure(string procName, params SqlParameter[] parameters);
        object ExecuteProcedureWithOpenConnection(string procName, params SqlParameter[] parameters);
        int ExecuteNonQuery(string query);
        Task<DataTable> ExecuteProcedureForDataTable(string procName, params SqlParameter[] parameters);
        Task<DataSet> ExecuteProcedureForDataSet(string procName, params SqlParameter[] parameters);
        DataTable ToDataTable<T>(List<T> items);
        List<T> ConvertDataTable<T>(DataTable dt);
        T GetItem<T>(DataRow dr);

    } 
}
