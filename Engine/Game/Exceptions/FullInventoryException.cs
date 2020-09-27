using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2d.Game.Exceptions
{
    public class FullInventoryException : Exception
    {

        public FullInventoryException(String s) : base(s)
        {

        }
    }

}
