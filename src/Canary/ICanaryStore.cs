using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canary.Web.Models;

namespace Canary
{
    public interface ICanaryStore
    {
        void InsertEvent(SquawkEventModel @event);
        IEnumerable<SquawkEventModel> GetAllEvents(string token);
        SquawkEventModel GetEvent(string token, string hash);
    }
}
