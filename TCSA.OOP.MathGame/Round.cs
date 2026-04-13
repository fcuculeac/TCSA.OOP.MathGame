using static FlorinCuculeac.MathGame.Enums;

namespace FlorinCuculeac.MathGame;

internal class Round
{
    public int FirstNumber { get; set; }
    public int SecondNumber { get; set; }
    public Operations Operation { get; set; }
    public string OperationAsString { get; set; } = string.Empty;
    public int Result { get; set; }
    public int UserInput { get; set; }

    public Round(Operations operation, Difficulty difficultyLevel)
    {
        Operation = operation;
        int MaxNumber = 0;
        switch (difficultyLevel)
        {
            case Difficulty.Easy:
                MaxNumber = 10;
                break;
            case Difficulty.Medium:
                MaxNumber = 50;
                break;
            case Difficulty.Hard:
                MaxNumber = 100;
                break;
        }

        FirstNumber = Random.Shared.Next(MaxNumber);
        SecondNumber = Random.Shared.Next(MaxNumber);
        
        
        switch (Operation)
        {
            case Operations.Addition:
                Result = FirstNumber + SecondNumber; OperationAsString = "+"; break;
            case Operations.Substraction:
                // no negative result for easy difficulty
                if (difficultyLevel == Difficulty.Easy)
                {
                    if (SecondNumber > FirstNumber)
                    {
                        (FirstNumber, SecondNumber) = (SecondNumber, FirstNumber);
                    }
                }
                Result = FirstNumber - SecondNumber; OperationAsString = "-"; break;
            case Operations.Multilpy:
                Result = FirstNumber * SecondNumber; OperationAsString = "*"; break;
            case Operations.Division:
                FirstNumber = SecondNumber * FirstNumber;
                Result = FirstNumber / SecondNumber;
                OperationAsString = "/";
                break;
        }
        
    }

    

    public bool CheckResult()
    {
        return Result == UserInput;
    }

}
