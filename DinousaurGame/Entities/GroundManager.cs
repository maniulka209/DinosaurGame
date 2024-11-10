using DinosaurGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinousaurGame.Entities
{

    public class GroundManager : IGameEntity
    {
        public int DrawOrder { get; set; }
        public GroundManager(Texture2D spriteSheet, EntityManager entityManager, Trex trex)
        {
            _spriteSheet = spriteSheet;
            _groundTiles = new List<GroundTile>();
            _entityManager = entityManager; 
            _trex = trex;

            _regularSprite = new SpriteM(spriteSheet, SPRITE_POS_X, SPRITE_POS_Y, SPRITE_WIDTH, SPRITE_HEIGHT);
            _bumpySprite = new SpriteM(spriteSheet, SPRITE_POS_X + SPRITE_WIDTH, SPRITE_POS_Y, SPRITE_WIDTH, SPRITE_HEIGHT);

            _random =new Random();
            
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch )
        {
            // GroundTiles are automaticly draw by entitymanager
        }

        public void Update(GameTime gameTime)
        {
           
                List<GroundTile> tilesToRemove = new List<GroundTile>();

                if (_groundTiles.Any())
                {
                    float MaxPosX = _groundTiles.Max(e => e.PositionX);
                    if (MaxPosX < 0)
                    {
                        SpawnTile(MaxPosX);
                    }
                }
                foreach (GroundTile gt in _groundTiles)
                {
                    gt.PositionX -= _trex.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (gt.PositionX < -SPRITE_WIDTH)
                    {
                        _entityManager.RemoveEntity(gt);
                        tilesToRemove.Add(gt);
                    }
                }

                foreach (GroundTile gt in tilesToRemove)
                {
                    _groundTiles.Remove(gt);
                }
                tilesToRemove.Clear();
            
        }
        public void Initialize()
        {
            _groundTiles.Clear();
            GroundTile groundTile = CreatRegularTile(0);

            _groundTiles.Add(groundTile);
            _entityManager.AddEntity(groundTile);
        }
        private Trex _trex;

        private const int SPRITE_WIDTH = 600;
        private const int SPRITE_HEIGHT = 14;
        private const int SPRITE_POS_X = 2;
        private const int SPRITE_POS_Y = 54;
        private const float GROUND_TILE_POS_Y = 119;

        private Texture2D _spriteSheet;
        private SpriteM _regularSprite;
        private SpriteM _bumpySprite;

        private List<GroundTile> _groundTiles;
        
        private readonly EntityManager _entityManager;

        private Random _random;
        private GroundTile CreatRegularTile(float posX)
        {
            GroundTile groundTile = new GroundTile( _regularSprite ,posX , GROUND_TILE_POS_Y);
            return groundTile;
        }
        private GroundTile CreatBumpyTile(float posX)
        {
            GroundTile groundTile = new GroundTile(_bumpySprite, posX, GROUND_TILE_POS_Y);
            return groundTile;  
        }

        private void SpawnTile(float MaxPosX)
        {
            double random = _random.NextDouble();

            GroundTile groundTile;
            float posX = MaxPosX + SPRITE_WIDTH;

            if(random > 0.5)
            {
                groundTile = CreatBumpyTile(posX);
            }
            else
            {
                groundTile = CreatRegularTile(posX);
            }

            _groundTiles.Add(groundTile);
            _entityManager.AddEntity(groundTile);
        }
    }
}
