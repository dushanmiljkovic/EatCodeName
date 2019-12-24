using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;


namespace EatCode.Api.MongoDb
{
    public abstract class MongoCRUD
    {
        protected IMongoDatabase _contex;
        private readonly string tableName;
        public MongoCRUD(string connectionString, string database, string tableName)
        {
            var client = new MongoClient(connectionString);
            _contex = client.GetDatabase(database);
            this.tableName = tableName;
        }

        public abstract void Seed();

        public virtual void InsertRecord<T>(T record)
        {
            var collection = _contex.GetCollection<T>(tableName);
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>()
        {
            var collection = _contex.GetCollection<T>(tableName);
            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadRecordById<T>(Guid id)
        {
            var collection = _contex.GetCollection<T>(tableName);
            var filter = Builders<T>.Filter.Eq("Id", id);
            return collection.Find(filter).First();
        }

        //  Insert Or Update
        public string UpsertRecord<T>(Guid id, T record)
        {
            var collection = _contex.GetCollection<T>(tableName);
            var result = collection.ReplaceOne(
                new BsonDocument("_id", id)
                , record
                , new UpdateOptions { IsUpsert = true });
            return result.UpsertedId.ToString();
        }

        public void DeleteRecord<T>(Guid id)
        {
            var collection = _contex.GetCollection<T>(tableName);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }
    }
}

