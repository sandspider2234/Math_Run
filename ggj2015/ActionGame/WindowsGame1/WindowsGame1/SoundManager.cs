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

namespace WindowsGame1
{
    public class SoundManager
    {
        public SoundEffect blip, menuSelect, explosion, roadChoice;
        public Song bgMusic;
        public SoundManager()
        {
            blip = menuSelect = explosion = roadChoice = null;
            bgMusic = null;
        }
        public void LoadContent(ContentManager Content)
        {
            blip = Content.Load<SoundEffect>("blip.wav");
            menuSelect = Content.Load<SoundEffect>("select.wav");
            explosion = Content.Load<SoundEffect>("explosion.wav");
            roadChoice = Content.Load<SoundEffect>("choice.wav");
            bgMusic = Content.Load<Song>("theme");
        }
    }
}
