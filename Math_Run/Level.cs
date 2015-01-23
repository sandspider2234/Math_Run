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
    public class Level
    {
        private Texture2D _backImage;
        private List<Rectangle> _inaccessableAreas;
        private Rectangle rec1 = new Rectangle(0, 0, 800, 800);
        Color col = new Color(255, 255, 255, 255);
      //  private List<Obstacle> _levelObstacles;
        private int _points;

        /// <summary>
        /// Creates a new level. 
        /// </summary>
        /// <param name="backImage">The level's background image (800x800)</param>
        /// <param name="inaccessableAreas">The rectanles on which the players shouldn't run - AKA everything that's no a road.</param>
        /// <param name="levelObstacles">All the obstacles we'll have in the levels</param>
        /// <param name="points">How many points you should get for winning.</param>

        //--------------------------CREATE THIS IN THE CONSTRUCTOR , List<Obstacle> levelObstacles,    -----------
        //--------------------------CREATE THIS IN THE CONSTRUCTOR , List<Rectangle> inaccessableAreas,    -----------
        public Level(Texture2D backImage, int points)
        {
            _backImage = backImage;
           // _inaccessableAreas = inaccessableAreas;
          //  _levelObstacles = levelObstacles;
            _points = points;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw( _backImage, rec1, col);
        }
        

    }
}
