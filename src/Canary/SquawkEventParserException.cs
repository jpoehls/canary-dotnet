using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Canary
{
    public class SquawkEventParserException : ApplicationException
    {
        public SquawkEventParserException(string message)
            : base(message) { }

        public SquawkEventParserException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
