enum Tick
{
    Empty,
    X,
    O
}

class Program
{
    const char EMPTY = '-';
    static bool win = false;

    public static void Main()
    {
        Tick[,] grid = new Tick[3, 3];
        FillGrid(grid, Tick.Empty);
        PrintGrid(grid);

        bool isFirstPlayer = true;

        Tick firstPlayer = Tick.X;
        Tick secondPlayer = Tick.O;

        while (!win)
        {
            SetTick(grid, isFirstPlayer ? firstPlayer : secondPlayer);
            PrintGrid(grid);

            if (isFirstPlayer && win)
            {
                Console.WriteLine("X win");
            }
            else if (!isFirstPlayer && win)
            {
                Console.WriteLine("0 win");
            }

            isFirstPlayer = !isFirstPlayer;
        }



    }

    static void PrintGrid(Tick[,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Console.Write("{0}\t", grid[i, j] == Tick.Empty ? EMPTY : grid[i, j]);
            }

            Console.WriteLine();
        }
    }

    static void FillGrid(Tick[,] grid, Tick tick)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = tick;
            }
        }
    }

    static Tick GetTick()
    {
        string input;
        do
        {
            Console.Write("Please input your tick: ");
            input = Console.ReadLine();
        } while (input.Length > 1 || input.ToUpper() != Tick.X.ToString() || input.ToUpper() != Tick.O.ToString());

        return input.ToUpper() == Tick.X.ToString() ? Tick.X : Tick.O;
    }

    static void SetTick(Tick[,] grid, Tick tick)
    {
        string[] inputCoords;
        int i;
        int j;
        do
        {
            Console.Write("Input Coord (i,j): ");
            string input = Console.ReadLine();
            inputCoords = input.Split(',');
        } while (!int.TryParse(inputCoords[0], out i) || !int.TryParse(inputCoords[1], out j) ||
                 grid[i, j] != Tick.Empty);

        grid[i, j] = tick;

        if (CheckHorizontal(grid, tick, i, j) || CheckVertical(grid, tick, i, j) || CheckRightDiagonal(grid, tick, j) || CheckLeftDiagonal(grid, tick, j))
        {

            win = true;
        }
    }

    static bool CheckHorizontal(Tick[,] grid, Tick tick, int i, int j)
    {
        for (int k = 0; k < grid.GetLength(1); k++)
        {
            if (k == j)
            {
                continue;
            }

            if (grid[i, k] != tick)
            {
                return false;
            }
        }

        return true;
    }

    static bool CheckVertical(Tick[,] grid, Tick tick, int i, int j)
    {
        for (int k = 0; k < grid.GetLength(0); k++)
        {
            if (k == i)
            {
                continue;
            }

            if (grid[k, j] != tick)
            {
                return false;
            }
        }

        return true;
    }

    static bool CheckRightDiagonal(Tick[,] grid, Tick tick, int j)
    {
        for (int k = 0; k < grid.GetLength(0); k++)
        {
            if (k == j)
            {
                continue;
            }

            if (grid[k, k] != tick)
            {
                return false;
            }
        }
        return true;
    }


    static bool CheckLeftDiagonal(Tick[,] grid, Tick tick, int j)
    {
        int a = grid.GetLength(1) - 1;
        for (int k = 0; k < grid.GetLength(0); k++)
        {
            if (a == j)
            {
                a--;
                continue;
            }

            if (grid[k, a] != tick)
            {
                return false;
            }
            a--;
        }
        return true;
    }
}

