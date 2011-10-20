using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MongoDB.Driver;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace Canary.Tests
{
    [TestFixture]
    public class MongoApiTests
    {
        private static string ConnectionString = "mongodb://127.0.0.1:27017";

        [Test]
        public void x()
        {
            var server = MongoServer.Create(ConnectionString);
            var db = server.GetDatabase("canary");
            var col = db.GetCollection("testing");

            col.Insert(new { Name = "Joshua" }, new MongoInsertOptions(col) { SafeMode = SafeMode.True });
        }

        [Test]
        public void SquawkEventParser_Parse_should_parse_correctly()
        {
            const string jsonPost = @"
{
	'time'   : '9/7/2011 2:22:54 AM',
	'token'  : 'RANDOM_TOKEN',
	'level'  : 1,
	'common' : {
		         'type'   : 'NullReferenceException',
		         'message': '...message...',
		         'source' : '...source...'
	           },
	'details': {
		'user'    : 'me',
		'platform': 'win32',
		'cookies' : [
			          { 'name' : 'SESSION_ID',
				        'value': '124380870SJFDK' },

			          { 'name' : 'email',
			            'value': 'someone@somewhere.com' }
		            ]
    }
}
";

            var ev = SquawkEventParser.Parse(jsonPost);

            Assert.IsTrue(ev["hash"].IsString);
            Console.WriteLine("hash: " + ev["hash"].AsString);

            Assert.AreEqual("RANDOM_TOKEN", ev["token"].AsString);
            Assert.AreEqual(1, ev["level"].AsInt32);
            Assert.AreEqual(new DateTime(2011, 9, 7, 2, 22, 54), ev["time"].AsDateTime);

            Assert.IsTrue(ev["common"].IsBsonDocument);
            Assert.AreEqual("NullReferenceException", ev["common"].AsBsonDocument["type"].AsString);
            Assert.AreEqual("...message...", ev["common"].AsBsonDocument["message"].AsString);
            Assert.AreEqual("...source...", ev["common"].AsBsonDocument["source"].AsString);

            Assert.IsTrue(ev["details"].IsBsonDocument);
            Assert.AreEqual("me", ev["details"].AsBsonDocument["user"].AsString);
            Assert.AreEqual("win32", ev["details"].AsBsonDocument["platform"].AsString);

            Assert.IsTrue(ev["details"].AsBsonDocument["cookies"].IsBsonArray);
            //Console.WriteLine(((JArray)ev.Details["cookies"])[0].GetType());
            //Assert.IsInstanceOf<JArray>(ev.Details["cookies"]);
        }

        [Test]
        public void SquawkHasher_should_hash_the_expected_fields()
        {

        }

        [Test]
        public void SquawkLogger_should_insert_into_new_event_mongo_database()
        {
            
        }

        [Test]
        public void SquawkLogger_should_append_recurring_event_to_occurances_collection_of_existing_mongo_document()
        {

        }

        [Test]
        public void SquawkLogger_should_update_LastTime_of_event_document_with_new_timestamp()
        {

        }
    }
}
