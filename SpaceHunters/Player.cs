using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceHunters
{
    class Player
    {
        #region Declarations

        public Animation playerAnimation;  // Animation for  the player      
        public Vector2 position; // Position of the Player relative to the upper left side of the screen        
        public bool active; // State of the player       
        public int shield; // Amount of shield the player has  
        public int lives; // Amount of lives remaining 
        public int score; // Keeps track of the score
        float playerMoveSpeed; // How quickly the ship moves
        Vector2 graphicsInfo; // Hold the Viewport

        // Keyboard and Pad states used to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        public int Width // Get the width and Height of the Ship
        {
            get { return playerAnimation.frameWidth; }
        }

        public int Height // Get the height of the player ship
        {
            get { return playerAnimation.frameHeight; }
        }
        public Vector2 PlayerPos // Player coordinates in game world
        {
            get { return position; }
        }

        #endregion

        public void Initialize(Animation ANIMATION, Vector2 POSITION, Vector2 grInfo)
        {
            playerAnimation = ANIMATION;
            position = POSITION;
            active = true; // Set the player to be active
            shield = 100; // Shield starting value
            lives = 5; // Amount of lives at the start            
            graphicsInfo = grInfo; // Set the viewport
            playerMoveSpeed = 9.75f;  // Player speed
        }

        public void Update(GameTime gameTime)
        {
            //Save the previous state of the keyboard and game pad to determine single key/button presses
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;

            //Read the current state of the keyboard and gamepad and store it
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentKeyboardState = Keyboard.GetState();


            //Get Thumbsticks Controls
            position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
            position.Y += currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;

            //Use the Keyboard/DPad
            if (currentKeyboardState.IsKeyDown(Keys.A) || currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                position.X -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.D) || currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                position.X += playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.W) || currentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                position.Y -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.S) || currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                position.Y += playerMoveSpeed;
            }

            if ( lives <= 0) //if equal or bellow 0
            { active = false; } // Player becomes false

            position.X = MathHelper.Clamp(position.X, 0, graphicsInfo.X - Width -10);  //Using the ship size to stop it from going past the left/right sides of the screen
            position.Y = MathHelper.Clamp(position.Y, 0, graphicsInfo.Y - Height -35); //Using the ship size to stop it from going past the top/bottom of the screen
            playerAnimation.position = position;
            playerAnimation.Update(gameTime);
     
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerAnimation.Draw(spriteBatch);
        }

    }
}
