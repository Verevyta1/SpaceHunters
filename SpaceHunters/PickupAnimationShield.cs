using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceHunters
{
    class PickupAnimationShield
    {
        #region Declaration

        Animation shieldPickupAnimation; // Animation for the explosion
        Vector2 position; // Where the explosion happens in the game world
        public bool active; // Set explosion to active
        int shieldPickupFrames; // How long the explosion animation stays on the screen

        public int Width
        {
            get { return shieldPickupAnimation.frameWidth; }
        }

        public int Height
        {
            get { return shieldPickupAnimation.frameWidth; }
        }

        #endregion

        public void Initialize(Animation ANIMATION, Vector2 POSITION) // Local variables
        {
            shieldPickupAnimation = ANIMATION;
            position = POSITION;
            active = true;
            shieldPickupFrames = 30; 
        }

        public void UpdateShield(GameTime gameTime)
        {
            shieldPickupAnimation.Update(gameTime); // Updates the frames
            shieldPickupFrames -= 1; // Counts the frames
            if (shieldPickupFrames <= 0) // If no frames remaining
            {
                this.active = false; // Leaves the screen
            }
        }

        public void Draw(SpriteBatch spriteBatch) // Draw
        {
            shieldPickupAnimation.Draw(spriteBatch);
        }   
    }

}
