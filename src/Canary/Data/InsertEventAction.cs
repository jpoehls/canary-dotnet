using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver;

namespace Canary.Data
{
    public class InsertEventAction : MongoAction
    {
        public void Execute(BsonDocument doc)
        {
            var occurance = new BsonDocument(doc.GetElement("time"),
                                             doc.GetElement("details"));
            doc.Remove("time");
            doc.Remove("details");

            var query = Query.And(Query.EQ("token", doc["token"]),
                                  Query.EQ("hash", doc["hash"]));

            UpdateBuilder ub = new UpdateBuilder();
            ub.Push("occurances", occurance);
            ub.Set("token", doc["token"]);
            ub.Set("hash", doc["hash"]);
            ub.Set("level", doc["level"]);
            ub.Set("common", doc["common"]);
            ub.Set("lastTime", occurance["time"]);

            Events.Update(query, ub, UpdateFlags.Upsert);
        }
    }
}
