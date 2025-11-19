using System.Data;
using Microsoft.Data.SqlClient;

namespace HanuMediSoftCore.Helpers
{
    public class DatabaseHelper
    {
        private readonly string? _connectionString;

        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Execute Stored Procedure → returns DataTable
        public DataTable ExecuteSP(string storedProcedure, Dictionary<string, object?> parameters)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand(storedProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
            }

            using SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // Execute Insert/Update/Delete SP → returns affected rows
        public int ExecuteNonQuery(string storedProcedure, Dictionary<string, object?> parameters)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand(storedProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (var param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
            }

            con.Open();
            return cmd.ExecuteNonQuery();
        }

        // Execute SP → returns single value
        public object? ExecuteScalar(string storedProcedure, Dictionary<string, object?> parameters)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand(storedProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (var param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
            }

            con.Open();
            return cmd.ExecuteScalar();
        }
        public  List<Dictionary<string, object?>> ToList(DataTable table)
        {
            var list = new List<Dictionary<string, object?>>();

            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object?>();

                foreach (DataColumn col in table.Columns)
                {
                    dict[col.ColumnName] = row[col] == DBNull.Value ? null : row[col];
                }

                list.Add(dict);
            }

            return list;
        }
    }
}



//using Microsoft.Data.SqlClient;
//using System.Data;

//namespace HanuMediSoftCore.Helpers
//{
//    public class DatabaseHelper
//    {
//        private readonly string? _connectionString;

//        public DatabaseHelper(IConfiguration config)
//        {
//            _connectionString = config.GetConnectionString("DefaultConnection");
//        }

//        public DataTable ExecuteDataTable(string sp, params SqlParameter[] parameters)
//        {
//            using SqlConnection con = new SqlConnection(_connectionString);
//            using SqlCommand cmd = new SqlCommand(sp, con);
//            cmd.CommandType = CommandType.StoredProcedure;
//            if (parameters != null)
//                cmd.Parameters.AddRange(parameters);

//            using SqlDataAdapter da = new SqlDataAdapter(cmd);
//            DataTable dt = new DataTable();
//            da.Fill(dt);
//            return dt;
//        }

//        public int ExecuteCommand(string sp, params SqlParameter[] parameters)
//        {
//            using SqlConnection con = new SqlConnection(_connectionString);
//            using SqlCommand cmd = new SqlCommand(sp, con);
//            cmd.CommandType = CommandType.StoredProcedure;
//            if (parameters != null)
//                cmd.Parameters.AddRange(parameters);

//            con.Open();
//            return cmd.ExecuteNonQuery();
//        }
//    }
//}
