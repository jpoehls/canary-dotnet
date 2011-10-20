using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver.Builders;

namespace Canary.Data
{
    public class GetAllEventsQuery : MongoAction
    {
        public IEnumerable<EventSummary> Execute(string token)
        {
            var query = Query.EQ("token", token);
            foreach (var doc in Events.Find(query))
            {
                var ev = new EventSummary();
                ev.Hash = doc["hash"].AsString;
                ev.LastTime = doc["lastTime"].AsDateTime;
                ev.Level = doc["level"].AsInt32;
                ev.OccuranceCount = doc["occurances"].AsBsonArray.Count;

                if (doc["common"].AsBsonDocument.Contains("message"))
                {
                    ev.Message = doc["common"].AsBsonDocument["message"].RawValue.ToString();
                }

                if (doc["common"].AsBsonDocument.Contains("type"))
                {
                    ev.Type = doc["common"].AsBsonDocument["type"].RawValue.ToString();
                }

                yield return ev;
            }
        }
    }
}
