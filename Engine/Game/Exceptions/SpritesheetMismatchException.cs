using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin.Game.Exceptions
{
    public class SpritesheetMismatchException : Exception
    {
        public SpritesheetMismatchException(string message) : base(message)
        {
        }
    }
}
