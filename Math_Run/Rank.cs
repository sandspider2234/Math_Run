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
    public class Rank
    {
        public Rank()
        {
        }

        public enum Ranking
        {
            None,
            Baby,
            Kindergarten,
            Elementary,
            Middle,
            High,
            College,
            Uni,
            Grad,
            Professor,
            Master,
            SuperMaster,
            Alpaca
        };

        public Ranking RetRank(int xp)
        {
            if (xp < 100)
                return Ranking.Baby;
            if (xp >= 100 && xp < 300)
                return Ranking.Kindergarten;
            if (xp >= 300 && xp < 400)
                return Ranking.Elementary;
            if (xp >= 400 && xp < 500)
                return Ranking.Middle;
            if (xp >= 500 && xp < 650)
                return Ranking.High;
            if (xp >= 650 && xp < 1000)
                return Ranking.College;
            if (xp >= 1000 && xp < 1500)
                return Ranking.Uni;
            if (xp >= 1500 && xp < 2000)
                return Ranking.Grad;
            if (xp >= 2000 && xp < 3500)
                return Ranking.Master;
            if (xp >= 3500 && xp < 5000)
                return Ranking.SuperMaster;
            if (xp >= 5000)
                return Ranking.Alpaca;
            else
                return Ranking.None;
        }
    }
}
