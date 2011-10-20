using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace Canary.Data
{
    public class GetSingleEventQuery : MongoAction
    {
        public FullEvent Execute(string token, string hash)
        {
            var query = Query.And(Query.EQ("token", token), Query.EQ("hash", hash));

            var doc = Events.FindOne(query);
            doc.Remove("_id");
            var ev = new FullEvent();
            ev.Token = token;
            ev.Hash = hash;
            ev.LastTime = doc["lastTime"].AsDateTime;
            ev.Level = doc["level"].AsInt32;
            ev.Raw = doc.ToJson();

            foreach (var el in doc["common"].AsBsonDocument.Elements)
            {
                string value = null;
                if (el.Value.IsBsonArray || el.Value.IsBsonDocument)
                {
                    value = el.Value.ToJson();
                }
                else if (el.Value.RawValue != null)
                {
                    value = el.Value.RawValue.ToString();
                }
                ev.Common.Add(el.Name, value);
            }

            foreach (var boc in doc["occurances"].AsBsonArray)
            {
                var boc2 = boc.AsBsonDocument;

                var oc = new EventOccurance();
                oc.Time = boc2["time"].AsDateTime;

                foreach (var el in boc2["details"].AsBsonDocument.Elements)
                {
                    string value = null;
                    if (el.Value.IsBsonArray || el.Value.IsBsonDocument)
                    {
                        value = el.Value.ToJson();
                    }
                    else if (el.Value.RawValue != null)
                    {
                        value = el.Value.RawValue.ToString();
                    }
                    oc.Details.Add(el.Name, value);
                }

                ev.Occurances.Add(oc);
            }
            
            return ev;
        }
    }
}
