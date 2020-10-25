using Merlin2d.Game;
using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerlinGame.Actors
{
    public abstract class AbstractActor : IActor
    {    // VARIABLES
        private int posX;
        private int posY;
        private Animation animation;
        private IWorld world;
        private string name;
        private bool isRemoved = false;
        private bool isGravityEnabled = false;

        // IMPLEMENTATION
        public AbstractActor()
        {

        }

        public AbstractActor(string name)
        {
            this.name = name;
        }

        public int GetX()
        {
            return posX;
        }

        public int GetY()
        {
            return posY;
        }

        public virtual int GetWidth()
        {
            return animation.GetWidth();
        }

        public virtual int GetHeight()
        {
            return animation.GetHeight();
        }

        public virtual void SetPosition(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public virtual Animation GetAnimation()
        {
            return animation;
        }

        public virtual void SetAnimation(Animation animation)
        {
            this.animation = animation;
        }

        //public virtual bool Intersects(IActor other)
        //{
        //    if ((posX < actor.GetX() - GetWidth()) || (posX > actor.GetX() + actor.GetWidth()))
        //    {
        //        return false;
        //    }

        //    if ((posY < actor.GetY() - GetHeight()) || (posY > actor.GetY() + actor.GetHeight()))
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        public void OnAddedToWorld(IWorld world)
        {
            this.world = world;
        }


        public IWorld GetWorld()
        {
            return world;
        }


        public virtual string GetName()
        {
            return name;
        }

        public virtual void SetName(string name)
        {
            this.name = name;
        }

        public virtual IActor GetActorByName(string name)
        {
            return GetWorld().GetActors().First(actor => actor.GetName().Equals(name));
        }

        public abstract void Update();
        
        public virtual bool IntersectsWithActor(IActor actor)
        {
            return false;
        }

        public virtual bool IsAffectedByPhysics()
        {
            return isGravityEnabled;
        }

        public virtual void SetPhysics(bool isGravityEnabled)
        {
            this.isGravityEnabled = isGravityEnabled;
        }

        public virtual void RemoveFromWorld()
        {
            isRemoved = true;
        }

        public virtual bool RemovedFromWorld()
        {
            return isRemoved;
        }
    }

}
