using Merlin2d.Game.Actions;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Enums;
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
        private List<IMessage> messages;
        private IInventory inventory;
        private Boolean debugGraphics = false;
        //private Class<? extends Actor>[] renderOrder;
        //private SlickWorld slickWorld;
        private IPhysics gameLevelPhysics;
        private Func<IWorld, MapStatus> endCondition = null;

        private bool isCameraFollowStyleDefined = false;


        //private Camera2D camera;

        private int width;
        private int height;
        private bool isLoaded = false;

        private static readonly int itemSize = 32;

        private List<Action<IWorld>> initActions = new List<Action<IWorld>>();

        public GameWorld(int width, int height)
        {
            this.width = width;
            this.height = height;
            //camera.offset = new Vector2(width / 2, height / 2);
            this.messages = new List<IMessage>();
        }

        internal void Initialize()
        {
            isLoaded = true;
            if (mapResource != null)
            {
                LoadMap();
            }

            initActions.ForEach(a =>
            {
                a(this);
                if (actorsToAdd.Count != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Actors added within initialization - acceptable for debug only, put them into .tmx object layer for the final submission!");
                    Console.ResetColor();
                    AddActors();
                }
            });
            initActions.Clear();      
        }

        public void SetMap(string resource)
        {
            this.mapResource = resource;
        }

        protected void LoadMap()
        {
            this.actors.Clear();
            tiledMap = new Map(this.mapResource);
            width = tiledMap.Width * tiledMap.TileWidth;
            height = tiledMap.Height * tiledMap.TileHeight;
            if (actors.Count != 0 || actorsToAdd.Count != 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Manually added actors were removed, use AddInitAction to manually add actors for debug.");
                Console.ResetColor();
            }
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

        //internal void SetCamera(Camera2D camera)
        //{
        //    this.camera = camera;
        //}

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
            if (!isCameraFollowStyleDefined)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Warning - camera follow style is not set in the GameContainer - use SetCameraFollowStyle first.");
                Console.ResetColor();
            }
            this.centeredOn = actor;
        }

        internal IActor GetCenteredActor()
        {
            return this.centeredOn;
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

        public void AddMessage(IMessage message)
        {
            this.messages.Add(message);
        }

        public void RemoveMessage(IMessage message)
        {
            this.messages.Remove(message);
        }

        public void ShowInventory(IInventory inventory)
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

        internal MapStatus Update()
        {
            AddActors();


            triggers.ForEach(trigger => trigger.Update());
            actors.ForEach(actor => actor.Update());
            actors.RemoveAll(actor => actor.RemovedFromWorld());

            //actors.RemoveAll(actor =>
            //{
            //    if (actor.RemovedFromWorld())
            //    {
            //        actor.GetAnimation().UnloadTexture();
            //        return true;
            //    }
            //    return false;
            //});
            if (gameLevelPhysics != null)
            {
                gameLevelPhysics.Execute();
            }

            return endCondition?.Invoke(this) ?? MapStatus.Unfinished;
        }

        //public void setRenderOrder(Class<? extends Actor>...actorClasses)
        //{
        //    this.renderOrder = actorClasses;
        //}

        internal void Render(GameContainer gc)
        {

            if (tiledMap != null)
            {
                RenderMap();
            }

            RenderActors(this.actors);
            RenderActors(this.triggers);
            RenderMessages(true);

        }

        internal void RenderOverlay(GameContainer gc)
        {
            RenderInventory(gc);
            RenderMessages(false);
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
            int offset = 4;
            int y = gc.GetHeight() - itemSize - offset;
            if (this.inventory != null)
            {
                x = offset;
                for (int i = 0; i < inventory.GetCapacity(); i++)
                {
                    Raylib.DrawRectangleLinesEx(new Rectangle(x - offset, y - offset, itemSize + 2 * offset, itemSize + 2 * offset),
                        offset, Raylib_cs.Color.LIGHTGRAY);
                    x += itemSize + offset;
                }

                x = offset;


                //throw new NotImplementedException();
                //graphics.setColor(Color.BLACK);
                //graphics.fillRect(0.0F, gc.GetHeight() - 24, gc.GetWidth(), gc.GetHeight());
                foreach (IItem item in this.inventory)
                {
                    item.GetAnimation().Render(x, y, itemSize, itemSize);
                    x += itemSize + offset;
                }

                //graphics.setColor(color);
                //        }
                //    }
            }
        }

        private void RenderMessages(bool isAnchored)
        {
            messages.Where(x => x.IsAnchored() == isAnchored).ToList().ForEach(message =>
            {
                Raylib.DrawText(message.GetText(), message.GetX(), message.GetY(),
                                message.GetFontSize(), message.GetColor());
            });

            messages.RemoveAll(message => message.RemainingTime() <= 0);
        }

        public List<IActor> GetActors()
        {
            if (isLoaded)
            {
                return actors.ToList();
            }else
            {
                throw new WorldNotInitializedException(
                    "The instance of GameWorld has not yet been initialized. Used AddInitAction to initialize actors.");
            }
        }


        public int GetTileWidth()
        {
            return this.tiledMap.TileWidth;
        }


        public int GetTileHeight()
        {
            return this.tiledMap.TileHeight;
        }

        /// <summary>
        /// Sets a custom function - condition to end the level
        /// </summary>
        /// <param name="condition">Anonymous function which evaluates the state and returns on of the three possible outcomes defined in MapStatus</param>
        public void SetEndCondition(Func<IWorld, MapStatus> condition)
        {
            endCondition = condition;
        }

        internal int GetWidth()
        {
            return width;
        }

        internal int GetHeight()
        {
            return height;
        }

        internal void UpdateCameraFollowStyle(bool isSet)
        {

            isCameraFollowStyleDefined = isSet;
        }
    }
}
