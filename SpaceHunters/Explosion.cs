using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceHunters
{
    class Explosion
    {
        #region Declaration

        Animation explosionAnimation; // Animation for the explosion
        Vector2 position; // Where the explosion happens in the game world
        public bool active; // Set explosion to active
        int explosionFrames; // How long the explosion animation stays on the screen

        public int Width
        {
            get { return explosionAnimation.frameWidth; }
        }

        public int Height
        {
            get { return explosionAnimation.frameWidth; }
        }

        #endregion

        public void Initialize(Animation ANIMATION, Vector2 POSITION) // Local variables
        {
            explosionAnimation = ANIMATION;
            position = POSITION;
            active = true;
            explosionFrames = 30;
        }

        public void Update(GameTime gameTime)
        {
            explosionAnimation.Update(gameTime); // Updates the frames
            explosionFrames -= 1; // Counts the frames
            if (explosionFrames <= 0) // If no more explosion frames left in explositionFrames then
            {
                this.active = false; // Makes explositon false so they get off the screen
            }      
        }

        public void Draw(SpriteBatch spriteBatch) // Draw
        {
            explosionAnimation.Draw(spriteBatch);
        }
    }
}
