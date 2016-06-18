using System;
using System.Text;

namespace Bowling.Model
{
    public class SimpleBowlingGame : ISimpleBowlingGame
    {
        private Frame _firstFrame;
        private Frame _lastFrame; 

        public void RecordFrame(params int[] throws)
        {
            var first = throws[0];
            var second = throws.Length > 1 ? throws[1] : (int?) null;
            var third = throws.Length > 2 ? throws[2] : (int?) null;
            
            var thisFrame = new Frame(first, second, third);

            if (_lastFrame == null)
                _lastFrame = _firstFrame = thisFrame;
            else
            {
                _lastFrame.AddNext(thisFrame);
                _lastFrame = thisFrame;
            }

            Console.WriteLine(thisFrame);
        }

        public int Score
        {
            get
            {
                var output = 0;

                var tmpFrame = _firstFrame;

                while (tmpFrame != null)
                {
                    output += tmpFrame.Score;
                    tmpFrame = tmpFrame.NextFrame;
                }

                return output;
            }
        }
    }

    internal class Frame
    {
        public Frame(int first, int? second, int? third)
        {
            First = first;
            Second = second;
            Third = third;
        }

        public int Score
        {
            get
            {
                int output;

                if (Second != null && Third != null)
                {
                    output = First + Second.Value + Third.Value;
                }
                else if (Second != null)
                {
                    if (First + Second.Value < 10)
                        output = First + Second.Value;
                    else
                    {
                        // Spare
                        output = 10 + (NextFrame?.First ?? 0);
                    }
                }
                else
                {
                    // Strike
                    var nextThrow = 0;
                    var secondNextThrow = 0;

                    if (NextFrame != null)
                    {
                        nextThrow = NextFrame.First;

                        if (NextFrame.Second != null)
                            secondNextThrow = NextFrame.Second.Value;
                        else if (NextFrame.NextFrame != null)
                        {
                            secondNextThrow = NextFrame.NextFrame.First;
                        }
                    }

                    output = 10 + nextThrow + secondNextThrow;
                }

                return output;
            }
        }

        public int First { get; }

        public int? Second { get; }

        public int? Third { get; }

        public Frame NextFrame { get; set; }

        public Frame PreviousFrame { get; set; }

        public void AddNext(Frame frame)
        {
            NextFrame = frame;
            NextFrame.PreviousFrame = this;
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            output.Append($"Frame [First: {First}");

            if (Second != null)
                output.Append($", Second: {Second}");

            if (Third != null)
                output.Append($", {Third}");

            output.Append($"] -- {Score}");

            return output.ToString();
        }
    }
}