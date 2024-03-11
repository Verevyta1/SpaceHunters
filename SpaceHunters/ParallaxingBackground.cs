using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceHunters
{
    class ParallaxingBackground
    {
        // The image needed to represent the parallaxing background
        Texture2D texture;

        // The array of positions for the parallaxing background
        Vector2[] positions;

        // Declaring the speed in which the background would be moving
        int speed;

        int bgHeight, bgWidth;

        public void Initialize(ContentManager content, string texturePath, int screenWidth, int screenHeight, int speed)
        {
            bgHeight = screenHeight;
            bgWidth = screenWidth;

            // Allow the background texture to be loaded for the parallaxing background
            texture = content.Load<Texture2D>(texturePath);
            // Setting the speed for the background
            this.speed = speed;

            positions = new Vector2[screenWidth / texture.Width + 1];

            // Declaring the position in which the parallaxing background will be
            for (int i = 0; i < positions.Length; i++)
            {
               
                positions[i] = new Vector2(i * texture.Width, 0);
            }
        }
        public void Update(GameTime gametime)
        {
            // Declaring the update of the parallaxing background
            for (int i = 0; i < positions.Length; i++)
            {
                // Allwoing the background to update through declaring speed
                // Duplicating the speed position allows it to run a little faster however, keep it smooth
                positions[i].Y += speed;
                positions[i].Y += speed;
                // If the speed has the background moving up
                if (speed <= 0)
                {
                    // Check the texture is out of view then put that texture at the end of the screen
                    if (positions[i].Y <= -texture.Width)
                    {
                        positions[i].Y = texture.Width * (positions.Length - 1);
                    }
                }

                // If the speed has the background moving down
                else
                {
                    // Check if the texture is out of view then position it to the start of the screen
                    if (positions[i].Y >= texture.Width * (positions.Length - 1))
                    {
                        positions[i].Y = -texture.Width;
                    }

                }

            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                Rectangle rectBg = new Rectangle((int)positions[i].X, (int)positions[i].Y, bgWidth, bgHeight);
                spriteBatch.Draw(texture, rectBg, Color.White);
            }
        }
    }
}
