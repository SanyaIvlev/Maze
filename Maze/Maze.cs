namespace MazeClass;

public class Maze
{
    static private int width = 10;
    static private int height = 10;
    private char[,] field = new char[height,width];

    private int blockFrequency = 28;

    private char dog = '@';
    private int dogX = 0;
    private int dogY = 0;
    
    private Random random = new Random();


    public void GenerateMap()
    {
        GenerateField();
        PlaceDog();
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
    }

    public void DrawMap()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                char symbol = field[i, j];
                
                if (i == dogX && j == dogY)
                {
                    symbol = dog;
                }
                Console.Write(symbol);
            }
            Console.WriteLine();
        }
    }
}