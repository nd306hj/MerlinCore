using Merlin2d.Game;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerlinGame.Actors
{
    public class DummyActor : AbstractActor, IItem
    {
        private int counter = 0;
        private int i = 0;

        public DummyActor()
        {
            Animation animation = new Animation("resources/sprites/default.png", 128, 128);
            animation.Start();
            SetAnimation(animation);

        }

        public override void Update()
        {
            //if (counter++ % 600 == 120)
            //{
            //    Console.WriteLine("{0}", i);
            //    this.RemoveFromWorld();
            //}

            if (Input.GetInstance().IsKeyPressed(Input.Key.A))
            {
                GetWorld().AddMessage(new Message("aaa", 100, 100, 20, Color.Blue, MessageDuration.Short));
            }
        }

        //public override void Update()
        //{
        //    //throw new NotImplementedException();
        //}
    }
}
