using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace Canary.Data
{
    public abstract class MongoAction
    {
        private static string ConnectionString = "mongodb://127.0.0.1:27017";

        private MongoCollection<BsonDocument> _events;
        protected MongoCollection<BsonDocument> Events
        {
            get
            {
                if (_events == null)
                {
                    var server = MongoServer.Create(ConnectionString);
                    var db = server.GetDatabase("canary");
                    _events = db.GetCollection("events");
                }

                return _events;
            }
        }
    }
}
