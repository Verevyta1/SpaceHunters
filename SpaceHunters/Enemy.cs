using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceHunters
{
    class Enemy
    {
        #region Declaration 

        public Animation EnemyAnimation;
        public Vector2 position;     
        public bool Active;
        public int health;
        public int damage;
        public int points;
        float enemySpeed;

        public int Width
        {
            get { return EnemyAnimation.frameWidth; }
        }

        public int Height
        {
            get { return EnemyAnimation.frameHeight; }
        }

        public Vector2 EnemyLocation // Used to set the explosion where the enemyLocation was when bool became false
        {
            get { return position; }
        }
  
        #endregion 

        public void Initialize(Animation ANIMATION, Vector2 POSITION)
        {
            EnemyAnimation = ANIMATION;
            position = POSITION;
            Active = true;
            health = 50;
            damage = 100;   
            points = 100;
            enemySpeed = 7f;  
        }

        public void Update(GameTime gameTime)
        {
            position.Y += enemySpeed;
            EnemyAnimation.position = position;
            EnemyAnimation.Update(gameTime);
            if (position.Y >= 900 || health <= 0) // Removes the enemy when the enemy reaches 500 in the Y axis 
            { Active = false; } // Removes enemy when it becomes false
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            EnemyAnimation.Draw(spriteBatch);
        }
    }
}
