using Merlin2d.Game.Actors;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Merlin2d.Game.Camera
{
    internal class CameraUpdaterCenterInsideMap : AbstractCameraUpdater
    {

        public CameraUpdaterCenterInsideMap(int width, int height) : base(width, height)
        {
        }

        public override void UpdateCamera(ref Camera2D camera, IActor actor, int mapWidth, int mapHeight)
        {
            Vector2 target = new Vector2(GetXCentered(actor), GetYCentered(actor));
            camera.offset = new Vector2(width / 2, height / 2);
            camera.target = target;

            Vector2 max = Raylib.GetWorldToScreen2D(new Vector2(mapWidth, mapHeight), camera);
            Vector2 min = Raylib.GetWorldToScreen2D(new Vector2(0, 0), camera);

            if (max.X < width)
            {
                camera.offset.X = width - (max.X - width / 2);
            }

            if (min.Y > 0)
            {
                camera.offset.Y = height / 2 - min.Y;
            }

            if (max.Y < height)
            {
                camera.offset.Y = height - (max.Y - height / 2);
            }
            if (min.X > 0)
            {
                camera.offset.X = width / 2 - min.X;
            }
        }
    }
}
