using Merlin2d.Game.Actors;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using TiledSharp;

namespace Merlin2d.Game
{
    internal class Map : IDisposable
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }

        private int[,] walls;
        //private int[,] background;

        private BackgroundTile[,] backgroundTiles;

        private Rectangle[] tilesetPositions;

        private Texture2D backgroundTexture;
        private bool disposedValue;

        public List<ActorData> ActorData { get; private set; }

        public Map(string mapResource)
        {
            TmxMap tiledMap = new TmxMap(mapResource);
            Width = tiledMap.Width;
            Height = tiledMap.Height;
            TileWidth = tiledMap.TileWidth;
            TileHeight = tiledMap.TileHeight;
            LoadWalls(tiledMap);
            LoadBackground(tiledMap);
            LoadActors(tiledMap);
        }

        private void LoadWalls(TmxMap tiledMap)
        {
            walls = new int[tiledMap.Width, tiledMap.Height];

            for (int x = 0; x < tiledMap.Width; x++)
            {
                for (int y = 0; y < tiledMap.Height; y++)
                {
                    walls[x, y] = tiledMap.Layers["walls"].Tiles.Single(tile => tile.X == x && tile.Y == y).Gid;
                }
            }
        }

        private void LoadBackground(TmxMap tiledMap)
        {
            backgroundTexture = Raylib.LoadTexture(tiledMap.Tilesets[0].Image.Source);

            //backgroundTexture = Raylib.LoadTexture(tiledMap.ImageLayers[0].Image.Source);

            int tileXCount = (int)tiledMap.Tilesets[0].Image.Width / TileWidth;
            int tileYCount = (int)tiledMap.Tilesets[0].Image.Height / TileHeight;

            tilesetPositions = new Rectangle[tileXCount * tileYCount + 1];

            int i = 1;

            for (int y = 0; y < tileYCount; y++)
            {
                for (int x = 0; x < tileXCount; x++)
                {
                    tilesetPositions[i++] = new Rectangle(x * TileWidth, y * TileHeight, TileWidth, TileHeight);
                }
            }

            backgroundTiles = new BackgroundTile[tiledMap.Width, tiledMap.Height];

            for (int x = 0; x < tiledMap.Width; x++)
            {
                for (int y = 0; y < tiledMap.Height; y++)
                {
                    backgroundTiles[x, y] = new BackgroundTile(x * tiledMap.TileWidth, y * tiledMap.TileHeight,
                        tiledMap.Layers["background"].Tiles.Single(tile => tile.X == x && tile.Y == y).Gid);
                }
            }
        }

        private void LoadActors(TmxMap tiledMap)
        {
            ActorData = new List<ActorData>();
            foreach(TmxObjectGroup groups in tiledMap.ObjectGroups)
            {
                foreach (TmxObject entry in groups.Objects)
                {
                    ActorData.Add(new ActorData(entry));
                }
            }
        }

        public void RenderMapBackground()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    BackgroundTile tile = backgroundTiles[x, y];
                    if (tile.TileCode != 0)
                    {
                        Raylib.DrawTextureRec(backgroundTexture, tilesetPositions[tile.TileCode], tile.Position, Raylib_cs.Color.WHITE);
                    }
                }
            }
        }

        public bool IsWall(int x, int y)
        {
            return walls[x, y] != 0;
        }

        public void SetWall(int x, int y, bool wall)
        {
            walls[x, y] = wall ? 1 : 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                Raylib.UnloadTexture(backgroundTexture);
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Map()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
