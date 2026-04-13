using Spectre.Console;
using System.Text;
using static TCSA.OOP.MathGame.Enums;

namespace TCSA.OOP.MathGame;

internal class UserInterface
{
   
    private GameController game = new();
    // select operation

    public void MainMenu()
    {
        while (true)
        {

            var mainOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title($"Please select [green] your option[/]. Difficulty: [blue][bold]{game.DifficultyLevel}[/][/]")
                .AddChoices(["New Game", "Change difficulty", "Show history", "Exit"])
                );
            switch (mainOption)
            {
                case "New Game":
                    OperationMenu();
                    break;
                case "Change difficulty":
                    ChangeDifficulty();
                    break;
                case "Show history":
                    ShowHistory();
                    break;
                case "Exit":
                    return;
            }
            AnsiConsole.Clear();
        }
    }

    private void ChangeDifficulty()
    {
        var dificultyLevel = AnsiConsole.Prompt(
            new SelectionPrompt<Difficulty>()
            .Title("Please select [green] a difficulty[/]")
            .AddChoices(Enum.GetValues<Difficulty>())
            );
        game.DifficultyLevel = dificultyLevel;
    }

    private void ShowHistory()
    {
        int round = 0;
        AnsiConsole.Clear();
        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.Grey)
            .Title("[yellow bold]History[/]");

        table.AddColumn("[yellow]Round Number[/]");
        table.AddColumn("[yellow]Operation Type[/]");
        table.AddColumn("[yellow]First Number[/]");
        table.AddColumn("[yellow]Operation[/]");
        table.AddColumn("[yellow]Second Number[/]");
        table.AddColumn("[yellow]Result[/]");
        table.AddColumn("[yellow]Answer[/]");
        table.AddColumn("[yellow]Info[/]");



        foreach (var item in game.AllRoundSets)
        {
            round++;
            foreach (var roundset in item.RoundSetHistory)
            {
                table.AddRow(
                    $"[blue]{round}[/][DarkOrange]({item.DifficultyLevel})[/]", 
                    $"[green]{item.Operation}[/]", 
                    $"[green]{roundset.FirstNumber}[/]", 
                    $"[cyan]{roundset.OperationAsString}[/]",
                    $"[green]{roundset.SecondNumber}[/]", 
                    $"[DarkOrange]{roundset.Result}[/]", 
                    $"[blue]{roundset.UserInput}[/]",
                    ((roundset.Result == roundset.UserInput) ? "[green]✓[/]" : "[bold red]X[/]")
                    );
            }
        }

        table.Columns[0].Footer = new Text("");
        table.Columns[1].Footer = new Text("Total points", new Style(decoration: Decoration.Bold));
        table.Columns[2].Footer = new Text($"{game.Score}", new Style(Color.Blue, decoration: Decoration.Bold));

        AnsiConsole.Write(table);
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Press enter key to continue:")
            .AddChoices("Enter"));
    }

    public void OperationMenu()
    {
        RoundSet roundSet = new RoundSet();
        roundSet.DifficultyLevel = game.DifficultyLevel;

        var operation = AnsiConsole.Prompt(
            new SelectionPrompt<Operations>()
            .Title("Please select [green] an operation[/]")
            .AddChoices(Enum.GetValues<Operations>())
            );
        
        roundSet.Operation = operation;
        for (int i = 0; i < roundSet.NumberOfQuestions; i++)
        {
            Round round = new(operation, game.DifficultyLevel);
            ShowQuestion(round);
            roundSet.RoundSetHistory.Add(round);
        }
        Console.WriteLine($"You score {game.Score} of {roundSet.NumberOfQuestions}");
        foreach (var item in roundSet.RoundSetHistory)
        {
            AnsiConsole.MarkupLine($"[green]{item.FirstNumber}[/] [cyan]{item.OperationAsString}[/] [green]{item.SecondNumber}[/] = " +
                $"Correct response: [cyan]{item.Result}[/]; Your response: [green]{item.UserInput}[/]" +
                ((item.Result == item.UserInput) ? "\t[blue][bold]CORRECT[/][/]" : "\t[red][bold]WRONG![/][/]"));
        }
        game.AllRoundSets.Add(roundSet);

        var choice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("Press enter key to continue:")
        .AddChoices("Enter"));

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
            game.Score++;
        }
        else
        {
            AnsiConsole.MarkupLine("[green]Your answer is [/][red][bold]WRONG[/][/]!");
        }
        
    }


}
