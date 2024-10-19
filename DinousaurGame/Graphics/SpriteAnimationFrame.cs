using DinosaurGame.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinousaurGame.Graphics
{
    public class SpriteAnimationFrame
    {
        public SpriteM Sprite { get; set; }
        public float TimeStamp { get;  }
        public SpriteAnimationFrame(SpriteM sprite , float timestamp) 
        {
            this.Sprite = sprite;
            this.TimeStamp = timestamp;
        }
    }
}
