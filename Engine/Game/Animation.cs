using Merlin.Game.Exceptions;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Merlin.Game
{
    public class Animation : IDisposable
    {

        private Texture2D texture = Raylib.LoadTexture("resources/raylib-cs_logo.png");
        private string resource;
        private int width;
        private int height;
        private int frameDuration;
        private int rotation;
        private bool looping = true;
        private bool pingPong = false;
        private bool isRunning = false;
        //private bool isDecrementing = false;

        private bool isFlipped = false;
        private bool disposedValue;

        private int currentFrame = 0;
        private int framesCount;
        private int time = 0;

        private int nextFrameStep = 1;

        private Rectangle[] frames;

        public Animation(String resource, int width, int height, int frameDuration)
        {
            this.resource = resource;
            this.width = width;
            this.height = height;
            this.frameDuration = frameDuration;

            texture = Raylib.LoadTexture(resource);

            if (texture.height != height || texture.width % width != 0)
            {
                throw new SpritesheetMismatchException("Width of the spritesheet has to be divisible " +
                    "by the frame width and heights have to match.");
            }

            framesCount = texture.width / width;

            frames = new Rectangle[framesCount];

            for (int i = 0; i < framesCount; i++)
            {
                frames[i] = new Rectangle(i * width, 0, width, height);
            }

        }

        public Animation(String resource, int width, int height) : this(resource, width, height, 10)
        {

        }

        public String GetResource()
        {
            return this.resource;
        }

        public int GetWidth()
        {
            return this.width;
        }

        public int GetHeight()
        {
            return this.height;
        }

        public int GetDuration()
        {
            return this.frameDuration;
        }

        public int GetRotation()
        {
            return this.rotation;
        }

        public void Draw(int x, int y)
        {
            if (isRunning)
            {

                if (time++ >= frameDuration)
                {
                    currentFrame += nextFrameStep;
                    time = 0;
                }

                if (currentFrame >= framesCount)
                {
                    if (pingPong)
                    {
                        currentFrame = framesCount - 1;
                        nextFrameStep = -1;
                    }
                    else
                    {
                        currentFrame = 0;
                    }


                }
                else if (currentFrame < 0)
                {
                    currentFrame = 0;
                    nextFrameStep = 1;
                }
            }

            //getslickanimation().draw(x, y);
            Raylib.DrawTextureRec(texture, frames[currentFrame], new Vector2(x, y), Raylib_cs.Color.WHITE);

            throw new NotImplementedException();
        }

        public void Draw(int x, int y, int width, int height)
        {
            //getSlickAnimation().draw(x, y, width, height);
            throw new NotImplementedException();
        }

        //private org.newdawn.slick.Animation getSlickAnimation()
        //{
        //    if (this.slickAnimation == null)
        //    {
        //        this.slickAnimation = AnimationCache.getInstance().loadAnimation(this.resource, this.width, this.height, this.duration);
        //        getSlickAnimation().setPingPong(this.pingPong);
        //        getSlickAnimation().setLooping(this.looping);
        //    }
        //    return this.slickAnimation;
        //}

        public void Stop()
        {
            isRunning = false;
        }

        public void Start()
        {
            isRunning = true;
        }

        public void SetPingPong(Boolean pingPong)
        {
            this.pingPong = pingPong;
            this.currentFrame = 0;
            this.nextFrameStep = 1;
        }

        public void SetLooping(bool looping)
        {
            this.looping = looping;
        }

        public void StopAt(int frameIndex)
        {
            //if (this.slickAnimation != null)
            //{
            //    this.slickAnimation.stopAt(frameIndex);
            //}
        }

        public void SetCurrentFrame(int frameIndex)
        {
            if (currentFrame < framesCount && currentFrame > 0)
            {
                currentFrame = frameIndex;
                time = 0;

            }
            //if (this.slickAnimation != null)
            //{
            //    this.slickAnimation.setCurrentFrame(frameIndex);
            //}
        }

        public int GetCurrentFrame()
        {
            return currentFrame;
        }

        public int GetFrameCount()
        {
            return framesCount;
        }

        public void SetDuration(int duration)
        {
            this.frameDuration = duration;
        }

        public void SetRotation(int angle)
        {
            //this.rotation = (angle % 360);
            //org.newdawn.slick.Animation newAnimation = new org.newdawn.slick.Animation();
            //for (int i = 0; i < getSlickAnimation().getFrameCount(); i++)
            //{
            //    Image image = this.slickAnimation.getImage(i);
            //    image.setRotation(angle);
            //    newAnimation.addFrame(image, this.slickAnimation.getDuration(i));
            //}
            //newAnimation.setLooping(this.looping);
            //newAnimation.setPingPong(this.pingPong);
            //setDuration(this.duration);

            //this.slickAnimation = newAnimation;
        }

        //private static class AnimationCache
        //{

        //    private static AnimationCache instance = new AnimationCache();
        //    private Map<String, SpriteSheet> cachedSpritesSheet = new HashMap<String, SpriteSheet>();

        //    static AnimationCache getInstance()
        //    {
        //        return instance;
        //    }

        //    SpriteSheet loadSpriteSheet(String resource, int tileWidth, int tileHeight)
        //    {
        //        try
        //        {
        //            SpriteSheet spriteSheet = (SpriteSheet)this.cachedSpritesSheet.get(resource);
        //            if (spriteSheet == null)
        //            {
        //                spriteSheet = new SpriteSheet(resource, tileWidth, tileHeight);
        //                this.cachedSpritesSheet.put(resource, spriteSheet);
        //            }
        //            return spriteSheet;
        //        }
        //        catch (Throwable e)
        //        {
        //            throw new RuntimeException(e);
        //        }
        //    }

        //    org.newdawn.slick.Animation loadAnimation(String resource, int tileWidth, int tileHeight, int duration)
        //    {
        //        return new org.newdawn.slick.Animation(loadSpriteSheet(resource, tileWidth, tileHeight), duration);
        //    }
        //}

        public void FlipAnimation()
        {
            isFlipped = true;
            for (int i = 0; i < framesCount; i++)
            {
                frames[i].width = -frames[i].width;
            }

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
                Raylib.UnloadTexture(texture);
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Animation()
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
