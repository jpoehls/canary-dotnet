using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using System.Security.Cryptography;

namespace Canary
{
    /// <summary>
    /// Parses the JSON squawk event into a BsonDocument.
    /// </summary>
    public static class SquawkEventParser
    {
        private static SHA1Managed Sha1 = new SHA1Managed();
        private static Encoding TextEncoding = Encoding.UTF8;

        private const string TOKEN = "token";
        private const string TIME = "time";
        private const string LEVEL = "level";
        private const string COMMON = "common";
        private const string DETAILS = "details";
        private const string HASH = "hash";

        public static BsonDocument Parse(string json)
        {
            BsonDocument doc;

            // parse the JSON
            try
            {
                doc = BsonDocument.Parse(json);
            }
            catch (Exception ex)
            {
                throw new SquawkEventParserException("Error parsing JSON.", ex);
            }

            // ensure that token is present
            if (!doc.Contains(TOKEN) || !doc[TOKEN].IsString)
                throw new SquawkEventParserException("Invalid JSON. 'token' property not found, or was not a string.");

            // set defaults for any missing or improperly typed properties
            if (doc.Contains(TIME) && doc[TIME].IsString)
            {
                DateTime parsedTime;
                if (DateTime.TryParse(doc[TIME].AsString, out parsedTime))
                {
                    doc.Set(TIME, parsedTime);
                }
            }

            if (!doc.Contains(TIME) || !doc[TIME].IsDateTime)
            {
                doc.Set(TIME, DateTime.UtcNow);
            }

            if (!doc.Contains(LEVEL) || !doc[LEVEL].IsInt32)
                doc.Set(LEVEL, 1);

            if (!doc.Contains(COMMON) || !doc[COMMON].IsBsonDocument)
                doc.Set(COMMON, BsonNull.Value);

            if (!doc.Contains(DETAILS) || !doc[DETAILS].IsBsonDocument)
                doc.Set(DETAILS, BsonNull.Value);

            // compute the hash
            doc.Set(HASH, ComputeHash(doc));

            return doc;
        }

        private static string ComputeHash(BsonDocument doc)
        {
            // smash together the information we want to hash
            var smash = new StringBuilder();

            smash.Append(doc[TOKEN].AsString);
            smash.Append(doc[LEVEL].AsInt32);

            var common = doc[COMMON].AsBsonDocument;
            smash.Append(common.ToJson());

            var buffer = TextEncoding.GetBytes(smash.ToString());
            var hash = BitConverter.ToString(Sha1.ComputeHash(buffer)).Replace("-", "");

            return hash;
        }
    }
}
