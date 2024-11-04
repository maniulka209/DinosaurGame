using DinosaurGame.Graphics;
using DinousaurGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D11;
using SharpDX.WIC;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinousaurGame.Entities
{
    public class Trex : IGameEntity
    {

        public Trex(Microsoft.Xna.Framework.Graphics.Texture2D spriteSheet, Vector2 position ,  SoundEffect jumpSound ) 
        { 
            
            this.Position = position;
            _startPosY = position.Y;
            _idleTrexBackGroundSprite = new SpriteM(spriteSheet, TREX_IDLE_BACKGROUND_SPRITE_POS_X, TREX_IDLE_BACKGROUND_SPRITE_POS_Y, TREX_DEFULT_SPRITE_WIDTH, TREX_DEFULT_SPRITE_HEIGHT);
            this.State = TrexState.Idle;

            _random = new Random();

            _idleSprite = new SpriteM(spriteSheet , TREX_DEFULT_SPRITE_POS_X , TREX_DEFULT_SPRITE_POS_Y ,TREX_DEFULT_SPRITE_WIDTH ,TREX_DEFULT_SPRITE_HEIGHT) ;
            _idleBlinkingSprite = new SpriteM(spriteSheet, TREX_DEFULT_SPRITE_POS_X + TREX_DEFULT_SPRITE_WIDTH, TREX_DEFULT_SPRITE_POS_Y, TREX_DEFULT_SPRITE_WIDTH, TREX_DEFULT_SPRITE_HEIGHT);
            _runSpriteOne = new SpriteM(spriteSheet, TREX_RUNNING_SPRITE_ONE_POS_X, TREX_RUNNING_SPRITE_ONE_POS_Y, TREX_DEFULT_SPRITE_WIDTH, TREX_DEFULT_SPRITE_HEIGHT);
            _runSpriteTwo = new SpriteM(spriteSheet, TREX_RUNNING_SPRITE_ONE_POS_X + TREX_DEFULT_SPRITE_WIDTH, TREX_RUNNING_SPRITE_ONE_POS_Y, TREX_DEFULT_SPRITE_WIDTH, TREX_DEFULT_SPRITE_HEIGHT);
            _duckSpriteOne = new SpriteM(spriteSheet, TREX_DUCKING_SPRITE_POS_X, TREX_DUCKING_SPRITE_POS_Y, TREX_DUCKING_SPRITE_WIDTH, TREX_DEFULT_SPRITE_HEIGHT);
            _duckSpriteTwo = new SpriteM(spriteSheet, TREX_DUCKING_SPRITE_POS_X + TREX_DUCKING_SPRITE_WIDTH , TREX_DUCKING_SPRITE_POS_Y, TREX_DUCKING_SPRITE_WIDTH, TREX_DEFULT_SPRITE_HEIGHT);

            _blinkAnimation = new SpriteAnimation();
            CreateBlinkAnimation();
            _blinkAnimation.Play();

            _runAnimation = new SpriteAnimation();
            CreatRunAnimation();
            _runAnimation.Play();

            _duckAnimation = new SpriteAnimation();
            CreatDuckAnimation();
            _duckAnimation.Play();

            _jumpSound = jumpSound;


        }

        public Vector2 Position { get; set; }


        public TrexState State { get; private set; }

        public bool IsAlive { get; private set; }

        public int DrawOrder { get; set; }

        public float Speed {  get; private  set; }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (State == TrexState.Idle) 
            {
                _idleTrexBackGroundSprite.Draw(spriteBatch, this.Position);
                _blinkAnimation.Draw(spriteBatch, this.Position);
            }
            else if (State == TrexState.Jumping || State == TrexState.Falling)
            {
                _idleSprite.Draw(spriteBatch, this.Position);
            }
            else if (State == TrexState.Running)
            {
                _runAnimation.Draw(spriteBatch, this.Position);
            }
            else if (State == TrexState.Ducking)
            {
                _duckAnimation.Draw(spriteBatch, this.Position);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (State == TrexState.Idle)
            {

                if (_blinkAnimation.IsPlaying == false)
                {
                    CreateBlinkAnimation();
                    _blinkAnimation.Play();
                }


                _blinkAnimation.Update(gameTime);

            }
             else if( State == TrexState.Falling || State == TrexState.Jumping)
            {
                this.Position = new Vector2(Position.X, Position.Y + _verticalVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds );
                _verticalVelocity += GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds  ;
                _verticalVelocity += _dropVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_verticalVelocity >= 0)
                {
                    State = TrexState.Falling;
                }
                if(Position.Y >= _startPosY)
                {
                    _verticalVelocity = 0;
                    this.Position = new Vector2(Position.X, _startPosY);
                    State = TrexState.Running;
                }
            }

            else if ( State == TrexState.Running)
            {
                _runAnimation.Update(gameTime);
            }
            else if (State == TrexState.Ducking)
            {
                _duckAnimation.Update(gameTime);
            }

            _dropVelocity = 0;
        }
        public bool Duck()
        { 
            if(State == TrexState.Jumping || State == TrexState.Falling)
            {
                return false;
            }
            State = TrexState.Ducking;
            return true;
        }

        

        public bool Drop()
        {
            if(State != TrexState.Falling && State !=TrexState.Jumping)
            {
                return false;
            }
            State = TrexState.Falling;
            _dropVelocity = DROP_VELOCITY;
            return true;

        }
        public bool CancelDuck()
        {
            if(State != TrexState.Ducking)
            {
                return false;
            }
            State = TrexState.Running;
            return true;
        }
        public bool BeginJump() 
        {
            if( State == TrexState.Jumping || State == TrexState.Falling)
            {
                return false;
            }

            _jumpSound.Play();


            State = TrexState.Jumping;
            _verticalVelocity = JUMP_START_VELOCITY;
           
            return true;
        }


        public bool CancelJump()
        {
            if (State == TrexState.Jumping && (_startPosY - Position.Y) >= MIN_JUMP_HIGHT)
            {   
                State = TrexState.Falling;
                _verticalVelocity = 0;
                return true;
            }
            return false;
        }

        private float _verticalVelocity;
        private float _startPosY;
        private float _dropVelocity;
        

        private SoundEffect _jumpSound;
       
        private SpriteAnimation _blinkAnimation;
        private SpriteAnimation _runAnimation;
        private SpriteAnimation _duckAnimation;

        private SpriteM _idleTrexBackGroundSprite;
        private SpriteM _idleSprite ;
        private SpriteM _idleBlinkingSprite;
        private SpriteM _runSpriteOne;
        private SpriteM _runSpriteTwo;
        private SpriteM _duckSpriteOne;
        private SpriteM _duckSpriteTwo;

        private const int TREX_IDLE_BACKGROUND_SPRITE_POS_X = 40;
        private const int TREX_IDLE_BACKGROUND_SPRITE_POS_Y = 0;

       
        private const float JUMP_START_VELOCITY = -480f;
        private const float GRAVITY = 1600f;
        private const float MIN_JUMP_HIGHT = 40f;
        private const float DROP_VELOCITY = 2500f;


        private const int TREX_DEFULT_SPRITE_WIDTH = 44;
        private const int TREX_DEFULT_SPRITE_POS_X = 848;
        private const int TREX_DEFULT_SPRITE_POS_Y = 0;
        private const int TREX_DEFULT_SPRITE_HEIGHT = 49;

        private const int TREX_RUNNING_SPRITE_ONE_POS_X = TREX_DEFULT_SPRITE_POS_X + TREX_DEFULT_SPRITE_WIDTH * 2;
        private const int TREX_RUNNING_SPRITE_ONE_POS_Y = TREX_DEFULT_SPRITE_POS_Y ;

        private const int TREX_DUCKING_SPRITE_WIDTH = 59;
        private const int TREX_DUCKING_SPRITE_POS_X = 1111;
        private const int TREX_DUCKING_SPRITE_POS_Y = 0;
       

        private Random _random ;

        private void CreateBlinkAnimation()
        {

            _blinkAnimation.Clear();
            _blinkAnimation.ShouldLoop = false;

            double blinkTimeStamp = 2f + _random.NextDouble() * (10f - 2f) ;  // random double number between 2.0 and 10.0


            _blinkAnimation.AddFrame(_idleSprite, 0);
            _blinkAnimation.AddFrame(_idleBlinkingSprite,(float)blinkTimeStamp);
            _blinkAnimation.AddFrame(_idleSprite, (float)blinkTimeStamp + 1f);
        }

        private void CreatRunAnimation()
        {
            _runAnimation.AddFrame( _runSpriteOne, 0);
            _runAnimation.AddFrame( _runSpriteTwo, 0.1f);
            _runAnimation.AddFrame( _runSpriteOne, 0.1f *2);
        }
        private void CreatDuckAnimation()
        {
            _duckAnimation.AddFrame(_duckSpriteOne, 0);
            _duckAnimation.AddFrame(_duckSpriteTwo, 0.1f);
            _duckAnimation.AddFrame(_duckSpriteOne, 0.1f * 2);
        }

    }
}
