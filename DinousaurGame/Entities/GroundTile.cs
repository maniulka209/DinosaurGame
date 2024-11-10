using DinosaurGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DinousaurGame.Entities
{
    public class GroundTile : IGameEntity
    {

        public GroundTile(SpriteM sprite, float positionX , float positionY)
        {
            Sprite = sprite;
            PositionX = positionX;
            _positionY = positionY;
        }

        public SpriteM Sprite { get; set; }
        public float PositionX { get; set; }

        public int DrawOrder { get; set; }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
          
            Sprite.Draw(spriteBatch, new Vector2(PositionX, _positionY));
           
        }

        public void Update(GameTime gameTime)
        {
           
        }

        private float _positionY;
    }
}
