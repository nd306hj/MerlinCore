using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2d.Game
{
    public interface IMessage
    {
        public string GetText();
        public void SetText(string newText);

        public int GetX();

        public int GetY();

        public Color GetColor();

        public int GetFontSize();
        public int RemainingTime();
        public bool IsAnchored();
        public void SetAnchorPoint(IActor actor);
    }
}
