 using System;
 using Microsoft.Xna.Framework;
 using Microsoft.Xna.Framework.Content;
 using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    /// <summary>
    /// Represents an obstacle in the game.
    /// </summary>
    public class Obstacle
    {
        private const int ObstacleDimensions = 32;
        public enum ObstacleType
        {
            Fire,
            Rock,
            Knife
        };

        private ObstacleType _type;
        private Rectangle _rectangle;
        private Texture2D _image;
        private Texture2D _fireImage;
        private Texture2D _knifeImage;
        private Texture2D _rockImage;

        private Texture2D _getImageFromType(ObstacleType type, GraphicsDevice device)
        {
            switch (type)
            {
                case ObstacleType.Fire:
                {
                    return _fireImage;
                }
                case ObstacleType.Knife:
                {
                    return _knifeImage;
                }
                case ObstacleType.Rock:
                {
                    return _rockImage;
                }
                default:
                    throw new Exception("Unknown obstacle type");
            }
        }

        public void LoadContent(ContentManager content)
        {
            _fireImage = content.Load<Texture2D>("Fire_obstacle");
            _knifeImage = content.Load<Texture2D>("Knife_obstacle");
            _rockImage = content.Load<Texture2D>("Rock_obstacle");
        }

        /// <summary>
        /// Create a new obstacle.
        /// </summary>
        /// <param name="type">What type of obstacle?</param>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="device">Holds the GraphicsDevice for obstacles.</param>
        public Obstacle(ObstacleType type, int locationX, int locationY, GraphicsDevice device)
        {
            _type = type;
            _rectangle = new Rectangle(locationX, locationY, ObstacleDimensions, ObstacleDimensions);
            _image = _getImageFromType(_type, device);
        }
    }
}
