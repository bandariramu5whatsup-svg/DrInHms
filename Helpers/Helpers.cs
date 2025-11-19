using Microsoft.Data.SqlClient;
using System.Data;

namespace HanuMediSoftCore.Helpers
{
    public class Helpers
    {
        public static class ConnectionHelper
        {
            private static string? _connectionString;

            public static void Initialize(IConfiguration config)
            {
                _connectionString = config.GetConnectionString("DefaultConnection")
                    ?? throw new Exception("Connection String Missing!");
            }

            public static string ConnectionString =>
                _connectionString ?? throw new Exception("Connection Helper not initialized!");
        }

        public static DataTable ExecuteSP(string connStr, string spName, Dictionary<string, object?> parameters)
        {
            using SqlConnection con = new(connStr);
            using SqlCommand cmd = new(spName, con)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Add parameters
            foreach (var p in parameters)
            {
                cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
            }

            con.Open();

            DataTable dt = new();
            using SqlDataAdapter da = new(cmd);
            da.Fill(dt);

            return dt;
        }

        public static List<Dictionary<string, object?>> ToList(DataTable table)
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

