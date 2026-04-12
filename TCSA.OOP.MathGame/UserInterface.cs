using Spectre.Console;
using System.Text;
using static TCSA.OOP.MathGame.Enums;

namespace TCSA.OOP.MathGame;

internal class UserInterface
{
    public int Score = 0;
    private readonly int numberOfQuestion = 5;
    private GameController game = new();
    // select operation
    public void MainMenu()
    {


        //switch (operation)
        //{
        //    case Operations.Addition:
        //        Console.WriteLine("You select addition");
        //        MakeAddition();
        //        break;
        //    case Operations.Multilpy:
        //        Console.WriteLine("You select multiply");
        //        break;
        //    case Operations.Substraction:
        //        Console.WriteLine("You select substraction");
        //        break;
        //    case Operations.Division:
        //        Console.WriteLine("You select division");
        //        break;
        //}

        var operation = AnsiConsole.Prompt(
            new SelectionPrompt<Operations>()
            .Title("Please select [green] an operation[/]")
            .AddChoices(Enum.GetValues<Operations>())
            );

        for (int i = 0; i < numberOfQuestion; i++)
        {
            Round round = new(operation);
            ShowQuestion(round);
            game.CurrentRound.Add(round);
        }
        Console.WriteLine($"You score {Score} of {numberOfQuestion}");
        foreach (var item in game.CurrentRound)
        {
            AnsiConsole.MarkupLine($"[green]{item.FirstNumber}[/] [cyan]{item.OperationAsString}[/] [green]{item.SecondNumber}[/] = " +
                $"Correct response: [cyan]{item.Result}[/]; Your response: [green]{item.UserInput}[/]" +
                ((item.Result == item.UserInput) ? "\t[blue][bold]CORRECT[/][/]" : "\t[red][bold]WRONG![/][/]"));
        }
    }

    private void ShowQuestion(Round round)
    {
        
        AnsiConsole.MarkupLine($"[green]{round.FirstNumber}[/] [cyan]{round.OperationAsString}[/] [green]{round.SecondNumber}[/]"
            + " = ?. [green]Please insert your answer.[/]");
        var userResult = AnsiConsole.Ask<int>("[cyan]Result:[/]");
        round.UserInput = userResult;
        if (round.UserInput == round.Result)
        {
            AnsiConsole.MarkupLine("[green]Your answer is [blue][bold]CORRECT[/][/]![/]");
            Score++;
        }
        else
        {
            AnsiConsole.MarkupLine("[green]Your answer is [/][red][bold]WRONG[/][/]!");
        }
    }


}
