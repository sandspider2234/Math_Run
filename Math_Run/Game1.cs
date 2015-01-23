using System;
using System.Collections.Generic;
using System.Linq;
using WindowsGame1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Math_Run
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Menu menu1=null;
        SoundManager sm = new SoundManager();
        Level level1, level2;
        private List<Obstacle> level1Obstacles;
        Player1 player1;
        Player1 player2;
        CharacterSelection Character = new CharacterSelection();
        Riddle tom = new Riddle(250, 150, 10, Keys.A, Keys.W, Keys.D);
        Riddle tom2 = new Riddle(450, 150, 10, Keys.Left, Keys.Up, Keys.Right);
       public enum gameState
        {
            menu,
           celection,
            level1,
            level2,
            level3,
            gameOver,
            pause

        }
      public gameState CurrnetGameState = gameState.menu;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
        }

      
        protected override void Initialize()
        {
           

            base.Initialize();
        }

       
        protected override void LoadContent()
        {
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            level1Obstacles.Add(new Obstacle(Obstacle.ObstacleType.Fire, 290, 414, graphics.GraphicsDevice));
            level1Obstacles.Add(new Obstacle(Obstacle.ObstacleType.Fire, 448, 414, graphics.GraphicsDevice));
            level1Obstacles.Add(new Obstacle(Obstacle.ObstacleType.Rock, 322, 257, graphics.GraphicsDevice));
            level1Obstacles.Add(new Obstacle(Obstacle.ObstacleType.Rock, 482, 257, graphics.GraphicsDevice));
            menu1 = new Menu(Content.Load<Texture2D>("Play_Button"), Content.Load<Texture2D>("Logo"));
            level1 = new Level(Content.Load<Texture2D>("level1"), 50); 
            level2 = new Level(Content.Load<Texture2D>("level2"), 100);
            menu1.SetPosition(new Vector2(260, 360), new Vector2(200,40));
            menu1.LoadContent(Content);
            IsMouseVisible = true;
            player1 = new Player1(Content.Load<Texture2D>("char_2_animation"),new Vector2(464,750),true);
            player2 = new Player1(Content.Load<Texture2D>("char_3_animation"), new Vector2(304, 750), false);
            sm.LoadContent(Content);
            player1.LoadContent(Content);
            Character.LoadContent(Content);
            tom.LoadContent(Content);
            tom2.LoadContent(Content);
            MediaPlayer.Play(sm.COOL);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

      
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            MouseState mouse = Mouse.GetState();
            Rectangle mouseRectangle1 = new Rectangle(mouse.X, mouse.Y, 1, 1);

           

            if (Character.GOTOlvl1) {
                Character.GOTOlvl1 = false;
                CurrnetGameState = gameState.level1;
                //start level
            }
            
            switch (CurrnetGameState)
            {
                case gameState.menu:
                    {
                        
                        menu1.Update(mouse);

                       
                        if (mouseRectangle1.Intersects(menu1.rectangle)&&mouse.LeftButton == ButtonState.Pressed)
                        {
                            Character.Update(mouse);
                                CurrnetGameState = gameState.celection;
                        }
                        break;
                    }
                case gameState.celection:
                    {
                        Character.Update(mouse);
                      
                       
                        break;
                    }
                  

                case gameState.level1:
                    {
                        player1.Update(gameTime);
                        player2.Update(gameTime);
                        tom.Update(Keyboard.GetState());
                        tom2.Update(Keyboard.GetState());
                        if (player1.sourceRect.Intersects(level1Obstacles[0]._rectangle))
                        {
                            sm.explosion.Play();
                        }
                        break;
                    }
                case gameState.level2:
                {
                    player1.Update(gameTime);
                    player2.Update(gameTime);
                    tom.Update(Keyboard.GetState());
                    tom2.Update(Keyboard.GetState());
                    break;
                }

            }

            base.Update(gameTime);
        }

       
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Silver);

            spriteBatch.Begin();
            switch (CurrnetGameState)
            {
                case gameState.menu:
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("backround"), new Rectangle(0, 0, 800, 800), Color.White);
                      //  spriteBatch.Draw(Content.Load<Texture2D>("Play_Button"), new Rectangle(260, 360, 300, 120), Color.White);
                        menu1.Draw(spriteBatch);
                        menu1.DrawLogo(spriteBatch);
                        break;
                    }
                case gameState.celection:
                    {
                        Character.Draw(spriteBatch);
                       
                        break;
                    }
                case gameState.level1:
                    {
                        level1.Draw(spriteBatch);
                        player1.Draw(spriteBatch);
                        player2.Draw(spriteBatch);
                        tom.Draw(spriteBatch);
                        tom2.Draw(spriteBatch);
                        break;
                    }
                case gameState.level2:
                    {
                        level2.Draw(spriteBatch);
                        player1.Draw(spriteBatch);
                        player2.Draw(spriteBatch);
                        tom.Draw(spriteBatch);
                        tom2.Draw(spriteBatch);
                        break;
                    }
            }
              spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
