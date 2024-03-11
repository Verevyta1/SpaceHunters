using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceHunters
{
    class ExplosionManager
    {
        #region Declarations

        List<Explosion> explosions; // List for explosions
        Texture2D explosionTexture; // Texture for the explosion animation
        Vector2 graphicsInfo; // Vector2 for texture information

        #endregion

        public void Initialize(Texture2D texture, GraphicsDevice Graphics)
        {
            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;
            explosions = new List<Explosion>(); // Initialize 
            explosionTexture = texture; 
        }

        public void LoadExplosionAnimation (Vector2 enemyDeathPosition, Sounds SND) // Load
        {
            Animation explosionAnimation = new Animation();

            explosionAnimation.Initialize(explosionTexture,
                enemyDeathPosition,
                133,
                134,
                12,
                30,
                Color.White,
                1.0f,
                true); // Parameters for texture and animation 

            Explosion explosion = new Explosion(); 
            explosion.Initialize(explosionAnimation, enemyDeathPosition);
            explosions.Add(explosion); // Add explosion to list
            SND.EXPLOSION.Play();
        }

        public void UpdateExplosion(GameTime gameTime) // Update
        {
            for (var i = 0; i < explosions.Count; i++) 
            {
                explosions[i].Update(gameTime); // Update explosion in game world
                if (!explosions[i].active) // If explosion is not active
                    explosions.Remove(explosions[i]); // Remove explosion 
            }
        }

        public void DrawExplosions(SpriteBatch spriteBatch)
        {        
            foreach (var i in explosions) 
            {
                i.Draw(spriteBatch); // draw explosions
            }
        }
    }
}
