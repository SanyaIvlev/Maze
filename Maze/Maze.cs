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
    private int jetpackX, jetpackY;
    private char jetpack = 'J';

    private bool hasJetpack = false;

    private bool isReachedFinish = false;

    private int oxygenLeft = 30;
    
    private Random random = new Random();


    public void RunGame()
    {
        GenerateField();
        DrawMap();

        while (!IsGameEnded())
        {
            GetInput();
            Logic();
            DrawMap();
        }
        
        Console.WriteLine("Game ended!");
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
        Console.WriteLine("\nOxygen left: " + oxygenLeft);
    }
    
    public void GetInput()
    {
        
        string input = Console.ReadLine();
        
        if (input.Length == 0)
        {
            return;
        }

        char firstSymbol = input[0];

        (dx, dy) = firstSymbol switch
        {
            'W' or 'w' => (0, -1),
            'A' or 'a' => (-1, 0),
            'S' or 's' => (0, 1),
            'D' or 'd' => (1, 0)
        };
        

    }

    public bool IsGameEnded()
    {
        return isReachedFinish || oxygenLeft == 0;
    }

    private void CheckFinish()
    {
        if (dogX == finishX && dogY == finishY)
        {
            isReachedFinish = true;
        }
    }

    private void CheckJetpack()
    {
        if (dogX == jetpackX && dogY == jetpackY)
        {
            hasJetpack = true;
            field[jetpackY, jetpackX] = '.';
        }
    }
    
    private bool IsWalkable(int x, int y)
    {
        return field[y, x] != '#';
    }
    
    private bool TryGoTo(int x, int y)
    {
        if (CanGoTo(x, y))
        {
            GoTo(x, y);
            oxygenLeft--;
            return true;
        }

        return false;
    }
    
    private bool CanGoTo(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            return false;
        }
        else if (!IsWalkable(x, y))
        {
            if (hasJetpack)
            {
                TryJumpOver(x, y);
            }
            return false;
        }
        return true;
    }

    private void TryJumpOver(int x, int y)
    {
        bool hasMoved = TryGoTo(x + dx, y + dy);
        hasJetpack = !hasMoved;
    }
    
    private void GoTo(int x, int y)
    {
        dogX = x;
        dogY = y;
    }
    
    public void Logic()
    {
        CheckJetpack();
        
        TryGoTo(dogX + dx, dogY + dy);
        
        CheckFinish();
    }
    
    private void PlaceDog()
    {
        (dogX, dogY) = GetRandomPosition();
    }

    private void PlaceJetpack()
    {
        (jetpackX, jetpackY) = GetRandomPosition();
        field[jetpackY, jetpackX] = jetpack;
    }

    private void PlaceFinish()
    {
        (finishX, finishY) = GetRandomPosition();
        field[finishY, finishX] = 'F';
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
        
        PlaceJetpack();
        PlaceFinish();
    }

    private (int x, int y) GetRandomPosition()
    {
        int x = random.Next(0, width);
        int y = random.Next(0, height);
        
        return (x, y);
    }

    
    
    
    
}