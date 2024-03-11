using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceHunters
{
    class EnemyManager
    {
        #region Declarations

        Texture2D enemyTexture;
        Vector2 graphicsInfo; 
        static public List<Enemy> basicEnemy = new List<Enemy>(); // List to keep track of the enemies spawned       
        TimeSpan enemySpawnTimer = TimeSpan.FromSeconds(0.7f); // Timer for the spawns
        TimeSpan previousSpawnTime = TimeSpan.Zero; // Sets the previous spawn timer to zero   
        Random random = new Random();

        #endregion

        public void Initialize(Texture2D texture, GraphicsDevice Graphics)
        {
            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;
            enemyTexture = texture;
        }

        private void LoadEnemy()
        {
            Animation enemyAnimation = new Animation();  // Object

            enemyAnimation.Initialize(
                enemyTexture,
                Vector2.Zero,
                56,
                56,
                1,
                30,
                Color.White,
                1f,
                true);   // Initialise the animation with the correct animation details 

                   
            Vector2 position = new Vector2(random.Next(40,610),random.Next( -50, -20 )); // random spot on the X axis, then random spot on the Y axis within the margin    
            Enemy enemy = new Enemy(); // Object
            enemy.Initialize(enemyAnimation, position); // Initialize the enemy
            basicEnemy.Add(enemy); // Adds enemy to the list
        }

        public static void UpdateCollision(Player player, GUI guiInfo, ExplosionManager explosion, Sounds SND)  
        {         
            Rectangle playerRec, enemyRec; // Declaration of rectangles

            playerRec = new Rectangle(
                (int)player.position.X,
                (int)player.position.Y,
                player.Width, player.Height); // Player re

            for (int i = 0; i < basicEnemy.Count; i++) // Do the collision between the player and the enemies
            {
                enemyRec = new Rectangle(
                    (int)basicEnemy[i].position.X,
                    (int)basicEnemy[i].position.Y,
                    basicEnemy[i].Width,
                    basicEnemy[i].Height);


                if (playerRec.Intersects(enemyRec)) // Checking to see if the rectangles collide 
                {
                    
                    if (player.shield == 0)
                    {
                        player.lives--; // If the player has no shield, remove 1 life
                        guiInfo.LIVES = player.lives; // Update the GUI
                        explosion.LoadExplosionAnimation(player.PlayerPos, SND); // Plays the explosion when player takes damage with no shields

                    }


                    if (player.shield == 100)
                    {
                        player.shield -= basicEnemy[i].damage;// Subtract the shield from the player based on the enemy damage
                        guiInfo.SHIELD = player.shield; // Sets shield UI to display 0
                    }

                    //Enemy
                    basicEnemy[i].health = 0;  // Since the enemy collided with the player destroy it
                    explosion.LoadExplosionAnimation(basicEnemy[i].EnemyLocation, SND); // Show the explosion where the enemy was

                    if (player.lives == 0) // If the player has hearts remaining
                    {
                        player.active = false; // Player dies                                   
                    }
                }
            }
        }

        public void UpdateEnemies(GameTime gameTime, Player player, GUI guiInfo, ExplosionManager explosion, Sounds sounds)
        {
            
            if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTimer) // Timer for enemy spawn
            {
                previousSpawnTime = gameTime.TotalGameTime;
                LoadEnemy();
            }

            UpdateCollision(player, guiInfo, explosion, sounds);
       
             for (int i = (basicEnemy.Count - 1); i >= 0; i--)
             {
                 basicEnemy[i].Update(gameTime);
                 if (basicEnemy[i].Active == false)
                 { basicEnemy.RemoveAt(i); } // Removes enemy when bool is false
             }
        }

        public void DrawEnemies(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < basicEnemy.Count; i++)
            { basicEnemy[i].Draw(spriteBatch); }
        }
    }
}
