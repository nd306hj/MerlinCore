using Merlin2d.Game;
using Merlin2d.Game.Actors;
using System;

namespace MerlinGame
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
