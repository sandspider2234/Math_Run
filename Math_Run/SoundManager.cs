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
    public class SoundManager
    {
        public SoundEffect blip, menuSelect, explosion, roadChoice, SelectSound, bgMusic;
        public Song bgLevel1, COOL;
        public SoundManager()
        {
            blip = menuSelect = explosion = roadChoice = null;
            bgMusic = null;
        }
        public void LoadContent(ContentManager Content)
        {
           blip = Content.Load<SoundEffect>("blip");
           //explosion = Content.Load<SoundEffect>("explosion.wav");
           roadChoice = Content.Load<SoundEffect>("choice");
            SelectSound = Content.Load<SoundEffect>("select");
            bgLevel1 = Content.Load<Song>("bgLevel1");
            COOL = Content.Load<Song>("menuBg");
            //bgMusic = Content.Load<Song>("theme");
           
        }
    }
}
