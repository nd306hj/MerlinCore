using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2d.Game.Actions
{
    public interface IAction<T>
    {
        public void Execute(T t);

    }
}
