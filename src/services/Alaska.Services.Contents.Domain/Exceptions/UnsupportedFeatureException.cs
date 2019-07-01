using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Alaska.Services.Contents.Domain.Exceptions
{
    public class UnsupportedFeatureException : Exception
    {
        public UnsupportedFeatureException()
        {
        }

        public UnsupportedFeatureException(string message) : base(message)
        {
        }

        public UnsupportedFeatureException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnsupportedFeatureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
