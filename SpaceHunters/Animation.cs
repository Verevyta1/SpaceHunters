using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceHunters
{
    class Animation
    {
        #region Declarations

        Texture2D spriteSheet; // Image sheet used with animation
        Rectangle sourceRect = new Rectangle(); // Which part of the image
        Rectangle destinationRect = new Rectangle(); // Where to display the image
        Color color; // Colour of the frame that will be displayed
        int currentFrame; // Whats being displayed 
        int frameCount; // Total animation frames
        int frameTime; // How long the frame lasts intill another frame is displayed
        int elapsedTime; // Time since last frame update
        public int frameWidth;
        public int frameHeight;
        public bool active; // State of animation
        public bool looping; // Use the same animation multiple times
        public Vector2 position;
        float scale; // used to display the spriteSheet
        private List<Rectangle> frames = new List<Rectangle>();

        #endregion

        public void Initialize(Texture2D TEXTURE, Vector2 POSITION, int FRAMEwidth,
           int FRAMEheight, int FRAMEcount, int FRAMEtime, Color COLOR, float SCALE, bool LOOPING)
        {
            // Local copy of values in the variables
            spriteSheet = TEXTURE;
            position = POSITION;
            this.frameWidth = FRAMEwidth;
            this.frameHeight = FRAMEheight;
            this.frameCount = FRAMEcount;
            this.frameTime = FRAMEtime;
            this.color = COLOR;
            this.scale = SCALE;
            looping = LOOPING;

            elapsedTime = 0; // Timer on frame change, set to 0 
            currentFrame = 0; // Current frame also set to 0    
            active = true; // Default state of animation is set to true

            sourceRect = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            //This is the Animation Rectangle to be picked from the actual SpriteSheet
            // Grab the correct frame in the imageSheet by multiplying the currentFrame index by the Frame width

            destinationRect = new Rectangle(
                (int)position.X - (int)(frameWidth * scale) / 2,
                (int)position.Y - (int)(frameHeight * scale) / 2,
                (int)(frameWidth * scale),
                (int)(frameHeight * scale)); // This plays the animation the Rectangle 

            for (int x = 0; x < frameCount; x++) // Animation order
            {
                frames.Add(new Rectangle(
                (frameWidth * x),
                0,
                frameWidth, frameHeight));
            }
        }

        public void Update(GameTime gameTime)
        {
            if (active == false) return;  // Do not update the game if we are not active

            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds; // Update the elapsed time

            if (elapsedTime > frameTime) // If the elapsed time is larger than the frame time we need to switch frames
            {
                currentFrame++; // goes to next frame
                if (currentFrame == frameCount) // If the currentFrame is equal to frameCount reset currentFrame to zero
                {
                    currentFrame = 0;
                    if (looping == false) // If we are not looping deactivate the animation
                        active = false;
                }
                elapsedTime = 0; // Reset the elapsed time to zero
            }

            sourceRect = frames[currentFrame];

            destinationRect = new Rectangle(
                (int)position.X,
                (int)position.Y,
                frameWidth,
                frameHeight); // This is for the Rectangle animation to be played in the game World
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(spriteSheet, destinationRect, sourceRect, color); // Only draw the animation when active
            }
        }
    }
}
