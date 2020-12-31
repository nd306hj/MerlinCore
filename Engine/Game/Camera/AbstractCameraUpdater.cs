using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace Merlin2d.Game.Camera
{
    internal abstract class AbstractCameraUpdater : ICameraUpdater
    {
        protected int width;
        protected int height;

        internal AbstractCameraUpdater(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public abstract void UpdateCamera(ref Camera2D camera, IActor actor, int mapWidth, int mapHeight);

        protected int GetXCentered(IActor actor)
        {
            return actor.GetX() + actor.GetWidth() / 2;
        }

        protected int GetYCentered(IActor actor)
        {
            return actor.GetY() + actor.GetHeight() / 2;
        }
    }
}
