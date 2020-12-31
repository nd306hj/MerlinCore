using Merlin2d.Game.Actors;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2d.Game.Camera
{
    internal class CameraUpdaterNone : AbstractCameraUpdater
    {
        public CameraUpdaterNone(int width, int height) : base(width, height)
        {
        }

        public override void UpdateCamera(ref Camera2D camera, IActor actor, int mapWidth, int mapHeight)
        {
            //throw new NotImplementedException();
        }
    }
}
