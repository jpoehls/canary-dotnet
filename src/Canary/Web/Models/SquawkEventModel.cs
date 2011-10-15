using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Canary.Web.Models
{
    /// <summary>
    /// Used to deserialize the JSON posted to the <see cref="Canary.Controllers.SquawkController"/>.
    /// </summary>
    public class SquawkEventModel
    {
        public SquawkEventModel()
        {
            Time = DateTime.UtcNow;

            Common = new Dictionary<string, string>();
            Details = new Dictionary<string, string>();
        }

        public DateTime? Time { get; set; }
        public string Token { get; set; }
        public int Level { get; set; }
        public Dictionary<string, string> Common { get; set; }
        public Dictionary<string, string> Details { get; set; }
    }
}