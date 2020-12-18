using Merlin2d.Game.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2d.Game
{
    public class Message : IMessage
    {

        private string text;
        private int x;
        private int y;
        private int fontSize;
        private int duration;
        private Color color;
        private MessageDuration initialDuration;

        public Message(string text, int x, int y, int fontSize, Color color, MessageDuration messageDuration)
        {
            this.text = text;
            this.x = x;
            this.y = y;
            this.color = color;
            this.initialDuration = messageDuration;
            this.duration = (int)messageDuration;
            this.fontSize = fontSize;
        }

        public Message(string text, int x, int y) : this(text, x, y, 20, Color.Red, MessageDuration.Indefinite)
        {

        }

        public string GetText()
        {
            return this.text;
        }

        public void SetText(string newText)
        {
            this.text = newText;
        }

        public int GetX()
        {
            return this.x;
        }

        public int GetY()
        {
            return this.y;
        }

        public Color GetColor()
        {
            return this.color;
        }

        public int GetFontSize()
        {
            return fontSize;
        }

        public int RemainingTime()
        {
            if (initialDuration != MessageDuration.Indefinite)
            {
                return duration < 0 ? 0 : duration--;
            }
            else
            {
                return 1;
            }
        }
    }
}
