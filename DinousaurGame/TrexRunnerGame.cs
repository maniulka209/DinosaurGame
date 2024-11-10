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

        public TrexRunnerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _entityManager = new EntityManager();
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

            _inputController = new InputController(_trex);

            _groundManager = new GroundManager(_SpriteSheetTexture, _entityManager,_trex);
            _groundManager.Initialize();

            _entityManager.AddEntity(_trex);
            _entityManager.AddEntity(_groundManager);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
            _inputController.ProcessControls(gameTime);
            _entityManager.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            _entityManager.Draw( gameTime,_spriteBatch);
                
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

        private SoundEffect _sfxHit;
        private SoundEffect _sfxScoreReached;
        private SoundEffect _sfxButtonPress;

        private Texture2D _SpriteSheetTexture;

        private Trex _trex;
        private InputController _inputController;

        private EntityManager _entityManager;
        private GroundManager _groundManager;
    }
}
