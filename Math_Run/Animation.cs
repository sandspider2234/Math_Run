using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Math_Run
{
    public class Animation
    {
        public Texture2D animation;
        public float timer, interval;
        public Vector2 position, origin;
        public int currnetFrame, spriteWidth, spriteHeight;
        public Rectangle sourceRect;

        public Animation(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            animation = newTexture;
            timer = 0;
            interval = 100f;
            currnetFrame = 1;
            spriteHeight = 32;
            spriteWidth = 32;
           
        }
        public void LoadContent(ContentManager Content)
        {

        }
        public void Update(GameTime gameTime)
        {
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
        public void Draw(SpriteBatch spriteBatch)
        {
            
                spriteBatch.Draw(animation, position, sourceRect, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
            
        }
    }
}
