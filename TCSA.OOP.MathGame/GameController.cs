using static TCSA.OOP.MathGame.Enums;

namespace TCSA.OOP.MathGame;

internal class GameController
{

    public List<RoundSet> AllRoundSets { get; set; } = [];
    public int Score { get; set; } = 0;
    public Difficulty DifficultyLevel { get; set; } = Difficulty.Easy;


}
