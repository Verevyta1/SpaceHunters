using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace SpaceHunters
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player; 
        Texture2D playerTexture; //spiteSheet texture for the player
        Vector2 playerPosition; //Player position in the game world
        float scale = 1f; //Scale for textures, each frame

        //code for the static background
        Texture2D mainbackground;
        Vector2 rectbackground;

        ParallaxingBackground bgLayer;
        Vector2 graphicsInfo; 
        GraphicsDevice details; // Contains image details

        //Enemy
        Texture2D enemyTexture;
        EnemyManager enemy = new EnemyManager();

        //Laser details       
        Texture2D laserTexture; // texture to hold the laser.     
        LaserManager LazerBeam = new LaserManager();// Controls all the laser beams

        // Explosion
        Texture2D explosionTexture;
        ExplosionManager explosion = new ExplosionManager();

        //Sounds
        //laser sound
        private SoundEffect laserSound;
        //Explosion sound
        private SoundEffect explosionSound;
        //Game music
        private Song gameMusic;
        Sounds SND = new Sounds();


        // Shield Drop
        Texture2D shieldDropTexture, lifeDropTexture;
        DropManager shieldDrops = new DropManager();
        DropManager lifeDrops = new DropManager();


        // Pickup animations
        Texture2D pickupTextureShield, pickupTextureLife;
        PickupAnimationManager shieldPickup = new PickupAnimationManager();
        PickupAnimationManager lifePickup = new PickupAnimationManager();

        //Code for the GUI implementation
        SpriteFont guiFont, MenuFont;
        Texture2D legand;
        Texture2D playerlives;
        GUI guiInfo = new GUI();

        //Code for Game Wrapper
        enum GameStates { TitleScreen, start, Playing, WaveComplete, GameOver };
        GameStates gameState = GameStates.TitleScreen;
        float gameOverTimer = 0.0f;
        float gameOverDelay = 16.0f;
        float waveCompleteTimer = 0.0f;
        float waveCompleteDelay = 2.0f;
        Texture2D titleScreen, endScreen;

        public GraphicsDevice Details { get => details; set => details = value; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 700;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 900;   // set this value to the desired height of your window
            graphics.ApplyChanges();
        }

      
        protected override void Initialize()
        {
            player = new Player(); // Player      
            bgLayer = new ParallaxingBackground(); // Background
            SND = new Sounds();
           
            base.Initialize();
        }

       
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice); // DRAWS textures
            graphicsInfo.X = GraphicsDevice.Viewport.Width;
            graphicsInfo.Y = GraphicsDevice.Viewport.Height;

            // Player 
            Animation playerAnimation = new Animation();
            playerTexture = Content.Load<Texture2D>("Images\\shipAnimation");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 138, 138, 3, 100, Color.White, scale, true); //150 is frameW, 115 frameH, 1 is frame count, 30 is frame count
            playerPosition = new Vector2(
                 GraphicsDevice.Viewport.TitleSafeArea.X,
                 GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(playerAnimation, playerPosition, graphicsInfo);

            // Background elements
            bgLayer.Initialize(Content, "Backgrounds\\BGLayer", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -1);
            mainbackground = Content.Load<Texture2D>("Backgrounds\\mainbackground");
            details = GraphicsDevice;

            // Enemy
            enemyTexture = Content.Load<Texture2D>("Images\\enemy");
            enemy.Initialize(enemyTexture, details);

            // Laser
            laserTexture = Content.Load<Texture2D>("Images\\laser"); 
            LazerBeam.Initialize(laserTexture, details);

            // Explosion
            explosionTexture = Content.Load<Texture2D>("Images\\explosionAnimation");
            explosion.Initialize(explosionTexture, details);

            // Drop Animation
            shieldDropTexture = Content.Load<Texture2D>("Images\\shieldPowerUpAnimation");
            shieldDrops.InitializeShield(shieldDropTexture, details);
            lifeDropTexture = Content.Load<Texture2D>("Images\\lifePowerUpAnimation");
            lifeDrops.InitializeLife(lifeDropTexture, details);

            // Pickup animation
            pickupTextureShield = Content.Load<Texture2D>("Images\\shieldPickupAnimation");     
            shieldPickup.InitializeShieldPickup(pickupTextureShield, details);

            pickupTextureLife = Content.Load<Texture2D>("Images\\lifePickupAnimation");
            lifePickup.InitializeLifePickup(pickupTextureLife, details);

            // GUI elements
            guiFont = Content.Load<SpriteFont>("GUI\\GUIFont");
            MenuFont = Content.Load<SpriteFont>("MenuFont");
            legand = Content.Load<Texture2D>("GUI\\legand");
            playerlives = Content.Load<Texture2D>("GUI\\HeartLives");

            //Titlescreen and Endscreen
            titleScreen = Content.Load<Texture2D>("Images\\MainMenu");
            endScreen = Content.Load<Texture2D>("Images\\EndMenu");

            //laser sound
            laserSound = Content.Load<SoundEffect>("Sounds\\laser_sound");
            explosionSound = Content.Load<SoundEffect>("Sounds\\explosion_sound");
            gameMusic = Content.Load<Song>("Sounds\\game_music");
            SND.Initialize(laserSound, explosionSound);
            MediaPlayer.Play(gameMusic);
                
                }

       
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

       
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            
            bgLayer.Update(gameTime); // Upadting the background for parallaxing
            

            switch (gameState)
            {
                case GameStates.TitleScreen:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        StartGame();

                        gameState = GameStates.start;
                    }
                    break;
                case GameStates.start:

                    waveCompleteTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (waveCompleteTimer > waveCompleteDelay)
                    {
                        gameState = GameStates.Playing;
                        waveCompleteTimer = 0.0f;
                    }

                    break;

                case GameStates.Playing:
                    player.Update(gameTime); // Player
                    enemy.UpdateEnemies(gameTime, player, guiInfo, explosion, SND); // Enemy 
                    LazerBeam.UpdateLaserManager(gameTime, player, guiInfo, explosion, SND); // Laser   
                    shieldDrops.UpdateShieldDrops(gameTime, player, guiInfo, shieldPickup); // Shield drop
                    lifeDrops.UpdateLifeDrops(gameTime, player, guiInfo, lifePickup); // Life drop
                    explosion.UpdateExplosion(gameTime); // Explosion animation
                    shieldPickup.UpdateShieldPickupAnimation(gameTime); // Update pickup animation for shield drop
                    lifePickup.UpdateLifePickupAnimation(gameTime); // Update pickup animation for life drop

                    if (guiInfo.SCORE >= guiInfo.NEXTWAVE)
                    {
                        gameState = GameStates.WaveComplete;
                        guiInfo.NEXTWAVE = guiInfo.SCORE + (guiInfo.SCORE + 100); // Upgrading for the next wave
                        guiInfo.WAVE++;
                    }

                    break;

                case GameStates.WaveComplete:
                    waveCompleteTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (waveCompleteTimer > waveCompleteDelay)
                    {
                        NewLevel();
                        gameState = GameStates.Playing;
                        waveCompleteTimer = 0.0f;
                    }

                    break;

                case GameStates.GameOver:
                    gameOverTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (gameOverTimer > gameOverDelay)
                    {
                        gameState = GameStates.TitleScreen;
                        gameOverTimer = 0.0f;
                    }
      
                    break;
            }

            base.Update(gameTime);
        }

      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(mainbackground, rectbackground, Color.White);  //Drawing the main background
            bgLayer.Draw(spriteBatch); //Drawing the parallaxing background

            if(gameState==GameStates.TitleScreen)
            {
                spriteBatch.Draw(titleScreen, new Rectangle(0, 0, titleScreen.Width, titleScreen.Height), Color.White);
            }
            if(gameState == GameStates.start)
            {
                spriteBatch.DrawString(MenuFont, "Wave: " + guiInfo.WAVE, new Vector2(300, 300), Color.Yellow);
            }
            if (gameState == GameStates.Playing || gameState == GameStates.WaveComplete)
            {
                player.Draw(spriteBatch); // Draws the player
                enemy.DrawEnemies(spriteBatch); // Draws basic enemy
                LazerBeam.DrawLaser(spriteBatch); // Draws Laser
                explosion.DrawExplosions(spriteBatch); // Draws Explosions
                shieldDrops.DrawShieldDrop(spriteBatch); // Draws shield drop
                shieldPickup.DrawShieldPickups(spriteBatch); // Draw shield pickup animation
                lifeDrops.DrawLifeDrop(spriteBatch); // Draws life drop
                lifePickup.DrawLifePickups(spriteBatch); // Draws Pickup animation
                
                PlayerDeath();

                    // Drawing GUI elements
                    spriteBatch.Draw(legand, new Vector2(0, 840), Color.White);
                    spriteBatch.DrawString(guiFont, "Score: " + guiInfo.SCORE, new Vector2(5, 842), Color.LightGreen);
                    spriteBatch.DrawString(guiFont, "Wave: " + guiInfo.WAVE, new Vector2(5, 870), Color.Yellow);
                    spriteBatch.DrawString(guiFont, "Shield: " + guiInfo.SHIELD, new Vector2(120, 842), Color.LightBlue);

                    for (int i = 1; i <= guiInfo.LIVES; i++)
                    {
                        spriteBatch.DrawString(guiFont, "Lives: ", new Vector2(120, 870), Color.White);
                        spriteBatch.Draw(playerlives, new Vector2(140 + i * 25, 870), Color.White);
                    }
            }

            if (gameState == GameStates.WaveComplete)
            {
                spriteBatch.DrawString(MenuFont, "Wave: " + guiInfo.WAVE, new Vector2(300, 300), Color.Yellow);
            }
            if (gameState == GameStates.GameOver)
            {
                spriteBatch.Draw(endScreen, new Rectangle(0, 0, titleScreen.Width, titleScreen.Height), Color.White);
            }

            spriteBatch.End(); // Stop drawing
            base.Draw(gameTime);
        }

        public void StartGame()
        {
            guiInfo.WAVE = guiInfo.WAVE;
            guiInfo.SCORE = guiInfo.SCORE;
            guiInfo.LIVES = guiInfo.LIVES;
            guiInfo.NEXTWAVE = 750;
            guiInfo.SHIELD = guiInfo.SHIELD;
            guiInfo.Initialize(0, 100, 5, 1);
            NewLevel();
        }

        public void NewLevel()
        {
            Animation shipanimation = new Animation();
            shipanimation.Initialize(playerTexture, Vector2.Zero, 138, 138, 3, 100, Color.White, scale, true);
            playerPosition = new Vector2(
                GraphicsDevice.Viewport.TitleSafeArea.X,
                GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(shipanimation, playerPosition, graphicsInfo);
            details = GraphicsDevice;
            enemy.Initialize(enemyTexture, details);
            LazerBeam.Initialize(laserTexture, details);
            explosion.Initialize(explosionTexture, details);
            shieldDrops.InitializeShield(shieldDropTexture, details);
            lifeDrops.InitializeLife(lifeDropTexture, details);
        }

        private void PlayerDeath()
        {
            if(guiInfo.LIVES<=0)
            {
                gameState = GameStates.GameOver;
            }
        }
    }
}
