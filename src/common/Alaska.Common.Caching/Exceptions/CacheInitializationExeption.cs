using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Common.Caching.Exceptions
{
    public class CacheInitializationExeption : CacheException
    {
        public CacheInitializationExeption()
        {
        }

        public CacheInitializationExeption(string message) : base(message)
        {
        }

        public CacheInitializationExeption(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
