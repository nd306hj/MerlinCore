using Merlin2d.Game.Actions;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Exceptions;
using Merlin2d.Game.Items;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using TiledSharp;

namespace Merlin2d.Game
{
    public class GameWorld : IWorld
    {
        private static String WALL_LAYER = "walls";
        private static String BACKGROUND_LAYER = "background";
        private static int BORDER = 2;
        private String mapResource = null;
        private Map tiledMap = null;
        private IFactory factory = null;
        private Scenario scenario = null;
        private List<IActor> actors = new List<IActor>();
        private List<IActor> actorsToAdd = new List<IActor>();
        private List<IActor> triggers = new List<IActor>();
        private IActor centeredOn;
        private Message message;
        private Inventory inventory;
        private Boolean debugGraphics = false;
        //private Class<? extends Actor>[] renderOrder;
        //private SlickWorld slickWorld;
        private IPhysics gameLevelPhysics;

        private Camera2D camera;

        private int width;
        private int height;

        private List<Action<IWorld>> initActions = new List<Action<IWorld>>();

        public GameWorld(int width, int height)
        {
            this.width = width;
            this.height = height;
            camera.offset = new Vector2(width / 2, height / 2);
        }

        internal void Initialize()
        {
            if (mapResource != null)
            {
                LoadMap();
            }

            initActions.ForEach(a => a(this));
        }

        public void SetMap(string resource)
        {
            this.mapResource = resource;
        }

        protected void LoadMap()
        {
            this.actors.Clear();
            tiledMap = new Map(this.mapResource);
            actors.Clear();
            actorsToAdd.Clear();
            if (factory != null)
            {
                tiledMap.ActorData.ForEach(a =>
                {
                    actorsToAdd.Add(factory.Create(a.Type, a.Name, a.X, a.Y));
                });
                AddActors();
            }
        }

        public void SetFactory(IFactory factory)
        {
            this.factory = factory;
        }

        internal void SetCamera(Camera2D camera)
        {
            this.camera = camera;
        }

        public void AddInitAction(Action<IWorld> action)
        {
            initActions.Add(action);
        }

        public void SetPhysics(IPhysics physics)
        {
            this.gameLevelPhysics = physics;
            if (physics != null)
            {
                physics.SetWorld(this);
            }
        }

        public void CenterOn(IActor actor)
        {
            this.centeredOn = actor;
        }


        public bool IsWall(int x, int y)
        {
            return tiledMap.IsWall(x, y);
        }

        public void SetWall(int x, int y, bool wall)
        {
            tiledMap.SetWall(x, y, wall);
        }

        public bool IntersectWithWall(IActor actor)
        {
            if (this.tiledMap == null)
            {
                return false;
            }
            int tileWidth = this.tiledMap.TileWidth;
            int tileHeight = this.tiledMap.TileHeight;

            int tileXStart = (actor.GetX() + 2) / tileWidth;
            int tileXEnd = (actor.GetX() + actor.GetWidth() - 4) / tileWidth;
            int tileYStart = (actor.GetY() + 2) / tileHeight;
            int tileYEnd = (actor.GetY() + actor.GetHeight() - 4) / tileHeight;
            for (int tileX = tileXStart; tileX <= tileXEnd; tileX++)
            {
                for (int tileY = tileYStart; tileY <= tileYEnd; tileY++)
                {
                    if (IsWall(tileX, tileY))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void AddActor(IActor actor)
        {
            actorsToAdd.Add(actor);
        }

        public void RemoveActor(IActor actor)
        {
            actor.RemoveFromWorld();
        }

        public void ShowMessage(Message message)
        {
            this.message = message;
        }

        public void ShowInventory(Inventory inventory)
        {
            this.inventory = inventory;
        }

        private int GetXOffset(GameContainer gc)
        {
            if (this.centeredOn != null)
            {
                return (gc.GetWidth() - this.centeredOn.GetWidth()) / 2 - this.centeredOn.GetX();
            }
            return 0;
        }

        private int GetYOffset(GameContainer gc)
        {
            if (this.centeredOn != null)
            {
                return (gc.GetHeight() - this.centeredOn.GetHeight()) / 2 - this.centeredOn.GetY();
            }
            return 0;
        }

        private void AddActors()
        {
            actorsToAdd.ForEach(actor =>
            {
                this.actors.Add(actor);
                actor.OnAddedToWorld(this);
            });
            actorsToAdd.Clear();
        }

        internal void Update(long i)
        {
            AddActors();


            triggers.ForEach(trigger => trigger.Update());
            actors.ForEach(actor => actor.Update());

            actors.RemoveAll(actor => actor.RemovedFromWorld());
            if (gameLevelPhysics != null)
            {
                gameLevelPhysics.Execute();
            }
        }

        //public void setRenderOrder(Class<? extends Actor>...actorClasses)
        //{
        //    this.renderOrder = actorClasses;
        //}

        internal void Render(GameContainer gc)
        {
            if (this.centeredOn != null)
            {
                camera.target = new Vector2(centeredOn.GetX(), centeredOn.GetY());
                //graphics.translate(getXOffset(gc), getYOffset(gc));
            }
            if (tiledMap != null)
            {
                RenderMap();
            }

            RenderActors(this.actors);
            RenderActors(this.triggers);
            RenderInventory(gc);

            RenderMessage();
        }

        private void RenderMap()
        {

            if (this.debugGraphics)
            {
                int width = this.tiledMap.Width;
                int height = this.tiledMap.Height;

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (IsWall(x, y))
                        {
                            Raylib.DrawRectangle(x * this.tiledMap.TileWidth, y * this.tiledMap.TileHeight,
                                this.tiledMap.TileWidth, this.tiledMap.TileHeight, Raylib_cs.Color.WHITE);
                        }
                    }
                }
            }
            this.tiledMap.RenderMapBackground();
        }


        private void RenderActors(List<IActor> listOfActors)
        {
            foreach (IActor actor in listOfActors)
            {
                Animation animation = actor.GetAnimation();
                if (animation != null)
                {
                    animation.Render(actor.GetX(), actor.GetY());
                }
                else
                {
                    throw new MissingAnimationException("Animation missing for" + actor.GetType().ToString());
                }
            }
        }

        private void RenderInventory(GameContainer gc)
        {
            int x;
            if (this.inventory != null)
            {
                x = 4;


                throw new NotImplementedException();
                //graphics.setColor(Color.BLACK);
                //graphics.fillRect(0.0F, gc.GetHeight() - 24, gc.GetWidth(), gc.GetHeight());
                foreach (Item item in this.inventory)
                {
                    item.GetAnimation().Render(x, gc.GetHeight() - 20, 16, 16);
                    x += item.GetWidth() + 4;
                }

                //graphics.setColor(color);
                //        }
                //    }
            }
        }

        private void RenderMessage()
        {
            if (this.message != null)
            {
                Raylib.DrawText(message.GetText(), message.GetX(), message.GetY(),
                                message.GetFontSize(), message.GetColor());
            }
        }

        public List<IActor> GetActors()
        {
            return actors;
        }


        public int GetTileWidth()
        {
            return this.tiledMap.TileWidth;
        }


        public int GetTileHeight()
        {
            return this.tiledMap.TileHeight;
        }
    }
}
