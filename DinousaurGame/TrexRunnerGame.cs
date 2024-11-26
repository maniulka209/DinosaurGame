using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using DinosaurGame.Graphics;
using DinousaurGame.Entities;
using DinousaurGame.System;

namespace DinosaurGame
{
    public class TrexRunnerGame : Game
    {
        public GameState State { get; private set; }
        public TrexRunnerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _entityManager = new EntityManager();

            State = GameState.Initial;
            _fadeInTexturePosX = 44;


        }

        private void HandleJumpComplete(object sender, System.EventArgs e)
        {
            if(State == GameState.Transition)
            {
                State = GameState.Playing;
                _trex.Initialize ();
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.ApplyChanges();
            
        }

      

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //loding assets
            _sfxButtonPress = Content.Load<SoundEffect>(ASSET_NAME_SFX_BUTTON_PRESS);
            _sfxScoreReached = Content.Load<SoundEffect>(ASSET_NAME_SFX_SCORE_REACHED);
            _sfxHit = Content.Load<SoundEffect>(ASSET_NAME_SFX_HIT);

            _SpriteSheetTexture = Content.Load<Texture2D>(ASSET_NAME_SPRITESHEET);

            _trex = new Trex(_SpriteSheetTexture, new Vector2(TREX_START_POSITION_X,TREX_START_POSITION_Y),_sfxButtonPress);
            _trex.DrawOrder = 10;

            _scoreBoard = new ScoreBoard(_SpriteSheetTexture , new Vector2(SCORE_BOARD_POS_X, SCORE_BOARD_POS_Y),_trex);
            

            _fadeInTexture = new Texture2D(GraphicsDevice, 1, 1);
            _fadeInTexture.SetData(new Color[] {Color.White});

            _inputController = new InputController(_trex);

            _groundManager = new GroundManager(_SpriteSheetTexture, _entityManager,_trex);
            _groundManager.Initialize();

            _entityManager.AddEntity(_trex);
            _entityManager.AddEntity(_groundManager);
            _entityManager.AddEntity(_scoreBoard);

            _trex.JumpComplete += HandleJumpComplete;
        }

       

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);

            KeyboardState keyboardstate = Keyboard.GetState();

            if (State == GameState.Playing)
            {
                _inputController.ProcessControls(gameTime);
                
            }
            else if (State == GameState.Transition)
            {
                _fadeInTexturePosX += TRANSITION_VELOCITY * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_fadeInTexturePosX >= WINDOW_WIDTH) 
                {
                    State = GameState.Playing;
                }

            }
            else if(State == GameState.Initial)
            {
                if (keyboardstate.IsKeyDown(Keys.Up) || keyboardstate.IsKeyDown(Keys.Space))
                {
                    StartGame();
                }
            }

            _entityManager.Update(gameTime);

            
            
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            _entityManager.Draw( gameTime,_spriteBatch);
            if (State == GameState.Initial || State == GameState.Transition)
            {
                _spriteBatch.Draw(_fadeInTexture, new Rectangle((int)System.Math.Round(_fadeInTexturePosX),0,WINDOW_WIDTH,WINDOW_HEIGHT),Color.White);
            }
                
            _spriteBatch.End(); 

            base.Draw(gameTime);
        }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private const string ASSET_NAME_SPRITESHEET = "TrexSpritesheet (1)";
        private const string ASSET_NAME_SFX_HIT = "hit";
        private const string ASSET_NAME_SFX_SCORE_REACHED = "score-reached";
        private const string ASSET_NAME_SFX_BUTTON_PRESS = "button-press";

        private const int WINDOW_WIDTH = 600;
        private const int WINDOW_HEIGHT = 150;
        private const int TREX_START_POSITION_Y = WINDOW_HEIGHT - 16 - 49;
        private const int TREX_START_POSITION_X = 1;
        private const float SCORE_BOARD_POS_X = WINDOW_WIDTH - 70f;
        private const float SCORE_BOARD_POS_Y = 10f;

        private ScoreBoard _scoreBoard;


        private SoundEffect _sfxHit;
        private SoundEffect _sfxScoreReached;
        private SoundEffect _sfxButtonPress;

        private Texture2D _SpriteSheetTexture;
        private Texture2D _fadeInTexture;

        private float _fadeInTexturePosX;
        private const float TRANSITION_VELOCITY = 800f;

        private Trex _trex;
        private InputController _inputController;

        private EntityManager _entityManager;
        private GroundManager _groundManager;


        private bool StartGame()
        {
            if(State != GameState.Initial)
            {
                return false;
            }
            State = GameState.Transition;
            _trex.BeginJump();
            return true;
        }
    }
}
