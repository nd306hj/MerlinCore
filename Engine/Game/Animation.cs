using Merlin2d.Game.Exceptions;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Merlin2d.Game
{
    public class Animation
    {

        private Texture2D texture;//= Raylib.LoadTexture("resources/raylib-cs_logo.png");
        private string resource;
        private int width;
        private int height;
        private int frameDuration;
        private int rotation;
        private bool looping = true;
        private bool pingPong = false;
        private bool isRunning = false;
        private bool shouldStopAt = false;
        private bool isFlipped = false;
        private bool disposedValue;

        private int currentFrame = 0;
        private int framesCount;
        private int time = 0;
        private int stopIndex = 0;

        private int nextFrameStep = 1;
        private Vector2 cameraFocus;

        private Rectangle[] frames;

        private bool renderSmall = true;

        private static int objectCount = 0;

        //private static readonly float inventorySize = 16.0f;


        public Animation(string resource, int width, int height, int frameDuration)
        {
            objectCount++;
            this.resource = resource;
            this.width = width;
            this.height = height;
            this.frameDuration = frameDuration;

            if (!System.IO.File.Exists(resource))
            {
                throw new FileNotFoundException(
                    String.Format("Error while loading spritesheet: {0} not found.", resource));
            }

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

        ~Animation()
        {
            objectCount--;
        }

        internal void UnloadTexture()
        {
            Raylib.UnloadTexture(texture);
        }

        public Animation(string resource, int width, int height) : this(resource, width, height, 10)
        {

        }

        public string GetResource()
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

        internal void Render(int x, int y)
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
                    if (!looping)
                    {
                        Stop();
                    }
                }
                else if (currentFrame < 0)
                {
                    currentFrame = 0;
                    nextFrameStep = 1;
                    if (!looping)
                    {
                        Stop();
                    }
                }
                if (shouldStopAt && currentFrame == stopIndex)
                {
                    Stop();
                }
            }
            Raylib.DrawTextureRec(texture, frames[currentFrame], new Vector2(x, y), Raylib_cs.Color.WHITE);
        }

        internal void Render(int x, int y, int width, int height)
        {
            //getSlickAnimation().draw(x, y, width, height);

            if (!renderSmall)
            {
                Render(x, y);
            }
            else
            {
                float scaleX = width / (float)this.width;
                float scaleY = height / (float)this.height;
                float scale = scaleX > scaleY ? scaleX : scaleY;
                Raylib.DrawTextureEx(texture, new Vector2(x, y), 0, scale, Raylib_cs.Color.WHITE);
            }
            //throw new NotImplementedException();
        }

        public void Stop()
        {
            isRunning = false;
            shouldStopAt = false;
        }

        public void Start()
        {
            isRunning = true;
            shouldStopAt = false;
        }

        public void SetPingPong(bool pingPong)
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
            stopIndex = frameIndex;
            shouldStopAt = true;
        }

        public void SetCurrentFrame(int frameIndex)
        {
            if (currentFrame < framesCount && currentFrame > 0)
            {
                currentFrame = frameIndex;
                time = 0;
            }
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
            this.rotation = angle;
        }
        public void FlipAnimation()
        {
            isFlipped = !isFlipped;
            for (int i = 0; i < framesCount; i++)
            {
                frames[i].width = -frames[i].width;
            }

        }
    }
}
