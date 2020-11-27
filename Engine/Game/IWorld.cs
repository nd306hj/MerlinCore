using Merlin2d.Game.Actions;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2d.Game
{
    public interface IWorld
    {

        void AddActor(IActor actor);
        void RemoveActor(IActor actor);
        //IEnumerator<Actor> GetEnumerator();
        Boolean IntersectWithWall(IActor actor);
        List<IActor> GetActors();
        void AddMessage(IMessage message);
        void RemoveMessage(IMessage message);
        bool IsWall(int x, int y);
        void SetWall(int x, int y, Boolean wall);

        int GetTileWidth();
        int GetTileHeight();

        void CenterOn(IActor actor);

        void ShowInventory(IInventory inventory);
        void SetFactory(IFactory factory);
        void AddInitAction(Action<IWorld> action);
        void SetMap(string path);
        void SetPhysics(IPhysics physics);
    }
}
