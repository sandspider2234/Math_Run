using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiddleCreation
{
    class MathRiddle_multiplication
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
                {
                    // Go to the next odd number.
                    factor += 2;
                }
            }

            // If num is not 1, then whatever is left is prime.
            if (num > 1) result.Add(num);

            return result;
        }

        private static readonly Random r = new Random();

        public long Answer {get; private set;}
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
            {
                First *= factors[i];
            }
            for (; i < factors.Count; ++i)
            {
                Second *= factors[i];
            }
        }

        public string RightAnswer()
        {
            return (First.ToString() + " * " + Second.ToString() + " = " + Answer.ToString());
        }
        public string WrongAnswer()
        {
            long wrong = Second;
            while (wrong * First == Answer)
            {
                wrong = r.Next(0, _range);
            }
            return (wrong.ToString() + " * " + Second.ToString() + " = " + Answer.ToString());
        }

        public int _range { get; private set; }
    }

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
            //System.Console.WriteLine("start.");
            //System.Console.WriteLine("Easy riddles (up to 10).");
            //MathRiddle e1 = new MathRiddle(10);
            //MathRiddle e2 = new MathRiddle(10);
            //MathRiddle e3 = new MathRiddle(10);

            //System.Console.WriteLine(e1.RightAnswer());
            //System.Console.WriteLine(e2.RightAnswer());
            //System.Console.WriteLine(e3.RightAnswer());

            //System.Console.WriteLine("Medium riddles (up to 100).");
            //MathRiddle m1 = new MathRiddle(100);
            //MathRiddle m2 = new MathRiddle(100);
            //MathRiddle m3 = new MathRiddle(100);

            //System.Console.WriteLine(m2.RightAnswer());
            //System.Console.WriteLine(m2.RightAnswer());
            //System.Console.WriteLine(m3.RightAnswer());
            
            //System.Console.WriteLine("Hard riddles (up to 1000).");
            //MathRiddle h1 = new MathRiddle(1000);
            //MathRiddle h2 = new MathRiddle(1000);
            //MathRiddle h3 = new MathRiddle(1000);

            //System.Console.WriteLine(h1.RightAnswer());
            //System.Console.WriteLine(h2.RightAnswer());
            //System.Console.WriteLine(h3.RightAnswer());

            //System.Console.WriteLine("Create a riddle, show the right one and then two wrong answers. (up to 50).");
            //MathRiddle test1 = new MathRiddle(50);
            //System.Console.WriteLine(test1.RightAnswer());
            //System.Console.WriteLine(test1.WrongAnswer());
            //System.Console.WriteLine(test1.WrongAnswer());

            System.Console.WriteLine("Create multiplication riddle, show the right one and then two wrong answers. (up to 500).");
            MathRiddle_multiplication testmul = new MathRiddle_multiplication(500);
            System.Console.WriteLine(testmul.RightAnswer());
            System.Console.WriteLine(testmul.WrongAnswer());
            System.Console.WriteLine(testmul.WrongAnswer());

            System.Console.WriteLine("done.");
        }
    }
}
