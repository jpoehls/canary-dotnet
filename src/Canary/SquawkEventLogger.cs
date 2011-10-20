using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canary.Data;

namespace Canary
{
    public static class SquawkEventLogger
    {
        public static void LogEvent(string json)
        {
            var doc = SquawkEventParser.Parse(json);
            var action = new InsertEventAction();
            action.Execute(doc);
        }
    }
}
