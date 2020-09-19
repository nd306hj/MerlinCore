using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin.Game.Actors
{
    public abstract class AbstractActor : Actor
    {    // VARIABLES
    private int posX, posY;
    private Animation animation;
    private World world;
    private String name;

    // IMPLEMENTATION
    public AbstractActor()
    {

    }

    public AbstractActor(String name)
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

    public int GetWidth()
    {
        return animation.GetWidth();
    }

    public int GetHeight()
    {
        return animation.GetHeight();
    }

    public void GetPosition(int arg0, int arg1)
    {
        posX = arg0;
        posY = arg1;
    }

    public Animation GetAnimation()
    {
        return animation;
    }

    public void GetAnimation(Animation animation)
    {
        this.animation = animation;
    }

    public bool Intersects(Actor actor)
    {
        if ((posX < actor.GetX() - GetWidth()) || (posX > actor.GetX() + actor.GetWidth()))
        {
            return false;
        }

        if ((posY < actor.GetY() - GetHeight()) || (posY > actor.GetY() + actor.GetHeight()))
        {
            return false;
        }
        return true;
    }

    public void AddedToWorld(World world)
    {
        this.world = world;
    }


    public World GetWorld()
    {
        return world;
    }


    public String GetName()
    {
        return name;
    }

    public void SetName(String name)
    {
        this.name = name;
    }

    public Actor GetActorByName(String name)
    {
        //for (Actor actor : getWorld())
        foreach(Actor actor in GetWorld().GetActors())
        {
            if (actor.GetName().Equals(name))
            {
                return actor;
            }
        }
        return null;
    }

        public abstract void SetAnimation(Animation animation);
        public abstract void SetPosition(int posX, int posY);
        public abstract void Update();
        public abstract bool IntersectsWithActor(Actor actor);
        public abstract bool IsAffectedByGravity();
        public abstract void SetGravity(bool isGravityEnabled);
        public abstract void RemoveFromWorld();
        public abstract bool RemovedFromWorld();
    }

}
