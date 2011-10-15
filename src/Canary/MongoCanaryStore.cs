using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using Canary.Web.Models;

namespace Canary
{
    public class MongoCanaryStore : ICanaryStore
    {
        private static string ConnectionString = "mongodb://127.0.0.1:27017";

        private MongoCollection<BsonDocument> GetCollection()
        {
            var server = MongoServer.Create(ConnectionString);
            var db = server.GetDatabase("canary");
            var col = db.GetCollection("events");

            return col;
        }

        public void InsertEvent(SquawkEventModel @event)
        {
            var col = GetCollection();

            var doc = new BsonDocument();
            col.Insert(typeof(SquawkEventModel), @event);
        }

        public IEnumerable<SquawkEventModel> GetAllEvents(string token)
        {
            var col = GetCollection();
            var query = Query.EQ("token", token);
            foreach (SquawkEventModel ev in col.FindAs<SquawkEventModel>(query))
            {
                yield return ev;
            }
        }

        public SquawkEventModel GetEvent(string token, string hash)
        {
            var col = GetCollection();
            var query = Query.And(Query.EQ("token", token), Query.EQ("hash", hash));
            var ev = col.FindOneAs<SquawkEventModel>(query);

            return ev;
        }
    }
}
