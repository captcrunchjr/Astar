// See https://aka.ms/new-console-template for more information
using Astar;

bool repeat = false;
do
{
    repeat = false;
    Console.WriteLine("A* Implementation");
    
    //retrieve user defined side length of the square board
    int dimension = UserPrompts.GetDimension();

    //generate the initial board with 10% of spaces randomly selected as obstacles
    GridBuilder Grid = new GridBuilder(dimension);
    //display the board with cell numbers displayed for user selection of start and end points
    Grid.IntialDrawDisplay();

    //retrieve user defined start and end
    int userStart = UserPrompts.GetStartChoice(Grid.CellCount, Grid.ObstaclesList);
    int userEnd = UserPrompts.GetEndChoice(Grid.CellCount, userStart, Grid.ObstaclesList);

    //re-render the board without cell numbers and with start and end points
    Grid.DrawDisplay(userStart, userEnd);

    //Construct the pathfinder and display the initial hCost to destination
    PathFinder pathFinder = new PathFinder(userStart, userEnd, dimension);

    //Find path and draw the final board ... path denoted with S = start, x = obstacles, E = end, > = node on the path
    List <Node> pathList = pathFinder.FindPath(Grid.Cells);
    Grid.DrawFinalDisplay(userStart, userEnd, pathList);

    repeat = UserPrompts.RepeatPrompt();

} while (repeat);
Console.WriteLine("Press any key to close");
Console.ReadLine();