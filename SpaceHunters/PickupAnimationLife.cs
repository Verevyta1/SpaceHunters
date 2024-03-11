using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceHunters
{
    class PickupAnimationLife
    {
        #region Declaration

        Animation lifePickupAnimation; // Animation
        Vector2 position; // Where the life drop pickup happens in the game world
        public bool active; // Set life drop to active
        int lifePickupFrames; // How long the animation stays on the screen

        public int Width
        {
            get { return lifePickupAnimation.frameWidth; }
        }

        public int Height
        {
            get { return lifePickupAnimation.frameWidth; }
        }

        #endregion

        public void Initialize(Animation ANIMATION, Vector2 POSITION) // Local variables
        {
            lifePickupAnimation = ANIMATION;
            position = POSITION;
            active = true;
            lifePickupFrames = 30;
        }

        public void UpdateLife(GameTime gameTime)
        {
            lifePickupAnimation.Update(gameTime); // Updates the frames
            lifePickupFrames -= 1; // Counts the frames
            if (lifePickupFrames <= 0) // If no frames remaining
            {
                this.active = false; // Leaves the screen
            }
        }

        public void Draw(SpriteBatch spriteBatch) // Draw
        {
            lifePickupAnimation.Draw(spriteBatch);
        }
    }
}

