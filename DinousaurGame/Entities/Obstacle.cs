using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinosaurGame.Graphics;

namespace DinousaurGame.Entities
{
    abstract public class Obstacle: IGameEntity
    {

        public int DrawOrder {  get; set; }

        public Vector2 Position { get;  private set; }

        public abstract Rectangle ColissionBox { get; }

        protected Obstacle(Trex trex , Vector2 position)
        {
            _trex = trex;
            this.Position = position;
        }
      
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sprite?.Draw(spriteBatch, Position);
        }

        public void Update(GameTime gameTime) 
        {
            float posX = Position.X - _trex.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds; 
            Position = new Vector2(posX, Position.Y);
        }

        private Trex _trex;


        protected SpriteM  _sprite;
    }
}
