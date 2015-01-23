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
    internal class Character
    {
        public Rectangle Rectangle { get; private set; }
        public Rectangle BigRectangle { get; private set; }
        public Texture2D Texture2D { get; private set; }

        public Character(Texture2D texture, Rectangle rectangle = new Rectangle(), Rectangle bigRectangle = new Rectangle())
        {
            Texture2D = texture;
            Rectangle = rectangle;
            BigRectangle = bigRectangle;
            Selected = false;
        }

        public bool Selected { get; set; }
    }

    public class CharacterSelection
    {
        private Texture2D _confirmButtonGray;
        private Texture2D _confirmButtonGreen;
        private Texture2D _characterSelection;
        private Texture2D _char;
        private Rectangle _chaRectangle = new Rectangle(400,400,32,32);
        private Rectangle _confirmRectangle = new Rectangle(100,600,600, 192);
        private Rectangle _backRectangle = new Rectangle(0,0,800,800);
        private Texture2D[] _smallCharacters = new Texture2D[8];
        private Character[] _leftCharecters = new Character[8];
        private Character[] _rightCharecters = new Character[8];
        private int _howManySelected;
        private SoundManager _sm;

        private void _drawSelectedCharacters(SpriteBatch spriteBatch)
        {
            foreach (var character in _leftCharecters.Concat(_rightCharecters).Where(character => character.Selected))
            {
                spriteBatch.Draw(character.Texture2D, character.BigRectangle, Color.White);
            }
        }

        private void _drawSmallCharacters(SpriteBatch spriteBatch, IEnumerable<Character> characters)
        {
            foreach (var character in characters)
            {
                spriteBatch.Draw(character.Texture2D, character.Rectangle, Color.White);
            }
        }


        public CharacterSelection()
        {
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

            var leftBig = new Rectangle(30, 240, 300, 300);
            var rightBig = new Rectangle(430, 240, 300, 300);
            int j = 0;
            var leftInitialPosition = new Rectangle(20, 550, 32, 32);
            var rightInitialPosition = new Rectangle(420, 550, 32, 32);
            foreach (var characterTexture in _smallCharacters)
            {
                _leftCharecters[j] = new Character(characterTexture, leftInitialPosition, leftBig);
                _rightCharecters[j] = new Character(characterTexture, rightInitialPosition, rightBig);
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
            }

            foreach (var character in _rightCharecters.Where(character => mouseRectangle.Intersects(character.Rectangle) && mouse.LeftButton == ButtonState.Pressed))
            {
                foreach (var innerCharacter in _rightCharecters)
                    innerCharacter.Selected = false;
                character.Selected = true;
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
                        _sm.blip.Play();
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_characterSelection, _backRectangle, Color.White);
            _drawSmallCharacters(spriteBatch, _leftCharecters);
            _drawSmallCharacters(spriteBatch, _rightCharecters);
            _drawSelectedCharacters(spriteBatch);
            spriteBatch.Draw(2 == _howManySelected ? _confirmButtonGreen : _confirmButtonGray, 
                _confirmRectangle, Color.White);
        }
    }
}
