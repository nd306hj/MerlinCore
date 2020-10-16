using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2d.Game
{
    public interface Factory
    {

        IActor Create(String actorType, String actorName);
    }
}
