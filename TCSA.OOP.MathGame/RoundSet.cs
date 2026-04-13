using static FlorinCuculeac.MathGame.Enums;

namespace FlorinCuculeac.MathGame;

internal class RoundSet
{
    public List<Round> RoundSetHistory { get; set; } = [];
    public Operations Operation { get; set; }
    public int NumberOfQuestions { get; } = 5;
    public Difficulty DifficultyLevel { get; set; }

    public TimeSpan TimeElapsed { get; set; }
}
