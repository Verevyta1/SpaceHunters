using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceHunters.Content
{
    class Laser
    {
        #region Declarations

        public Animation laserAnimation; //Texutre for laser
        public Vector2 position; //Position of the laser in game world
        public bool Active; //Set the laser to active      
        float laserSpeed = 20f; // How quickly the laser moves
        public int damage; 

        public int Width
        {
            get { return laserAnimation.frameWidth; }
        }

        public int Height
        {
            get { return laserAnimation.frameHeight; }
        }

       

        #endregion

        public void Initialize(Animation ANIMATION, Vector2 POSITION)
        {
            laserAnimation = ANIMATION;
            position = POSITION;
            Active = true;
            damage = 50;
        }

        public void Update(GameTime gameTime) // Update the laser in real time in the game world
        {
            position.Y -= laserSpeed;
            laserAnimation.position = position;
            laserAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch) // Draw laser
        {
            laserAnimation.Draw(spriteBatch);
        }
    }
}
