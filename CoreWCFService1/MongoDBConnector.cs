using System.Text.Json;
using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CoreWCFService1
{
    public class MongoDBConnector
    {
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;

        public MongoDBConnector()
        {
            // Configura la conexión con la base de datos MongoDB
            var client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("local");
            collection = database.GetCollection<BsonDocument>("Demo");
        }

        public void InsertDocument(BsonDocument document)
        {
            // Inserta un documento en la colección
            collection.InsertOne(document);
        }

        public BsonDocument GetDocumentById(string id)
        {
            // Obtiene un documento por su ID de la colección
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            return collection.Find(filter).FirstOrDefault();
        }

        public void UpdateDocument(string id, BsonDocument document)
        {
            // Actualiza un documento por su ID en la colección
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            collection.ReplaceOne(filter, document);
        }

        public void DeleteDocument(string id)
        {
            // Elimina un documento por su ID de la colección
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            collection.DeleteOne(filter);
        }

        public List<BsonDocument> GetAllDocuments()
        {
            // Obtiene todos los documentos de la colección
            return collection.Find(new BsonDocument()).ToList();
        }
    }
}
