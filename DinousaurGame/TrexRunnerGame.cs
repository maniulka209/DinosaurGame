using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using TrexRunner.Graphics;

namespace TrexRunner
{
    public class TrexRunnerGame : Game
    {
       

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        
        private const string ASSET_NAME_SPRITESHEET = "TrexSpritesheet (1)";
        private const string ASSET_NAME_SFX_HIT = "hit";
        private const string ASSET_NAME_SFX_SCORE_REACHED = "score-reached";
        private const string ASSET_NAME_SFX_BUTTON_PRESS = "button-press";

        private SoundEffect _sfxHit;
        private SoundEffect _sfxScoreReached;
        private SoundEffect _sfxButtonPress;
        
        private Texture2D _SpriteSheetTexture;
        public TrexRunnerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //loding assets
            _sfxButtonPress = Content.Load<SoundEffect>(ASSET_NAME_SFX_BUTTON_PRESS);
            _sfxScoreReached = Content.Load<SoundEffect>(ASSET_NAME_SFX_SCORE_REACHED);
            _sfxHit = Content.Load<SoundEffect>(ASSET_NAME_SFX_HIT);

            _SpriteSheetTexture = Content.Load<Texture2D>(ASSET_NAME_SPRITESHEET);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();


            //  this  replace _spriteBatch.Draw(_SpriteSheetTexture, new Vector2(10, 10), new Rectangle(848,0,44,49),Color.White);  
            SpriteM DefultTrex = new SpriteM(_SpriteSheetTexture, 848, 0, 44, 49);
            DefultTrex.Draw(_spriteBatch, new Vector2(50, 50));
            
            _spriteBatch.End(); 

            base.Draw(gameTime);
        }
    }
}
