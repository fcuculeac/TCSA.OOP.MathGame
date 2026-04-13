using static TCSA.OOP.MathGame.Enums;

namespace TCSA.OOP.MathGame;

internal class Round
{
    public int FirstNumber { get; set; }
    public int SecondNumber { get; set; }
    public Operations Operation { get; set; }
    public string OperationAsString { get; set; } = string.Empty;
    public int Result { get; set; }
    public int UserInput { get; set; }

    public Round(Operations operation)
    {
        Operation = operation;
        FirstNumber = Random.Shared.Next(10);
        SecondNumber = Random.Shared.Next(10);
        
        
        switch (Operation)
        {
            case Operations.Addition:
                Result = FirstNumber + SecondNumber; OperationAsString = "+"; break;
            case Operations.Substraction:
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
