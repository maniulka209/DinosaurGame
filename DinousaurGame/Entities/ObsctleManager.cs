using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DinousaurGame.Entities
{
    public class ObsctleManager : IGameEntity
    {
        public int DrawOrder => 0;

        public bool IsManagerOn {  get; set; }  

        public ObsctleManager(EntityManager entityManager, Trex trex, ScoreBoard scoreBoard)
        {
            _entityManager = entityManager;
            _trex = trex;
            _scoreBoard = scoreBoard; 
            _random = new Random();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //like in Groundmanager entityManager is drawing obstacle
        }

        public void Update(GameTime gameTime)
        {
            
        }
        private readonly EntityManager _entityManager;
        private readonly Trex  _trex;
        private readonly ScoreBoard  _scoreBoard;  // scoreboard will describe dinosaur distace 

        private readonly Random _random;

        private const float MIN_SPAWN_DISTANCE = 100;

        private const float MIN_OBSTACLE_DISTANCE = 100;
        private const float MAX_OBSTACLE_DISTANCE = 500;

      
    }
}
