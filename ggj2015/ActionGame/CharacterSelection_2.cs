using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
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
    public class Character
    {
        public Rectangle Rectangle { get; set; }
        public Rectangle BigRectangle { get; private set; }
        public Texture2D Texture2D { get; private set; }
        public bool Selected { get; set; }
        public bool Locked { get; set; }

        public Character(Texture2D texture, Rectangle rectangle = new Rectangle(), Rectangle bigRectangle = new Rectangle())
        {
            Texture2D = texture;
            Rectangle = rectangle;
            BigRectangle = bigRectangle;
            Selected = false;
            Locked = false;
        }
    }

    public class CharacterSelection
    {
        private Texture2D _confirmButtonGray;
        private Texture2D _confirmButtonGreen;
        private Texture2D _characterSelection;
        private readonly Rectangle _confirmRectangle = new Rectangle(150, 650, 500, 120);
        private readonly Rectangle _backRectangle = new Rectangle(0,0,800,800);
        private readonly Texture2D[] _smallCharacters = new Texture2D[8];
        private readonly Character[] _leftCharecters = new Character[8];
        private readonly Character[] _rightCharecters = new Character[8];
        private int _howManySelected;
        private SoundManager _sm;
        private bool _selectedAreUnlocked;

        private const int LeftBigX = 36;
        private const int LeftBigY = 256;
        private const int RightBigX = 441;
        private const int RightBigY = 256;
        private const int BigDimensions = 320;
        
        private const int LeftInitialX = 36;
        private const int LeftInitialY = 589;
        private const int RightInitialX = 441;
        private const int RightInitialY = 589;
        private const int SmallDimensions = 32;

        private void _drawSelectedCharacters(SpriteBatch spriteBatch)
        {
            foreach (var character in _leftCharecters.Concat(_rightCharecters).Where(character => character.Selected))
            {
                spriteBatch.Draw(character.Texture2D, character.BigRectangle,
                    character.Locked ? Color.Black : Color.White);
            }
        }

        private static void _drawSmallCharacters(SpriteBatch spriteBatch, IEnumerable<Character> characters)
        {
            foreach (var character in characters)
            {
                spriteBatch.Draw(character.Texture2D, character.Rectangle, character.Locked ? Color.Black : Color.White);
            }
        }

        public void LoadContent(ContentManager content)
        {
            _confirmButtonGray = content.Load<Texture2D>("Confirm_button_gray");
            _confirmButtonGreen = content.Load<Texture2D>("Confirm_button_green");
            _characterSelection = content.Load<Texture2D>("character_select");
            for (int i = 0; i < _smallCharacters.Length; ++i)
            {
                int charnum = i + 1;
                _smallCharacters[i] = content.Load<Texture2D>("char_" + charnum.ToString(CultureInfo.InvariantCulture));
            }

            var leftBig = new Rectangle(LeftBigX, LeftBigY, BigDimensions, BigDimensions);
            var rightBig = new Rectangle(RightBigX, RightBigY, BigDimensions, BigDimensions);
            int j = 0;
            var leftInitialPosition = new Rectangle(LeftInitialX, LeftInitialY, SmallDimensions, SmallDimensions);
            var rightInitialPosition = new Rectangle(RightInitialX, RightInitialY, SmallDimensions, SmallDimensions);
            foreach (var characterTexture in _smallCharacters)
            {
                _leftCharecters[j] = new Character(characterTexture, leftInitialPosition, leftBig);
                _rightCharecters[j] = new Character(characterTexture, rightInitialPosition, rightBig);
                if (j > 3)
                {
                    _leftCharecters[j].Locked = true;
                    _rightCharecters[j].Locked = true;
                }
                leftInitialPosition.X += 42;
                rightInitialPosition.X += 42;
                ++j;
            }
            _sm = new SoundManager();
            _sm.LoadContent(content);
        }

        public void Update(MouseState mouse)
        {
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            foreach (var character in _leftCharecters.Where(character => mouseRectangle.Intersects(character.Rectangle) && mouse.LeftButton == ButtonState.Pressed))
            {
                foreach (var innerCharacter in _leftCharecters)
                    innerCharacter.Selected = false;
                character.Selected = true;
                _sm.roadChoice.Play();
            }

            foreach (var character in _rightCharecters.Where(character => mouseRectangle.Intersects(character.Rectangle) && mouse.LeftButton == ButtonState.Pressed))
            {
                foreach (var innerCharacter in _rightCharecters)
                    innerCharacter.Selected = false;
                character.Selected = true;
                _sm.roadChoice.Play();
            }

            _howManySelected = _leftCharecters.Concat(_rightCharecters).Count(character => character.Selected);

            if (2 == _howManySelected)
            {
                Character leftCharacter = null, rightCharacter = null;
                if (mouseRectangle.Intersects(_confirmRectangle) && mouse.LeftButton == ButtonState.Pressed)
                {
                    foreach (var character in _leftCharecters.Where(character => character.Selected))
                    {
                        leftCharacter = character;
                    }
                    foreach (var character in _rightCharecters.Where(character => character.Selected))
                    {
                        rightCharacter = character;
                    }
                    // These are the selected characters: leftCharacter, rightCharacter. Here we just play a sound.
                    if (leftCharacter != null && rightCharacter != null)
                    {
                        if (!(leftCharacter.Locked || rightCharacter.Locked))
                        {
                            _sm.blip.Play();
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_characterSelection, _backRectangle, Color.White);
            _drawSmallCharacters(spriteBatch, _leftCharecters.Concat(_rightCharecters));
            _drawSelectedCharacters(spriteBatch);
            spriteBatch.Draw(2 == _howManySelected ? _confirmButtonGreen : _confirmButtonGray, 
                _confirmRectangle, Color.White);
        }
    }
}
