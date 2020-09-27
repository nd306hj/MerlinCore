using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2d.Game.Actions
{
    public interface Action<T>
    {

        public String GetActionName();

        public void Execute(T t);

    }
}
