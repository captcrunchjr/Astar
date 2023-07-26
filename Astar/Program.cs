// See https://aka.ms/new-console-template for more information
using Astar;

bool repeat = false;
do
{
    repeat = false;
    Console.WriteLine("A* Implementation");
    
    int dimension = UserPrompts.GetDimension();

    GridBuilder Grid = new GridBuilder(dimension);
    for(int i = 0; i < Grid.ObstaclesList.Count; i++)
    {
        nodes.Add(Grid.ObstaclesList.ElementAt(i));
    }
    Grid.IntialDrawDisplay();

    int userStart = UserPrompts.GetStartChoice(Grid.CellCount, Grid.ObstaclesList);
    int userEnd = UserPrompts.GetEndChoice(Grid.CellCount, userStart, Grid.ObstaclesList);

    //re-render the board without cell numbers and with start and end points
    Grid.DrawDisplay(userStart, userEnd);

    PathFinder pathFinder = new PathFinder(userStart, userEnd, dimension);
    Console.WriteLine("The hCost is: " + pathFinder.GetCost());

    pathFinder.FindPath(Grid, Grid.Cells);
    Grid.DrawFinalDisplay(userStart, userEnd, pathFinder.GetPathList(), pathFinder.GetSimPosition());

    repeat = UserPrompts.RepeatPrompt();

} while (repeat);
Console.WriteLine("Press any key to close");
Console.ReadLine();