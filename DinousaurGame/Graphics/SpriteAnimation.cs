using DinosaurGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinousaurGame.Graphics
{
    public class SpriteAnimation
    {
        public bool IsPlaying { get; private set; }
        public float PlayBackProgress { get; private set; }
        public bool ShouldLoop { get; set; } = true;

        public SpriteAnimationFrame this[int idx]  
        {
            get
            {
                return GetFrame(idx);
            }
        }
        public SpriteAnimationFrame CurrentFrame
        {
            get
            {
                return _frames
                    .Where(f => f.TimeStamp <= PlayBackProgress)
                    .OrderBy(f => f.TimeStamp)
                    .LastOrDefault();
            }
        }

        public float Duration
        {
            get
            {
                if(_frames.Count <= 0)
                {
                    return 0;
                }

                return  _frames.Max(f => f.TimeStamp);
            }
        }

        public void AddFrame(SpriteM sprite, float timestamp)
        {
            SpriteAnimationFrame frame = new SpriteAnimationFrame(sprite, timestamp);
            _frames.Add(frame);
        }

        public void Update(GameTime gametime)
        {
            if (IsPlaying == true) 
            {
                this.PlayBackProgress += (float)gametime.ElapsedGameTime.TotalSeconds;

                if(this.PlayBackProgress > this.Duration )
                {
                    if (this.ShouldLoop == true)
                    {
                        this.PlayBackProgress -= this.Duration;
                    }
                    else
                    {
                        Stop();
                    }
                }
                
            }



        }
        public void Draw(SpriteBatch spriteBatch , Vector2 position)
        {
            SpriteAnimationFrame frame = CurrentFrame;
            if( frame != null)
            {
                frame.Sprite.Draw(spriteBatch, position);
            }
        }
        public void Play()
        {
            this.IsPlaying = true;

        }
        public void Stop()
        {
            this.IsPlaying=false;
            this.PlayBackProgress = 0;
        }

        public SpriteAnimationFrame GetFrame (int idx)
        {
            if (idx < 0 || idx >= _frames.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(idx), " a frame with index " + idx + " does not exist in this farme");
            }

            return _frames[idx];
            
        }

        public void Clear()
        {
            Stop();
            _frames.Clear();
            
        }
        private List<SpriteAnimationFrame> _frames = new List<SpriteAnimationFrame>();

        
        
    }
}
