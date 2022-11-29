using MazeSearch_Console;
MazeProcessor maze = new();
string? choice;
do
{
    Console.WriteLine("Welcome to Maze Search Console App");
    Console.WriteLine("- Enter 1 to Start Maze\n- Enter anything to exit");
    choice = Console.ReadLine();
    Console.Clear();
    if (choice == "1")
    {
        maze.Process();
    }
} while (choice == "1");

Console.WriteLine("Thank You for using Maze Search Console App!\n\nby: Shane Denney Cuizon\n");
Environment.Exit(0);