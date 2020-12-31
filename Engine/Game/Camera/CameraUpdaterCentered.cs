using Merlin2d.Game.Actors;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Merlin2d.Game.Camera
{
    internal class CameraUpdaterCentered : AbstractCameraUpdater
    {
        public CameraUpdaterCentered(int width, int height) : base(width, height)
        {
        }

        public override void UpdateCamera(ref Camera2D camera, IActor actor, int mapWidth, int mapHeight)
        {
            camera.offset = new Vector2(width / 2, height / 2);
            camera.target = new Vector2(GetXCentered(actor), GetYCentered(actor));
        }
    }
}
