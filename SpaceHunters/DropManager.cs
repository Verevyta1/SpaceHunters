using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceHunters
{
    class DropManager
    {
        #region

        Texture2D shieldTexture, lifeTexture; // Texture
        Vector2 graphicsInfoShield, graphicsInfoLife; // Position 
        
        // Lists
        static public List<DropShield> shieldDrop = new List<DropShield>(); // List to keep track of shield drops
        static public List<DropLife> lifeDrop = new List<DropLife>(); // List to keep track of life drops

        // Timer
        TimeSpan shieldSpawnTimer = TimeSpan.FromSeconds(12f); // Timer for the spawns for the shield drops
        TimeSpan previousShieldSpawnTime = TimeSpan.Zero; // Sets the previous spawn timer to zero 
        TimeSpan lifeSpawnTimer = TimeSpan.FromSeconds(15f); // Timer for the spawns for the life drops
        TimeSpan previousLifeSpawnTime = TimeSpan.Zero; // Sets the previous spawn timer to zero 

        // Random
        Random randomShield = new Random(); 
        Random randomLife = new Random();

        #endregion

        public void InitializeShield(Texture2D shieldTEXTURE, GraphicsDevice shieldGraphics)
        {
            graphicsInfoShield.X = shieldGraphics.Viewport.Width;
            graphicsInfoShield.Y = shieldGraphics.Viewport.Height;
            shieldTexture = shieldTEXTURE;
        }

        public void InitializeLife(Texture2D lifeTEXTURE, GraphicsDevice lifeGraphics)
        {
            graphicsInfoLife.X = lifeGraphics.Viewport.Width;
            graphicsInfoLife.Y = lifeGraphics.Viewport.Height;
            lifeTexture = lifeTEXTURE;
        }

        private void LoadShieldDrop()
        {           
            Animation shieldAnimation = new Animation(); // Object           
            shieldAnimation.Initialize(shieldTexture, Vector2.Zero, 45, 45, 5, 60, Color.White, 1.0f,true); // Initialize animation         
            Vector2 shieldDropPosition = new Vector2(randomShield.Next(30, 630), randomShield.Next(-60, -10)); // Location in game world 
            DropShield shield = new DropShield(); // Object
            shield.Initialize(shieldAnimation, shieldDropPosition); // Initialize          
            shieldDrop.Add(shield);  // Add to list
        }

        private void LoadLifeDrop()
        {           
            Animation lifeAnimation = new Animation(); // Object        
            lifeAnimation.Initialize(lifeTexture, Vector2.Zero, 45, 45, 5, 60, Color.White, 1.0f, true); // Initialize animation     
            Vector2 lifeDropPosition = new Vector2(randomLife.Next(30, 630), randomLife.Next(-60, -10)); // Location in game world            
            DropLife life = new DropLife(); // Object          
            life.Initialize(lifeAnimation, lifeDropPosition); // Initialize life drop
            lifeDrop.Add(life); // Add to list
        }

        public static void UpdateCollisionShieldDrop(Player player, GUI guiInfo, PickupAnimationManager pickup)
        {
            Rectangle playerRec, shieldRec; // Declaration

            playerRec = new Rectangle(
                (int)player.position.X,
                (int)player.position.Y,
                player.Width,
                player.Height); // Rectangle attributes for player

            for (int i = 0; i < shieldDrop.Count; i++) // Collision beween player and shield drop
            {
                shieldRec = new Rectangle(
                    (int)shieldDrop[i].position.X,
                    (int)shieldDrop[i].position.Y,
                    shieldDrop[i].Width,
                    shieldDrop[i].Height); // Rectangle attributes 

                if (playerRec.Intersects(shieldRec))
                {
                    player.shield = 100; // sets shield to 100
                    guiInfo.SHIELD = player.shield; // Updates the GUI to show the correct shield amount
                    shieldDrop[i].active = false; // Deletes shield from list
                    pickup.LoadShieldPickupAnimation(shieldDrop[i].ShieldDropLocation); // Loads the animation in the location of where the drop was picked up
                }
            }
        }

        public static void UpdateCollisionLifeDrop(Player player, GUI guiInfo, PickupAnimationManager pickup)
        {
            Rectangle playerRec, lifeRec; // Declaration

            playerRec = new Rectangle(
                (int)player.position.X,
                (int)player.position.Y,
                player.Width,
                player.Height); // Rectangle attributes for player

            for (int i = 0; i < lifeDrop.Count; i++) // Collision beween player and life drop
            {
                lifeRec = new Rectangle(
                   (int)lifeDrop[i].position.X,
                    (int)lifeDrop[i].position.Y,
                    lifeDrop[i].Width,
                    lifeDrop[i].Height); // Rectangle attributes

                if (playerRec.Intersects(lifeRec))
                {
                    player.lives++; // adds 1 life to the player
                    guiInfo.LIVES = player.lives; // Updates the GUI to show the correct shield amount
                    lifeDrop[i].active = false; // Deletes life from list
                    pickup.LoadLifePickupAnimation(lifeDrop[i].LifeDropLocation); // Loads the animation in the location of where the drop was picked up
                }
            }
        }

        public void UpdateShieldDrops (GameTime gameTime, Player player, GUI guiInfo, PickupAnimationManager  shieldPickup) 
        {
            if (gameTime.TotalGameTime - previousShieldSpawnTime > shieldSpawnTimer) // Math for the shield drop timer
            {
                previousShieldSpawnTime = gameTime.TotalGameTime;
                LoadShieldDrop();
            }

            UpdateCollisionShieldDrop(player, guiInfo, shieldPickup); // Update
           
            for (int i = (shieldDrop.Count - 1); i >= 0; i--)
            {
                shieldDrop[i].Update(gameTime);
                if (shieldDrop[i].active == false)
                { shieldDrop.RemoveAt(i); } // Removes shield drop if bool is false
            }   
        }

        public void UpdateLifeDrops (GameTime gameTime, Player player, GUI guiInfo, PickupAnimationManager lifePickup)
        {
            if (gameTime.TotalGameTime - previousLifeSpawnTime > lifeSpawnTimer) // Math for the life drop timer
            {
                previousLifeSpawnTime = gameTime.TotalGameTime;
                LoadLifeDrop();
            }

            UpdateCollisionLifeDrop(player, guiInfo, lifePickup); // Update 

            for (int i = (lifeDrop.Count - 1); i >= 0; i--)
            {
                lifeDrop[i].Update(gameTime);
                if (lifeDrop[i].active == false)
                { lifeDrop.RemoveAt(i); } // Removes life drop if bool is false
            }
        }

        public void DrawShieldDrop(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < shieldDrop.Count; i++) // Checks for shield drops that are spawned
            { shieldDrop[i].Draw(spriteBatch); }; // Draws shield drop         
        }

        public void DrawLifeDrop(SpriteBatch spriteBatch)
        {
            for (int l = 0; l < lifeDrop.Count; l++) // Cehcks for life drops that are spawned
            { lifeDrop[l].Draw(spriteBatch); } // Draws life drop
        }
    }
}
