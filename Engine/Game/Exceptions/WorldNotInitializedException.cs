using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Merlin2d.Game.Exceptions
{
    public class WorldNotInitializedException : Exception
    {
        public WorldNotInitializedException()
        {
        }

        public WorldNotInitializedException(string message) : base(message)
        {
        }

        public WorldNotInitializedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WorldNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
