using System;
using System.Data;
using System.Data.SqlClient;

public class DatabaseConnector
{
    private readonly string connectionString;

    public DatabaseConnector(string server, string database)
    {
        // Construye la cadena de conexión utilizando la autenticación de Windows integrado
        connectionString = $"Server={server};Database={database};Integrated Security=True;";
    }

    //public void ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
    //{
    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        using (SqlCommand command = new SqlCommand(procedureName, connection))
    //        {
    //            command.CommandType = CommandType.StoredProcedure;
    //            command.Parameters.AddRange(parameters);

    //            connection.Open();
    //            command.ExecuteNonQuery();
    //        }
    //    }
    //}

    public DataTable ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);

                connection.Open();

                DataTable dataTable = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }

                return dataTable;
            }
        }
    }

    public DataTable ExecuteQuery(string query)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();

                DataTable dataTable = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }

                return dataTable;
            }
        }
    }



}
