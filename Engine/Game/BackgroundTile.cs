using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Merlin2d.Game
{
    class BackgroundTile
    {
        public Vector2 Position { get; private set; }
        public int TileCode { get; private set; }
        public BackgroundTile(int posX, int posY, int code)
        {
            Position = new Vector2(posX, posY);
            TileCode = code;
        }
    }
}
