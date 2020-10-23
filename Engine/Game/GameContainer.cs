using Merlin2d.Game.Actions;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Items;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin2d.Game
{
    public class GameContainer : IDisposable
    {

        //public static int stateID = 1;
        //private SlickAppGameContainer container;
        //private Input input;

        private IFactory factory;
        private Scenario scenario;
        private IPhysics physics;
        private string mapPath = null;

        private int currentGameStateID = 1;

        private GameWorld gameWorld;
        //private NewGame newGame;
        private bool disposedValue;

        private int width;
        private int height;

        public GameContainer(string title, int width, int height)
        {
            this.width = width;
            this.height = height;
            Raylib.InitWindow(width, height, title);
            Raylib.SetTargetFPS(60);
            gameWorld = new GameWorld(width, height);

            Input.GetInstance();
        }

        public void AddActor(IActor actor)
        {
            GetWorld().AddActor(actor);

        }

        private void SetFactory(IFactory factory)
        {
            this.factory = factory;
            //newGame.SetFactory(factory);
        }

        private void SetScenario(Scenario scenario)
        {
            this.scenario = scenario;
            //newGame.SetScenario(scenario);
        }

        private void SetPhysics(IPhysics physics)
        {
            this.physics = physics;
            //newGame.SetPhysics(physics);
        }

        public void SetMap(String path)
        {
            this.mapPath = path;
            //newGame.SetMap(mapPath);

        }

        public void RemoveActor(IActor actor)
        {
            //    GameState state = getCurrentState();
            //    if (state instanceof World) {
            //        ((World)state).removeActor(actor);
            //    }
            //else
            //    {
            //        ((World)getState(stateID)).removeActor(actor);
            //    }
        }

        public void Run()
        {
            if (mapPath != null)
            {
                this.gameWorld.SetMap(mapPath);
            }

            if (physics != null)
            {
                this.gameWorld.SetPhysics(physics);
            }


            long index = 0;
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Raylib_cs.Color.BLACK);
                //Raylib.DrawText("Hello, world!", 12, 12, 20, Color.BLACK);
                gameWorld.Update(index++);
                gameWorld.Render(this);

                Raylib.EndDrawing();

            }


        }

        public void ShowMessage(Message message)
        {
            gameWorld.ShowMessage(message);
        }

        public int GetWidth()
        {
            return this.width;
        }

        public int GetHeight()
        {
            return this.height;
        }

        public IWorld GetWorld()
        {
            return this.gameWorld;
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
