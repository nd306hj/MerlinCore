using Merlin2d.Game;
using Merlin2d.Game.Actions;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Enums;
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
        private readonly Random random;
        private Animation animation1;
        private Animation animation2;
        private Command move;
        private Message healthMsg;

        public DummyActor()
        {
            Animation animation = new Animation("resources/sprites/default.png", 128, 128);
            //animation1 = new Animation("", 0, 0);
            //animation2 = new Animation("", 0, 0);
            animation.Start();
            SetAnimation(animation);

            random = new Random();
            //move = new Move(this, 0, 0, 0);

        }

        public override void Update()
        {
            //if (counter++ % 600 == 120)
            //{
            //    Console.WriteLine("{0}", i);
            //    this.RemoveFromWorld();
            //}

            //if (Input.GetInstance().IsKeyPressed(Input.Key.A))
            {
                //SetAnimation(animation1);
                //GetWorld().AddMessage(new Message("aaa", 100, 100, 20, Color.Blue, MessageDuration.Short));
                //move.Execute();
                this.SetPosition(GetX() - 1, GetY() - 1);
                ShowMessage();
            }
        }

        private void ShowMessage()
        {
            if (GetWorld() != null)
            {
                if (healthMsg != null)
                {
                    GetWorld().RemoveMessage(healthMsg);
                }
                healthMsg = new Message(GetHealth().ToString(), -26, -10, 10, new Color(255, 0, 0), MessageDuration.Indefinite);
                healthMsg.SetAnchorPoint(this);
                GetWorld().AddMessage(healthMsg);
            }
        }

        private int GetHealth()
        {
            return 100;
        }

        //public override void Update()
        //{
        //    //throw new NotImplementedException();
        //}
    }
}
