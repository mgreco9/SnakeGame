using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Snake.Source.Control
{
    public class InputManager
    {
        protected KeyboardState _prevKeyState;
        protected KeyboardState _currentKeyState;

        public Direction inputDirection;

        public bool inputReset;

        public bool inputIncreaseTime;
        public bool inputDecreaseTime;

        //Singleton class
        protected static InputManager _instance;
        
        public static InputManager Instance
        {
            get
            {
                if (_instance is null)
                    _instance = new InputManager();
                return _instance;
            }
        }


        protected InputManager()
        {

        }

        public void Update()
        {
            _prevKeyState = _currentKeyState;
            _currentKeyState = Keyboard.GetState();

            getInputDirection();
            getInputReset();
            getInputIncreaseTime();
            getInputDecreaseTime();
        }

        protected virtual void getInputDirection()
        {
            if (_currentKeyState.IsKeyDown(Keys.Up))
                inputDirection = Direction.UP;
            if (_currentKeyState.IsKeyDown(Keys.Right))
                inputDirection = Direction.RIGHT;
            if (_currentKeyState.IsKeyDown(Keys.Down))
                inputDirection = Direction.DOWN;
            if (_currentKeyState.IsKeyDown(Keys.Left))
                inputDirection = Direction.LEFT;
        }

        protected virtual void getInputReset()
        {
            if (_currentKeyState.IsKeyDown(Keys.Enter) && _prevKeyState.IsKeyUp(Keys.Enter))
                inputReset = true;
            else
                inputReset = false;
        }

        protected virtual void getInputIncreaseTime()
        {
            if (_currentKeyState.IsKeyDown(Keys.NumPad1) && _prevKeyState.IsKeyUp(Keys.NumPad1))
                inputIncreaseTime = true;
            else
                inputIncreaseTime = false;

            //Trace.TraceInformation($"{inputIncreaseTime}");
        }

        protected virtual void getInputDecreaseTime()
        {
            if (_currentKeyState.IsKeyDown(Keys.NumPad0) && _prevKeyState.IsKeyUp(Keys.NumPad0))
                inputDecreaseTime = true;
            else
                inputDecreaseTime = false;
        }
    }
}
