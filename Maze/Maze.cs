namespace MazeClass;

public class Maze
{
    public int dx = 0, dy = 0;
    
    private static int width = 10;
    private static int height = 10;
    private char[,] field = new char[height,width];

    private int blockFrequency = 28;

    private char dog = '@';
    private int dogX, dogY;

    private int finishX, finishY;

    private bool isReachedFinish = false;
    
    private Random random = new Random();


    public void RunGame()
    {
        GenerateField();
        DrawMap();

        while (!IsGameEnded())
        {
            (dx,dy) = GetInput();
            Logic();
            DrawMap();
        }
        
        Console.WriteLine("You have finished!");
    }
    
    public void GenerateMap()
    {
        GenerateField();
        PlaceDog();
    }
    
    public void DrawMap()
    {
        Console.Clear();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                char symbol = field[i, j];
                
                if (i == dogY && j == dogX)
                {
                    symbol = dog;
                }
                Console.Write(symbol);
            }
            Console.WriteLine();
        }
    }
    
    public (int,int) GetInput()
    {
        //(dx, dy) = (0, 0);
        int dx = 0, dy = 0;
        
        string input = Console.ReadLine();
        
        if (input.Length == 0)
        {
            return (0,0);
        }

        char firstSymbol = input[0];

        if (firstSymbol == 'W' || firstSymbol == 'w')
        {
            dy = -1;
        }
        else if (firstSymbol == 'A' || firstSymbol == 'a')
        {
            dx = -1;
        }
        else if (firstSymbol == 'S' || firstSymbol == 's')
        {
            dy = 1;
        }
        else if (firstSymbol == 'D' || firstSymbol == 'd')
        {
            dx = 1;
        }

        return (dx, dy);

    }

    public bool IsGameEnded()
    {
        return isReachedFinish;
    }

    private void TryGoTo(int x, int y)
    {
        if (CanGoTo(x, y))
        {
            GoTo(x, y);
        }
    }

    private void CheckFinish()
    {
        if (dogX == finishX && dogY == finishY)
        {
            isReachedFinish = true;
        }
    }
    
    private bool IsWalkable(int x, int y)
    {
        return field[y, x] != '#';
    }
    
    private bool CanGoTo(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            return false;
        }
        else if (!IsWalkable(x, y))
        {
            return false;
        }
        return true;
    }

    private void GoTo(int x, int y)
    {
        dogX = x;
        dogY = y;
    }
    
    public void Logic()
    {
        TryGoTo(dogX + dx, dogY + dy);
        
        CheckFinish();
    }
    
    private void PlaceDog()
    {
        dogX = random.Next(0, width);
        dogY = random.Next(0, height);
    }

    private void GenerateField()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int randomNumber = random.Next(0, 101);

                char symbol = '.';
                if (randomNumber < blockFrequency)
                {
                    symbol = '#';
                }
                field[i, j] = symbol;
            }
        }

        finishX = random.Next(0, width);
        finishY = random.Next(0, height);

        field[finishY,finishX] = 'F';
    }

    
    
    
    
}