using Microsoft.Extensions.Options;
using Models.Domein;
using Settings;
using System;

namespace MongoDB.Stack
{
    public class ScoreboardRepository : MongoCRUD
    {
        private string tableName;  
        public ScoreboardRepository(IOptions<ScoreboardMongoDbSettings> settings)
            : base(settings.Value.Connection,
                  settings.Value.Database,
                  settings.Value.TableNameScoreboard)
        { 
            this.tableName = settings.Value.TableNameScoreboard;
        }

        public override void InsertRecord<T>(T record)
        {
            if (typeof(T) != typeof(ScoreboeadTable))
            {
                throw new Exception("its not a Scoreboead");
            }

            var collection = _contex.GetCollection<T>(tableName);
            collection.InsertOne(record);
        }

        public override void Seed()
        {
            throw new NotImplementedException();
        }
    }
}