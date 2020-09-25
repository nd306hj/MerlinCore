using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin.Game.Actors
{
    public class DummyActor : AbstractActor
    {
        private Animation animation;
        private bool shouldRemove = false;
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
            return shouldRemove;
        }

        public override void RemoveFromWorld()
        {
            shouldRemove = true;
        }

        //public override void SetAnimation(Animation animation)
        //{
        //    this.animation = animation;
        //}

        public override void SetGravity(bool isGravityEnabled)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            //throw new NotImplementedException();
        }
    }
}
