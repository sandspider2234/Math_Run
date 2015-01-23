using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Math_Run
{
    public class Player1
    {
        //int speed;
        public int experience;
        public Texture2D animation;
        Texture2D texture;
        public float timer, interval;
        public Vector2 position123, origin;
        public int currnetFrame, spriteWidth, spriteHeight;
        public Rectangle sourceRect;
        public Rank rank1 = new Rank();
        Color col = new Color(255, 255, 255, 255);
       public Vector2 position;
        Rectangle rec1;
        Rectangle endOfLevel;
        bool isEndLevel = false;
        bool IsUsingArrows;
      

        // object rank = ?
        public Player1(Texture2D newTexture, Vector2 newPosi, bool b)
        {
           position.X = (int)newPosi.X;
           position.Y = (int)newPosi.Y;
          experience = 0;
          this.texture = newTexture;
          rec1 = new Rectangle((int)position.X, (int)position.Y, 32, 32);
         endOfLevel = new Rectangle(0,0,800,100);
         IsUsingArrows = b;
         position = newPosi;
         timer = 0;
         interval = 100f;
         currnetFrame = 1;
         spriteHeight = 32;
         spriteWidth = 32;
         animation = newTexture;
         
        }
        
       
        public void Draw(SpriteBatch spriteBatch)
        {
         
           
            spriteBatch.Draw(animation, position, sourceRect, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
          

        }

        public void Update(GameTime gameTime)
        {
            position.Y = position.Y - 3;
            //check for keyboard input evety frame
            KeyboardState keyState = Keyboard.GetState();
            if (IsUsingArrows == true)
            {
                if (keyState.IsKeyDown(Keys.Up))
                    position.Y = position.Y - 1;

                if (keyState.IsKeyDown(Keys.Left))
                    position.X = position.X - 1;

                if (keyState.IsKeyDown(Keys.Down))
                    position.Y = position.Y + 1;

                if (keyState.IsKeyDown(Keys.Right))
                    position.X = position.X + 1;
            }
            else
            {
                if (keyState.IsKeyDown(Keys.W))
                    position.Y = position.Y - 1;

                if (keyState.IsKeyDown(Keys.A))
                    position.X = position.X - 1;

                if (keyState.IsKeyDown(Keys.S))
                    position.Y = position.Y + 1;

                if (keyState.IsKeyDown(Keys.D))
                    position.X = position.X + 1;
            }
                    // keep player ship in the screen (invisible wall)
            if (position.X <= 0)
                position.X = 0;

            if (position.X >= 800 - texture.Width)
                position.X = 800 - texture.Width;

            if (position.X > 495)
                position.X = 494;

            if (position.X < 460 && position.X>345)
                if (IsUsingArrows)
                { position.X = 459; }
                else
                { position.X = 344; }
                

            if (position.X > 460 && position.X < 402)
                position.X = 459;

            if (position.X <297)
                position.X = 298;


            if (position.Y <= 0)
                position.Y = 0;

            if (position.Y >= 800 - texture.Height)
                position.Y = 800 - texture.Height;
                      
            if (rec1.Intersects(endOfLevel))
            {
                isEndLevel = true;
            }
            //increase the timer by the time of miliseconds
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                //show the next frame
                currnetFrame++;
                timer = 0;
            }
            if (currnetFrame == 3)//if we are in the last frame, make the explosion invisible and resert current to the beginning
            {

                currnetFrame = 0;
            }
            sourceRect = new Rectangle(currnetFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);


                }
        public void LoadContent(ContentManager Content)
        {
           
        }
     

        
            }

        }
       




