using Merlin2d.Game.Actions;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Enums;
using Merlin2d.Game.Items;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Merlin2d.Game
{
    public class GameContainer : IDisposable
    {

        private bool usesMultipleWorlds;
        private int currentWorldIndex = 0;
        private List<GameWorld> gameWorlds;
        private GameWorld currentWorld;

        private int width;
        private int height;

        private Camera2D camera;
        private float cameraZoomLevel = 1.0f;

        private IMessage winMessage;
        private IMessage failMessage;

        private bool disposedValue;


        public GameContainer(string title, int width, int height) : this(title, width, height, false)
        {

        }

        public GameContainer(string title, int width, int height, bool usesMultipleWorlds)
        {
            this.width = width;
            this.height = height;
            this.usesMultipleWorlds = usesMultipleWorlds;
            Raylib.InitWindow(width, height, title);
            Raylib.SetTargetFPS(60);
            gameWorlds = new List<GameWorld>();
            if (!usesMultipleWorlds)
            {
                currentWorld = new GameWorld(width, height);
                gameWorlds.Add(currentWorld);
            }
            camera = new Camera2D();
            Input.GetInstance();
        }

        /// <summary>
        /// Creates a new map from the resource and adds it into the container. Works only if the container uses multiple worlds.
        /// </summary>
        /// <param name="mapSource">Path to the tiled map (.tmx)</param>
        /// <returns>Index of the new world.</returns>
        public int AddWorld(string mapSource)
        {
            CheckMapMode(true);
            GameWorld world = new GameWorld(width, height);
            world.SetMap(mapSource);
            gameWorlds.Add(world);
            return gameWorlds.Count - 1;
        }

        public void AddActor(IActor actor)
        {
            CheckMapMode(false);
            currentWorld.AddActor(actor);

        }

        private void SetFactory(IFactory factory)
        {
            //this.factory = factory;
            //newGame.SetFactory(factory);
        }

        private void SetPhysics(IPhysics physics)
        {
            this.GetWorld().SetPhysics(physics);
            //newGame.SetPhysics(physics);
        }

        public void SetMap(string path)
        {
            CheckMapMode(false);
            this.GetWorld().SetMap(path);
            //newGame.SetMap(mapPath);

        }

        /// <summary>
        /// Sets the camera's zoom.
        /// </summary>
        /// <param name="zoomLevel">Camera zoom - 1.0f is default value</param>
        public void SetCameraZoom(float zoomLevel)
        {
            cameraZoomLevel = zoomLevel;
        }

        public void Run()
        {
            camera.zoom = cameraZoomLevel;
            //gameWorld.SetCamera(camera);
            currentWorldIndex = 0;
            currentWorld = gameWorlds[currentWorldIndex];
            currentWorld.Initialize();
            bool isRunning = true;
            MapStatus status = MapStatus.Unfinished;

            while (!Raylib.WindowShouldClose())
            {

                if (isRunning)
                {
                    status = Run(currentWorld);
                }

                if (status == MapStatus.Finished)
                {
                    if (currentWorldIndex < gameWorlds.Count - 1)
                    {
                        currentWorld = gameWorlds[++currentWorldIndex];
                        currentWorld.Initialize();
                    }
                    else
                    {
                        isRunning = false;
                        RenderEndGame(winMessage);
                    }
                }
                else if (status == MapStatus.Failed)
                {
                    isRunning = false;
                    RenderEndGame(failMessage);
                }
            }
        }

        private MapStatus Run(GameWorld gameWorld)
        {

            var status = gameWorld.Update();

            IActor centered = gameWorld.GetCenteredActor();
            if (centered != null)
            {
                camera.offset = new Vector2(width / 2, height / 2);
                camera.target = new Vector2(centered.GetX(), centered.GetY());
                //graphics.translate(getXOffset(gc), getYOffset(gc));
            }

            Raylib.BeginDrawing();
            Raylib.BeginMode2D(camera);
            Raylib.ClearBackground(Raylib_cs.Color.BLACK);
            gameWorld.Render(this);
            Raylib.EndMode2D();

            gameWorld.RenderOverlay(this);
            Raylib.EndDrawing();

            return status;
        }

        private void RenderEndGame(IMessage message)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib_cs.Color.BLACK);
            if (message != null)
            {
                Raylib.DrawText(message.GetText(), message.GetX(), message.GetY(),
                    message.GetFontSize(), message.GetColor());
            }
            Raylib.EndDrawing();
        }

        /// <summary>
        /// Sets message to be displayed at the end of the game.
        /// </summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="hasWon">Indicates whether it is a failure or win message</param>
        public void SetEndGameMessage(IMessage message, bool hasWon)
        {
            if (hasWon)
            {
                winMessage = message;
            }
            else
            {
                failMessage = message;
            }
        }

        internal void ShowMessage(Message message)
        {
            GetWorld().AddMessage(message);
        }

        public int GetWidth()
        {
            return this.width;
        }

        public int GetHeight()
        {
            return this.height;
        }

        /// <summary>
        /// Used to access active world.
        /// </summary>
        /// <returns>Active World.</returns>
        public IWorld GetWorld()
        {
            CheckMapMode(false);
            return currentWorld;
        }

        /// <summary>
        /// Used to access world with the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>World with the given index.</returns>
        public IWorld GetWorld(int index)
        {
            CheckMapMode(true);
            return gameWorlds[index];
        }

        private bool CheckMapMode(bool isMultiMap)
        {
            if (isMultiMap != usesMultipleWorlds)
            {
                throw new InvalidOperationException("The container is set to invalid map mode!");
            }

            return true;
        }

        /// <summary>
        /// Since the collection itself is not exposed, it is necessary to somehow know the number of worlds stored in the container.
        /// </summary>
        /// <returns>Number of worlds prepared in the container</returns>
        public int GetWorldCount()
        {
            CheckMapMode(true);
            return gameWorlds.Count;
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
                Raylib.CloseWindow();
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~GameContainer()
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
