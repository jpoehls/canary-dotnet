using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Canary
{
    public class EventSummary
    {
        public DateTime LastTime { get; set; }
        public string Hash { get; set; }
        public int Level { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }

        public int OccuranceCount { get; set; }
    }

    public class FullEvent
    {
        public FullEvent()
        {
            Common = new Dictionary<string, string>();
            Occurances = new List<EventOccurance>();
        }

        public DateTime LastTime { get; set; }
        public string Token { get; set; }
        public string Hash { get; set; }
        public int Level { get; set; }
        public IDictionary<string, string> Common { get; set; }
        public IList<EventOccurance> Occurances { get; set; }
        public string Raw { get; set; }
    }

    public class EventOccurance
    {
        public EventOccurance()
        {
            Details = new Dictionary<string, string>();
        }

        public DateTime Time { get; set; }
        public IDictionary<string, string> Details { get; set; }
    }
}