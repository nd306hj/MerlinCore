using Merlin2d.Game.Actors;
using Merlin2d.Game.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2d.Game
{
    public interface World
    {

        void AddActor(IActor actor);
        void RemoveActor(IActor actor);
        //IEnumerator<Actor> GetEnumerator();
        Boolean IntersectWithWall(IActor actor);
        List<IActor> GetActors();
        void ShowMessage(Message message);
        bool IsWall(int x, int y);
        void SetWall(int x, int y, Boolean wall);
        IActor GetActorByName(String name);

        int GetTileWidth();
        int GetTileHeight();

        void CenterOn(IActor actor);

        void ShowInventory(Inventory inventory);
        void SetFactory(Factory factory);
        void SetScenario(Scenario scenario);
        void SetMap(string path);
        void SetPhysics(Physics physics);
    }
}
