using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2d.Game
{
    public interface Scenario
    {
        void CreateActors(IWorld world);
    }
}
