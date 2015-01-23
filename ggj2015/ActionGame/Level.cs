using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    /// <summary>
    /// Represents a level. Use this to have multiple levels.
    /// </summary>
    public class Level
    {
        private Texture2D _backImage;
        private List<Rectangle> _inaccessableAreas;
        private List<Obstacle> _levelObstacles;
        private int _points;

        /// <summary>
        /// Creates a new level. 
        /// </summary>
        /// <param name="backImage">The level's background image (800x800)</param>
        /// <param name="inaccessableAreas">The rectanles on which the players shouldn't run - AKA everything that's no a road.</param>
        /// <param name="levelObstacles">All the obstacles we'll have in the levels</param>
        /// <param name="points">How many points you should get for winning.</param>
        public Level(Texture2D backImage, List<Rectangle> inaccessableAreas, List<Obstacle> levelObstacles, int points)
        {
            _backImage = backImage;
            _inaccessableAreas = inaccessableAreas;
            _levelObstacles = levelObstacles;
            _points = points;
        }
    }
}
