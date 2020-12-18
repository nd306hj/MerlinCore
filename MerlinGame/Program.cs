using Merlin2d.Game;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Enums;
using Merlin2d.Game.Items;
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
            GameContainer container = new GameContainer("Remorseless winter", 1000, 800, true);
            //container.GetWorld().SetFactory(new ActorFactory());
            int index = container.AddWorld("resources/maps/demo1.tmx");

            //IActor actor = new DummyActor();
            //actor.SetPosition(100, 100);
            //Animation animation = new Animation("resources/demo.png", 64, 64);
            //animation.Start();
            //actor.SetAnimation(animation);
            //container.AddActor(actor);
            container.GetWorld(index).SetPhysics(new Gravity());

            container.AddWorld("resources/maps/basicTest.tmx");
            //container.GetWorld().AddInitAction(world =>
            //{
            //    world.CenterOn(world.GetActors().Find(a => a.GetName() == "actor"));
            //});
            IInventory backpack = new Backpack(5);
            backpack.AddItem(new DummyActor());
            backpack.AddItem(new DummyActor());

            container.GetWorld(0).ShowInventory(backpack);
            container.GetWorld(0).AddInitAction(w =>
            {
                IActor actor = new DummyActor();
                actor.SetPosition(300, 500);
                w.AddActor(actor);

                w.CenterOn(actor);
            });

            container.GetWorld(0).SetEndCondition(world =>
            {
                Console.WriteLine("Map finished");
                return MapStatus.Finished;
            });
            Message messageBad = new Message("Game over", 100, 100, 20, Color.Blue, MessageDuration.Short);
            Message messageGood = new Message("You won!", 100, 100, 20, Color.Blue, MessageDuration.Short);
            container.SetEndGameMessage(messageBad, false);
            container.SetEndGameMessage(messageGood, true);
            container.Run();
            //var a = container.GetWorld().GetActors().Where(x => x.GetName().Equals("aaa"));
            //List<int> values = new List<int>();
            //int smallesteValue = values.OrderBy(x => x).First();
            //int numberOfPositiveValues = values.Where(x => x > 0).Count();

            
        }
}
}
