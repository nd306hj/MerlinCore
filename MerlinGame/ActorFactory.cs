using Merlin2d.Game;
using Merlin2d.Game.Actors;
using MerlinGame.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerlinGame
{
    public class ActorFactory : IFactory
    {
        public IActor Create(string actorType, string actorName, int x, int y)
        {
            IActor actor = null;
            if (actorType == "DummyActor")
            {
                actor = new DummyActor();
                actor.SetName(actorName);
                actor.SetPosition(x, y);
            }

            return actor;
        }
    }
}
