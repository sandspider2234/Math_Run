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
    public class Menu
    {
        public Texture2D texture;
        Vector2 position, namePosition;
        public Rectangle rectangle;
        Color col = new Color(255, 255, 255, 255);
        public Vector2 size;
        SoundManager sm = new SoundManager();
        public Menu(Texture2D newTexture)
        {
            texture = newTexture;
            size.X = texture.Width;
            size.Y = texture.Height;
           
        }
        bool down;
       
        public bool isClicked=false;
        
        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
          
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);


            // mouse enter play button
            if (mouseRectangle.Intersects(rectangle))
            {
                if (col.A == 255) down = false;
                if (col.A == 0) down = true;
                if (down == true)
                {
                    col.A += 3;
                }
                else
                {
                    col.A -= 3;
                }
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
            }
            else if (col.A < 255)
            {
                col.A += 3;
                isClicked = false;
            }
            if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
            if (isClicked==true)
            {
                sm.SelectSound.Play();
            }

           

        }
        public void SetPosition(Vector2 newPosition, Vector2 newNamePosition)
        {
            position = newPosition;
            namePosition = newNamePosition;
          
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, col);
        }

        public void LoadContent(ContentManager Content)
        {
            sm.LoadContent(Content);
        }
     

    }
}
