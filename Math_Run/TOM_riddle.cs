using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
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
    internal class RiddleOption
    {
        public SpriteFont Font { get; set; }
        public Vector2 Position { get; set; }
        public string _String { get; set; }
        public Color Color { get; set; }

        public RiddleOption(SpriteFont font, string String, Vector2 position = new Vector2())
        {
            Font = font;
            _String = String;
            Position = position;
            Color = Color.Red;
        }
    }
   public class MathRiddle_multiplication
    {
        private List<long> FindFactors(long num)
        {
            List<long> result = new List<long>();

            // Take out the 2s.
            while (num % 2 == 0)
            {
                result.Add(2);
                num /= 2;
            }

            // Take out other primes.
            long factor = 3;
            while (factor * factor <= num)
            {
                if (num % factor == 0)
                {
                    // This is a factor.
                    result.Add(factor);
                    num /= factor;
                }
                else
                    // Go to the next odd number.
                    factor += 2;
            }

            // If num is not 1, then whatever is left is prime.
            if (num > 1) result.Add(num);

            return result;
        }

        private static readonly Random r = new Random();

        public long Answer { get; private set; }
        public long First { get; private set; }
        public long Second { get; private set; }

        public MathRiddle_multiplication(int range)
        {
            Answer = r.Next(range);
            _range = range;

            // Get the number's factors.
            long num = Answer;
            List<long> factors = FindFactors(num);

            // get them into 2 arguments
            First = 1;
            Second = 1;
            int i = 0;
            for (; i <= factors.Count / 2; ++i)
                First *= factors[i];
            for (; i < factors.Count; ++i)
                Second *= factors[i];
        }

        public string RightAnswer()
        {
            return (First.ToString() + " * " + Second.ToString() + " = " + Answer.ToString());
        }
        public string WrongAnswer()
        {
            long wrong = Second;
            while (wrong * First == Answer)
                wrong = r.Next(0, _range);
            return (wrong.ToString() + " * " + Second.ToString() + " = " + Answer.ToString());
        }

        public int _range { get; private set; }
    }
    public class MathRiddle
    {
        private static readonly Random r = new Random();

        public int Answer { get; private set; }
        public int First { get; private set; }
        public int Second { get; private set; }

        public MathRiddle(int answer, int first, int second)
        {
            Answer = answer;
            First = first;
            Second = second;
        }

        public MathRiddle(int range)
        {
            Answer = r.Next(range / 2, range);
            First = r.Next(0, range / 2);
            Second = 0;
            while (First + Second != Answer)
                Second = r.Next(0, range);
            _range = range;
        }

        public string RightAnswer()
        {
            return (First.ToString() + " + " + Second.ToString() + " = " + Answer.ToString());
        }
        public string WrongAnswer()
        {
            int wrong = Second;
            while (wrong + First == Answer)
                wrong = r.Next(0, _range);
            return (wrong.ToString() + " + " + Second.ToString() + " = " + Answer.ToString());
        }

        public int _range { get; private set; }
    }
    public class Riddle
    {
        private SoundManager _sm;
        private RiddleOption _topOption;
        private RiddleOption _leftOption;
        private RiddleOption _rightOption;
        private int _initialX, _initialY, _range;
        public static Random _r = new Random();
        public int rand = _r.Next(0, 3);
        private Keys _left, _top, _right;

        private volatile bool _shouldTerminate;

        public Riddle(int initialX, int initialY, int range, Keys leftChoice, Keys topChoice, Keys rightChoice)
        {
            _initialX = initialX;
            _initialY = initialY;
            _left = leftChoice;
            _top = topChoice;
            _right = rightChoice;
            _range = range;
        }


        public void LoadContent(ContentManager content)
        {
            MathRiddle mathRiddle = new MathRiddle(_range);
            if (rand == 0)
            {
                _topOption = new RiddleOption(content.Load<SpriteFont>("Font1"), mathRiddle.RightAnswer(), new Vector2(_initialX, _initialY));
                _leftOption = new RiddleOption(content.Load<SpriteFont>("Font1"), mathRiddle.WrongAnswer(), new Vector2(_initialX - 96, _initialY + 96));
                _rightOption = new RiddleOption(content.Load<SpriteFont>("Font1"), mathRiddle.WrongAnswer(), new Vector2(_initialX + 96, _initialY + 96));
            }
            else if (rand == 1)
            {
                _topOption = new RiddleOption(content.Load<SpriteFont>("Font1"), mathRiddle.WrongAnswer(), new Vector2(_initialX, _initialY));
                _leftOption = new RiddleOption(content.Load<SpriteFont>("Font1"), mathRiddle.RightAnswer(), new Vector2(_initialX - 96, _initialY + 96));
                _rightOption = new RiddleOption(content.Load<SpriteFont>("Font1"), mathRiddle.WrongAnswer(), new Vector2(_initialX + 96, _initialY + 96));
            }
            else
            {
                _topOption = new RiddleOption(content.Load<SpriteFont>("Font1"), mathRiddle.WrongAnswer(), new Vector2(_initialX, _initialY));
                _leftOption = new RiddleOption(content.Load<SpriteFont>("Font1"), mathRiddle.WrongAnswer(), new Vector2(_initialX - 96, _initialY + 96));
                _rightOption = new RiddleOption(content.Load<SpriteFont>("Font1"), mathRiddle.RightAnswer(), new Vector2(_initialX + 96, _initialY + 96));
            }

            _sm = new SoundManager();
            _sm.LoadContent(content);
        }

        public void Update(KeyboardState keyboard)
        {
            float movementRate = 1;
            _topOption.Position = new Vector2(_topOption.Position.X, _topOption.Position.Y - movementRate);
            _leftOption.Position = new Vector2(_leftOption.Position.X - movementRate, _leftOption.Position.Y);
            _rightOption.Position = new Vector2(_rightOption.Position.X + movementRate, _rightOption.Position.Y);

            int colorChangeRate = 2;
            _topOption.Color = new Color(_topOption.Color.R - colorChangeRate, _topOption.Color.G - colorChangeRate, _topOption.Color.B - colorChangeRate);
            _leftOption.Color = new Color(_leftOption.Color.R - colorChangeRate, _leftOption.Color.G - colorChangeRate, _leftOption.Color.B - colorChangeRate);
            _rightOption.Color = new Color(_rightOption.Color.R - colorChangeRate, _rightOption.Color.G - colorChangeRate, _rightOption.Color.B - colorChangeRate);

            if (keyboard.IsKeyDown(_top) && rand == 0)
                _sm.blip.Play();
            else if (keyboard.IsKeyDown(_left) && rand == 1)
                _sm.blip.Play();
            else if (keyboard.IsKeyDown(_right) && rand == 2)
                _sm.blip.Play();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_topOption.Font, _topOption._String, _topOption.Position, _topOption.Color);
            spriteBatch.DrawString(_leftOption.Font, _leftOption._String, _leftOption.Position, _leftOption.Color);
            spriteBatch.DrawString(_rightOption.Font, _rightOption._String, _rightOption.Position, _rightOption.Color);
        }
    }
}