using System.Collections;

namespace Bowling.Model
{
    public interface ISimpleBowlingGame
    {
        // Called when a player completes a frame.
        // This method will be called 10 times for a bowling game.
        // The throws parameter provides the number of pins knocked down on each throw in the frame being recorded.
        // The 10th frame may have 3 values.
        void RecordFrame(params int[] throws);

        // Called at the end of the game to get the final score.
        int Score { get; }
    }
}