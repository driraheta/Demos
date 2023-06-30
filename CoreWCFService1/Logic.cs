using MongoDB.Bson;
using System.Data;
using System.Data.SqlClient;

namespace CoreWCFService1
{
    public class Logic
    {
        // Crea una instancia del conector de la base de datos
        DatabaseConnector connector = new DatabaseConnector("OEI-PRE-DI\\SQLEXPRESS", "ph20577510151_SANALBERTO");

        public DataTable GetDataVentas(string IdCliente)
        {
            int paramValue = Convert.ToInt32(IdCliente);
            // Ejecuta un procedimiento almacenado con parámetros
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@IdCliente", paramValue)
            };

            var resp = connector.ExecuteStoredProcedure("GetDatosVentas", parameters);

            if (resp.Rows.Count > 0)
            {
                resp.TableName = "Datos";
                return resp;
            }
            return new DataTable("Datos");
        }

        public void NewDataMongoo(Employee emp)
        {
            var connector = new MongoDBConnector();

            // Insertar un nuevo documento
            var document = new BsonDocument
        {
            { "Name", emp.Name },
            { "Age",emp.Age },
            { "City", emp.City }
        };
            connector.InsertDocument(document);
        }

    }
}
