using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiddleCreation
{
    class MathRiddle
    {
        private static readonly Random r = new Random();

        public int Answer {get; private set;}
        public int First {get; private set;}
        public int Second {get; private set;}

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
            {
                Second = r.Next(0, range);
            }
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
            {
                wrong = r.Next(0, _range);
            }
            return (wrong.ToString() + " + " + Second.ToString() + " = " + Answer.ToString());
        }

        public int _range { get; private set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("start.");
            System.Console.WriteLine("Easy riddles (up to 10).");
            MathRiddle e1 = new MathRiddle(10);
            MathRiddle e2 = new MathRiddle(10);
            MathRiddle e3 = new MathRiddle(10);

            System.Console.WriteLine(e1.RightAnswer());
            System.Console.WriteLine(e2.RightAnswer());
            System.Console.WriteLine(e3.RightAnswer());

            System.Console.WriteLine("Medium riddles (up to 100).");
            MathRiddle m1 = new MathRiddle(100);
            MathRiddle m2 = new MathRiddle(100);
            MathRiddle m3 = new MathRiddle(100);

            System.Console.WriteLine(m2.RightAnswer());
            System.Console.WriteLine(m2.RightAnswer());
            System.Console.WriteLine(m3.RightAnswer());
            
            System.Console.WriteLine("Hard riddles (up to 1000).");
            MathRiddle h1 = new MathRiddle(1000);
            MathRiddle h2 = new MathRiddle(1000);
            MathRiddle h3 = new MathRiddle(1000);

            System.Console.WriteLine(h1.RightAnswer());
            System.Console.WriteLine(h2.RightAnswer());
            System.Console.WriteLine(h3.RightAnswer());

            System.Console.WriteLine("Create a riddle, show the right one and then two wrong answers. (up to 50).");
            MathRiddle test1 = new MathRiddle(50);
            System.Console.WriteLine(test1.RightAnswer());
            System.Console.WriteLine(test1.WrongAnswer());
            System.Console.WriteLine(test1.WrongAnswer());

            System.Console.WriteLine("done.");
        }
    }
}
