using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2d.Game.Actors
{
    public class DummyActor : AbstractActor
    {
        private Animation animation;
        private bool shouldRemove = false;
        private int counter = 0;
        private int i = 0;

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
