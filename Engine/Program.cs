using System;
using Merlin.Game;
using Merlin.Game.Actors;
using Raylib_cs;

namespace Merlin
{
    class Program
    {
        static void Main(string[] args)
        {
            GameContainer container = new GameContainer("Remorseless winter", 1000, 800);
            container.SetMap("resources/maps/basicTest.tmx");
            Actor actor = new DummyActor();
            actor.SetPosition(100, 100);
            Animation animation = new Animation("resources/demo.png", 64, 64);
            animation.Start();
            actor.SetAnimation(animation);
            container.AddActor(actor);
            container.Run();
            
        }
    }
}
