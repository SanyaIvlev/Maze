using MazeClass;

static class Program
{
    static void Main(string[] args)
    {
        Maze maze = new Maze();
        maze.GenerateMap();
        maze.DrawMap();

        while (!maze.IsGameEnded())
        {
            (maze.dx, maze.dy) = maze.GetInput();
            maze.Logic();
            maze.DrawMap();
        }
        
        Console.WriteLine("You have finished!");
    } 
}