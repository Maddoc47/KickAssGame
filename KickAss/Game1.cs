using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace KickAss
{       
    /// This is the main type for your game.    
    public class Game1 : Game
    {
        //GAME CLASS FIELDS (member fields are implicitly private)
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont scoreFont;
        SpriteFont fpsFont;        

        Sprite background1, background2, shuttle, ship;
        Direction direction = Direction.down;
        AnimatedSprite ventje;

        float frameRate;
        int score = 0;
        
           
        
                
        //GAME CONSTRUCTOR
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //the graphics device can be used to set the window size
            graphics.PreferredBackBufferHeight = 720;         
            graphics.PreferredBackBufferWidth = 1280;            
        }
        

        //INITIALIZE
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.        /
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            //Create Sprites (refactor in a method?)
            background1 = new Sprite();
            background2 = new Sprite();
            shuttle = new Sprite();            
            ship = new Sprite();                     

            base.Initialize();
        }
        

        // LOAD CONTENT
        ///will be called once per game and is the place to load all of the game content      
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Use this.Content to load your game content here            
            scoreFont = Content.Load<SpriteFont>("my_font");
            fpsFont = Content.Load<SpriteFont>("sfont");
            
            ventje = new AnimatedSprite(Content.Load<Texture2D>(@"textures/professor_walk_cycle_no_hat"), 4,9);
            ventje.position = new Vector2(GraphicsDevice.PresentationParameters.Bounds.Width / 2, GraphicsDevice.PresentationParameters.Bounds.Height / 2);            
            
            background1.rectScale = 2.0f;
            background1.position = new Vector2(0, 0);
            background1.LoadContent(this.Content, @"textures/Desert");
                        
            background2.rectScale = 2.0f;
            background2.position = new Vector2(background1.position.X + background1.rectSize.Width, 0);
            background2.LoadContent(this.Content, @"textures/Desert2");
            
            shuttle.position = new Vector2(800, 500);
            shuttle.LoadContent(this.Content, @"textures/shuttle");
                        
            ship.position = new Vector2(470, 470);
            ship.RotationAngle = 0;
            ship.LoadContent(this.Content, @"textures/fighter_03");
        }

        
        // UNLOAD CONTENT
        ///will be called once per game and is the place to unload game-specific content.              
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }    


        //UPDATE
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.        
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //check for keyboard input
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {                
                direction = Direction.right;
                handlePlayerMovement(direction);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {                
                direction = Direction.left;
                handlePlayerMovement(direction);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {                
                direction = Direction.up;
                handlePlayerMovement(direction);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {                
                direction = Direction.down;
                handlePlayerMovement(direction);
            }
             
            //Add game logic below
            score++;            
            //angle += 0.01f; //this makes the ship rotate
            ship.RotationAngle += 0.01f;            

            //we use the gameTime to calculate the framerate. Divide 1(s) with the runtime of the last Frame
            frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
                        
            base.Update(gameTime);
        }

        //Handle Player Movement - 
        public void handlePlayerMovement(Direction dir)
        {
            switch (dir)
            {
                case Direction.up:
                    ventje.position.Y -= 2;
                    break;
                case Direction.down:
                    ventje.position.Y += 2;
                    break;
                case Direction.left:
                    background1.position.X += 2;
                    background2.position.X += 2;
                    break;
                case Direction.right:
                    background1.position.X -= 2;
                    background2.position.X -= 2;
                    break;
            }
            perFormBackgroundChecks();

            ventje.Update();
        }

        private void perFormBackgroundChecks()
        {
            //Check Background
            if (background1.position.X < -background1.rectSize.Width)
                background1.position.X = background2.position.X + background2.rectSize.Width;
            if (background2.position.X < -background2.rectSize.Width)
                background2.position.X = background1.position.X + background1.rectSize.Width;

            if (background1.position.X > background1.rectSize.Width)
                background1.position.X = background2.position.X - background2.rectSize.Width;
            if (background2.position.X > background2.rectSize.Width)
                background2.position.X = background1.position.X - background1.rectSize.Width;

            //set player movement limits
            if (ventje.position.Y > 650)
                ventje.position.Y = 650;
            if (ventje.position.Y < 0)
                ventje.position.Y = 0;
        }


        //DRAW THE GAME
        /// This is called when the game should draw itself.        
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Drawing Code note: try to keep the number of spritebatches down;            
            spriteBatch.Begin();            
            background1.DrawWithRect(this.spriteBatch);
            background2.DrawWithRect(this.spriteBatch);
            shuttle.Draw(this.spriteBatch);
            ship.DrawRotating(spriteBatch);
            ventje.Draw(spriteBatch, ventje.position, direction);          
            spriteBatch.DrawString(scoreFont, "Score: " + score, new Vector2(50, 30), Color.LightGreen);
            spriteBatch.DrawString(fpsFont, "FPS " + frameRate, new Vector2(1150, 20), Color.Red);
            spriteBatch.End();                              

            base.Draw(gameTime);
        }
    }
}
