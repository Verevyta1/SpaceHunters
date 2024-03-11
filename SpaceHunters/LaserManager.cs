using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceHunters.Content;

namespace SpaceHunters
{
    class LaserManager
    {
        #region Declarations

        static Texture2D laserTexture;
        static Rectangle laserRectangle; // For collision
        static public List<Laser> laserBeam; // List of "bullets"
        const float SECONDS_IN_MINUTE = 60f;
        const float RATE_OF_FIRE = 200f;

        // How quickly you can shoot the laser
        static TimeSpan laserSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
        static TimeSpan previousLaserSpawnTime;

        //Handle the graphics info
        GraphicsDeviceManager graphics;
        static Vector2 graphicsInfo;

        // Determines input form these devices
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        #endregion

        public void Initialize(Texture2D texture, GraphicsDevice Graphics)
        {
            laserBeam = new List<Laser>();
            previousLaserSpawnTime = TimeSpan.Zero;
            laserTexture = texture;
            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;
        }

        private static void AddLaser(Player player)
        {
           
            Animation laserAnimation = new Animation(); 

            laserAnimation.Initialize(
                laserTexture,
                player.position,
                7,
                35,
                1,
                30,
                Color.White,
                1f,
                true); // initlize the laser

            Laser laser = new Laser();  


            var laserPostion = player.position; // Get the starting postion of the laser.

            // Coordinates from where the laser spawns
            laserPostion.Y += 10;
            laserPostion.X += 65;

            laser.Initialize(laserAnimation, laserPostion); //Initialising the laser
            laserBeam.Add(laser);
            // laserSoundInstance.Play();
        }

        private static void FireLaser(GameTime gameTime, Player player, Sounds SND)
        {

            if (gameTime.TotalGameTime - previousLaserSpawnTime > laserSpawnTime)
            {
                previousLaserSpawnTime = gameTime.TotalGameTime; // Fire rate of the laser
                {
                    AddLaser(player); // Adds the laser to the List<Laser>
                    SND.LAZER.Play();
                }
            }
        }

        public void UpdateLaserManager(GameTime gameTime, Player player, GUI guiInfo, ExplosionManager explosion, Sounds SND)
        {
            //Save the previous state of the keyboard and game pad so we can determine single key/button presses
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;

            //Read the current state of the keyboard and gamepad and store it
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentKeyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                FireLaser(gameTime, player, SND);
            }

            for (var i = 0; i < laserBeam.Count; i++)
            {
                laserBeam[i].Update(gameTime); // update laser

                if (!laserBeam[i].Active || laserBeam[i].position.Y > graphicsInfo.Y) // Remove the beam when its deactivated or is at the end of the screen.
                {
                    laserBeam.Remove(laserBeam[i]); // Remove laser
                }

                foreach (Enemy enemy in EnemyManager.basicEnemy) // Local vairable "enemy"
                {
                    Rectangle enemyRectangle = new Rectangle(
                       (int)enemy.position.X,
                       (int)enemy.position.Y,
                       enemy.Width,
                       enemy.Height); // Rectangle used for the enemies

                    foreach (Laser laser in LaserManager.laserBeam) // Local variable "laser"
                    {
                        laserRectangle = new Rectangle(
                        (int)laser.position.X,
                        (int)laser.position.Y,
                        laser.Width,
                        laser.Height); // Reactangle used for the laser


                        if (laserRectangle.Intersects(enemyRectangle))
                        {
                            enemy.health = enemy.health - laser.damage; // Subtract the health of the enemy by the laser damage
                            explosion.LoadExplosionAnimation(enemy.position, SND); // Explosion animation
                            guiInfo.SCORE += 15; // Add +15 to score
                            laser.Active = false; //  After laser connects with enemy rectangle, it will become false
                        }

                    }
                }
            }
        }

        public void DrawLaser(SpriteBatch spriteBatch)
        {
            
            foreach (var l in laserBeam)
            {
                l.Draw(spriteBatch); // Draw the lasers
            }
        } 
    }
}