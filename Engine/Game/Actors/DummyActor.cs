using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin.Game.Actors
{
    public class DummyActor : AbstractActor
    {
        private Animation animation;
        public override bool IntersectsWithActor(Actor actor)
        {
            throw new NotImplementedException();
        }

        public override bool IsAffectedByGravity()
        {
            return false;
        }

        public override bool RemovedFromWorld()
        {
            throw new NotImplementedException();
        }

        public override void RemoveFromWorld()
        {
            throw new NotImplementedException();
        }

        public override void SetAnimation(Animation animation)
        {
            this.animation = animation;
        }

        public override void SetGravity(bool isGravityEnabled)
        {
            throw new NotImplementedException();
        }

        public override void SetPosition(int posX, int posY)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
