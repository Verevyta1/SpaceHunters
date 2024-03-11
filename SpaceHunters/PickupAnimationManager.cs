using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceHunters
{
    class PickupAnimationManager
    {
        #region Declarations

        List<PickupAnimationShield> shieldPickup; // List 
        List<PickupAnimationLife> lifePickup; // List
        Texture2D shieldPickupTexture, lifePickupTexture; // Texture 
        Vector2 shieldGraphicsInfo, lifeGraphicsInfo; // Vector2 for texture information

        #endregion

        public void InitializeShieldPickup(Texture2D textureS, GraphicsDevice Graphics)
        {
            // Texture info
            shieldGraphicsInfo.X = Graphics.Viewport.Width;
            shieldGraphicsInfo.Y = Graphics.Viewport.Height;
            // Initialize
            shieldPickup = new List<PickupAnimationShield>(); // Shield      
            shieldPickupTexture = textureS;
        }

        public void InitializeLifePickup(Texture2D textureL, GraphicsDevice Graphics)
        {
            // Texture info
            lifeGraphicsInfo.X = Graphics.Viewport.Width;
            lifeGraphicsInfo.Y = Graphics.Viewport.Height;
            // Initialize
            lifePickup = new List<PickupAnimationLife>();  // Life
            lifePickupTexture = textureL; 
        }

        public void LoadShieldPickupAnimation(Vector2 shieldDropPosition)
        {
            Animation shieldPickupAnimation = new Animation(); // Object      
            shieldPickupAnimation.Initialize(shieldPickupTexture, shieldDropPosition, 50, 45, 5, 120, Color.White,1.0f, true); // Animation parameters
            PickupAnimationShield pickupShield = new PickupAnimationShield(); // List
            pickupShield.Initialize(shieldPickupAnimation, shieldDropPosition); // Initialize
            shieldPickup.Add(pickupShield); // Add to list 
        }

        public void LoadLifePickupAnimation(Vector2 lifeDropPosition)
        {
            Animation lifePickupAnimation = new Animation(); // Object
            lifePickupAnimation.Initialize(lifePickupTexture, lifeDropPosition, 50, 45, 5, 120, Color.White, 1.0f, true); // Animation parameters         
            PickupAnimationLife pickupLife = new PickupAnimationLife();  // List
            pickupLife.Initialize(lifePickupAnimation, lifeDropPosition); // Initialize
            lifePickup.Add(pickupLife); // Add to list
        }

        public void UpdateShieldPickupAnimation(GameTime gameTime)
        {
            for (var i = 0; i < shieldPickup.Count; i++)
            {
                shieldPickup[i].UpdateShield(gameTime); // Update pickups in game world
                if (!shieldPickup[i].active) // If pickups is not active
                    shieldPickup.Remove(shieldPickup[i]); // Remove it
            }
        }

        public void UpdateLifePickupAnimation(GameTime gameTime)
        { 
            for (var l = 0; l < lifePickup.Count; l++)
            {
                lifePickup[l].UpdateLife(gameTime); // Update pickups in game world
                if (!lifePickup[l].active) // If pickups is not active
                    lifePickup.Remove(lifePickup[l]); // Remove it
            }
        }

        public void DrawShieldPickups(SpriteBatch spriteBatch)
        {
            foreach (var s in shieldPickup)
            {
                 s.Draw(spriteBatch); // Draw animation for picking up shield
            }
        }


        public void DrawLifePickups(SpriteBatch spriteBatch)
        {
            {
                foreach (var l in lifePickup)
                {
                    l.Draw(spriteBatch); // Draw animatoin for life pickup
                }
            }
        }

    }
}
