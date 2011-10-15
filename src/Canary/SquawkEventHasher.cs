using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canary.Web.Models;
using System.Security.Cryptography;

namespace Canary
{
    public class SquawkEventHasher
    {
        private static SHA1Managed Sha1 = new SHA1Managed();
        private static Encoding TextEncoding = Encoding.UTF8;

        public string ComputeHash(SquawkEventModel ev)
        {
            // smash together the information we want to hash
            var smash = new StringBuilder();

            smash.Append(ev.Token);
            smash.Append(ev.Level);

            foreach (string key in ev.Common.Keys)
            {
                string value = ev.Common[key];
                smash.Append(key);
                smash.Append(value);
            }

            var buffer = TextEncoding.GetBytes(smash.ToString());
            var hash = BitConverter.ToString(Sha1.ComputeHash(buffer)).Replace("-", "");

            return hash;
        }
    }
}
