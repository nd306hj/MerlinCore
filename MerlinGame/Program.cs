using Merlin2d.Game;
using Merlin2d.Game.Actors;
using MerlinGame.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MerlinGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameContainer container = new GameContainer("Remorseless winter", 1000, 800);
            container.SetMap("resources/maps/basicTest.tmx");
            IActor actor = new DummyActor();
            actor.SetPosition(100, 100);
            //Animation animation = new Animation("resources/demo.png", 64, 64);
            //animation.Start();
            //actor.SetAnimation(animation);
            //container.AddActor(actor);
            container.SetPhysics(new Gravity());
            container.Run();
            //var a = container.GetWorld().GetActors().Where(x => x.GetName().Equals("aaa"));
            //List<int> values = new List<int>();
            //int smallesteValue = values.OrderBy(x => x).First();
            //int numberOfPositiveValues = values.Where(x => x > 0).Count();

            
        }
    }
}
