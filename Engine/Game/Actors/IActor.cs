using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2d.Game.Actors
{
    public interface IActor
    {
        void SetName(string name);
        void SetAnimation(Animation animation);
        void SetPosition(int posX, int posY);
        string GetName();
        int GetX();
        int GetY();
        int GetWidth();
        int GetHeight();
        Animation GetAnimation();
        IWorld GetWorld();
        void Update();
        void OnAddedToWorld(IWorld world);
        bool IntersectsWithActor(IActor other);
        bool IsAffectedByPhysics();
        void SetPhysics(bool isPhysicsEnabled);
        void RemoveFromWorld();
        bool RemovedFromWorld();
    }
}
