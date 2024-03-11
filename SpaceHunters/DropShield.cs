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
    class DropShield
    {
        #region Declarations

        public Animation shieldDropAnimation; // Holds texture for animation      
        public Vector2 position;
        public bool active;
        float speed;
        
       
        public int Width
        {
            get { return shieldDropAnimation.frameWidth; }
        }

        public int Height
        {
            get { return shieldDropAnimation.frameHeight; }
        }

        public Vector2 ShieldDropLocation // Saves position to be used later in the animation
        {
            get { return position; }
        }

        #endregion

        public void Initialize(Animation animation, Vector2 POSITION)
        {
            shieldDropAnimation = animation;
            position = POSITION;
            active = true;
            speed = 3f;
        }

        public void Update(GameTime gameTime)
        {
            position.Y += speed; // Constatly moves down toward the bottom of the screen
            shieldDropAnimation.position = position; // Update the new position
            shieldDropAnimation.Update(gameTime); // Update animation
            if (position.Y >= 900) 
            { active = false; } // Removes power up when reaches 900 on the Y axis
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            shieldDropAnimation.Draw(spriteBatch);
        }
    }
}
