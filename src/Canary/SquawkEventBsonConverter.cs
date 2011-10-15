using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using Canary.Web.Models;

namespace Canary
{
    public class SquawkEventBsonConverter
    {
        private SquawkEventHasher _hasher;

        public SquawkEventBsonConverter()
            : this(new SquawkEventHasher()) { }

        public SquawkEventBsonConverter(SquawkEventHasher hasher)
        {
            _hasher = hasher;
        }

        public BsonDocument ToBson(SquawkEventModel ev)
        {
            var hash = _hasher.ComputeHash(ev);

            var doc = new BsonDocument();
            doc.Add("appToken", ev.Token);
            doc.Add("level", ev.Level);
            doc.Add("hash", hash);
            doc.Add("lastTime", ev.Time);

            var commonDoc = GetBsonFromDictionary(ev.Common);
            doc.Add("common", commonDoc);

            var doc2 = new BsonDocument();
            doc2.Add("time", ev.Time);

            var detailsDoc = GetBsonFromDictionary(ev.Details);
            doc.Add("details", detailsDoc);

            doc.Add("occurances", new BsonArray(doc2));            

            return doc;
        }

        private BsonDocument GetBsonFromDictionary(IDictionary<string, string> dict)
        {
            var doc = new BsonDocument();
            foreach (var key in dict.Keys)
            {
                doc.Add(key, dict[key]);
            }
            return doc;
        }
    }
}
