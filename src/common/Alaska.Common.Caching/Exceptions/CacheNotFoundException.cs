using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Common.Caching.Exceptions
{
    public class CacheNotFoundException : CacheException
    {
        public CacheNotFoundException()
        {
        }

        public CacheNotFoundException(string message) : base(message)
        {
        }

        public CacheNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
