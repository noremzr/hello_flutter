using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Connection
{
    public static SqlConnection DefaultConnection { get; set; } = null;
    private static string connectionString = @"Server=MVTALESSANDRO\SQLEXPRESS;Database=Biblioteca;User Id=sa;Password=123;";

    public static SqlConnection GetConnection()
    {
        if (DefaultConnection is null)
        {
            DefaultConnection = new SqlConnection(connectionString);
            DefaultConnection.Open();
        }
        if (DefaultConnection.State == System.Data.ConnectionState.Closed)
        {
            DefaultConnection.Open();
        }
        return DefaultConnection;
    }

    public static void EndConnection()
    {
        if (DefaultConnection != null)
        {
            DefaultConnection.Dispose();
            if (DefaultConnection.State != System.Data.ConnectionState.Closed)
            {
                DefaultConnection.Close();
            }
        }
    }
}