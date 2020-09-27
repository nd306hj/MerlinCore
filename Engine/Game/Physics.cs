using Merlin2d.Game.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2d.Game
{
    public interface Physics : Command
    {
        void SetWorld(World world);
    }
}
