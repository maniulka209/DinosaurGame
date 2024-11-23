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
    public class ScoreBoard : IGameEntity
    {
        public ScoreBoard(Texture2D texture , Vector2 position) 
        {
            _texture = texture;
            this.Position = position;
        }
        public double Score { get; set; }

        public int DisplayScore => (int)Math.Floor(Score);

        public int HighScore { get; set; }

        public int DrawOrder { get;  } = 100;
        public Vector2 Position { get; set; }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int[] scoreDigits = SplitDigit(DisplayScore);

            float posX = Position.X;    

            foreach (int digit in scoreDigits)
            {
                SpriteM digitToDraw = getDigitToDraw(digit);

                Vector2 screenPos = new Vector2(posX, Position.Y);
                digitToDraw.Draw(spriteBatch, screenPos);

                posX += TEXTURE_NUM_WIDTH;
            }

            
        }

        public void Update(GameTime gameTime)
        {
          
        }
        

        private const int TEXTURE_NUM_POS_X = 655;
        private const int TEXTURE_NUM_POS_Y = 0;
        private const int TEXTURE_NUM_WIDTH = 10;
        private const int TEXTURE_NUM_HEIGHT  = 13;

        private const int NUMBER_DIGIT_TO_DRAW = 5;

        private Texture2D _texture;

        private DinosaurGame.Graphics.SpriteM getDigitToDraw ( int num)  
        {
            if(num < 0 || num > 9 )
            {
                throw new ArgumentOutOfRangeException("num" , "the digit must be between 0 - 9");
            }

            int posX = TEXTURE_NUM_POS_X + num * TEXTURE_NUM_WIDTH;
            int posY = TEXTURE_NUM_POS_Y;
            
            return new DinosaurGame.Graphics.SpriteM(_texture, posX , posY ,TEXTURE_NUM_WIDTH , TEXTURE_NUM_HEIGHT);
        }

        private int[] SplitDigit(int num)
        {
           string numStr = num.ToString().PadLeft(NUMBER_DIGIT_TO_DRAW , '0');

            int[] result = new int[numStr.Length];

            for (int i = 0; i < numStr.Length; i++)
            {
                result[i] = (int)char.GetNumericValue(numStr[i]);
            }
            return result;

        }

    }   
}
        