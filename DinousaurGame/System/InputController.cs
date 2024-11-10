using DinousaurGame.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D11;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinousaurGame.System
{
    public class InputController
    {
        public InputController(Trex trex) 
        {
            _trex = trex;
        }

        public void ProcessControls(GameTime gametime)
        {
            KeyboardState keyboardstate = Keyboard.GetState();

            if ((keyboardstate.IsKeyDown(Keys.Up ) && !_privouskeyboardstate.IsKeyDown(Keys.Up) )|| 
                (keyboardstate.IsKeyDown(Keys.Space) && !_privouskeyboardstate.IsKeyDown(Keys.Space)))
            {   
                if( _trex.State != TrexState.Jumping)
                {
                    _trex.BeginJump();
                }

            }
            else if ((keyboardstate.IsKeyUp(Keys.Up) && keyboardstate.IsKeyUp(Keys.Space)) && _trex.State == TrexState.Jumping)
            {
                _trex.CancelJump();
            }
            else if(keyboardstate.IsKeyDown(Keys.Down))
            {
                if (_trex.State == TrexState.Jumping || _trex.State == TrexState.Falling) 
                { 
                    _trex.Drop();
                }
                else
                {
                    _trex.Duck();
                }
            }
            else if (keyboardstate.IsKeyUp(Keys.Down)  && _trex.State == TrexState.Ducking)
            {
                _trex.CancelDuck();
            }


            _privouskeyboardstate = keyboardstate;
        }

        private Trex _trex;
        private KeyboardState _privouskeyboardstate;
    }
}
