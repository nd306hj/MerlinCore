﻿using Merlin2d.Game;
using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerlinGame.Actors
{
    public class DummyActor : AbstractActor
    {
        private int counter = 0;
        private int i = 0;

        public DummyActor()
        {
            SetAnimation(new Animation("resources/sprites/default.png", 128, 128));
        }

        public override void Update()
        {
            if (counter++ % 60 == 0)
            {
                Console.WriteLine("{0}", i);
            }
        }

        //public override void Update()
        //{
        //    //throw new NotImplementedException();
        //}
    }
}
