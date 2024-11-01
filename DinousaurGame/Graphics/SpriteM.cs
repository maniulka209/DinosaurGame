﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinosaurGame.Graphics
{

    public class SpriteM  //allow to draw sprite faster and easier
    {
       
        public Texture2D Texture { get; private set; }

        public int X {  get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }


        public Color TintColor { get; set; } = Color.White;
       

        public SpriteM(Texture2D texture , int x , int y , int width , int height) 
        {
            Texture = texture;
            X = x;
            Y = y;
            Width = width; 
            Height = height;
        }

        

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture , position , new Rectangle(X, Y, Width, Height), TintColor);
        }
    }
}
