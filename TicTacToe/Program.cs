namespace TicTacToe
{
    enum Tick
    {
        Empty,
        X,
        O
    }

    class Program
    {
        public const char EMPTY = '-';
        public const int MATRIX_SIZE = 3;

        public static void Main()
        {
            Tick[,] grid = new Tick[MATRIX_SIZE, MATRIX_SIZE];
            Grid.FillGrid(grid, Tick.Empty);
            Grid.PrintGrid(grid);

            Tick firstPlayer = Tick.X;
            Tick secondPlayer = Tick.O;
            Tick currentTick = Tick.Empty;
            bool hasWinner = false;
            int step = 0;
            bool isFirstPlayer = true;

            while (step < MATRIX_SIZE * MATRIX_SIZE)
            {
                GameManager.Step(grid, ref currentTick, ref firstPlayer, ref secondPlayer, ref isFirstPlayer, ref step, ref hasWinner);
                if (hasWinner)
                    break;
            }
            GameManager.WinAction(hasWinner, step, currentTick);
        }
    }

    struct GameManager
    {
        public static void Step(Tick[,] grid, ref Tick currentTick, ref Tick firstPlayer, ref Tick secondPlayer, ref bool isFirstPlayer, ref int step, ref bool hasWinner)
        {
            currentTick = isFirstPlayer ? firstPlayer : secondPlayer;
            step++;
            TickAction.InputTick(grid, currentTick, out int selectedPoint);
            Grid.PrintGrid(grid);

            TickAction.GetCoords(selectedPoint, out int i, out int j);
            hasWinner = Grid.Check(grid, currentTick, i, j);
            if (hasWinner)
            {
                return;
            }

            isFirstPlayer = !isFirstPlayer;
        }

        public static void WinAction(bool hasWinner, int step, Tick currentTick)
        {
            if (hasWinner)
            {
                Console.WriteLine($"Winner is: {currentTick}");
                return;
            }

            if (step == Program.MATRIX_SIZE * Program.MATRIX_SIZE)
            {
                Console.WriteLine("Draw");
            }
        }
    }

    struct TickAction
    {
        public static void GetCoords(int point, out int i, out int j)
        {
            i = (point - 1) / Program.MATRIX_SIZE;
            j = (point - 1) % Program.MATRIX_SIZE;
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

        public static bool HasTick(Tick[,] grid, int point, Tick tick)
        {
            GetCoords(point, out int i, out int j);
            return HasTick(grid, i, j, tick);
        }

        public static bool HasTick(Tick[,] grid, int i, int j, Tick tick)
        {
            return grid[i, j] == tick;
        }

        public static void InputTick(Tick[,] grid, Tick tickTemplate, out int selectedPoint)
        {
            do
            {
                Console.Write("Input point (1 - 9): ");
                string input = Console.ReadLine();
                selectedPoint = int.Parse(input);
            } while (selectedPoint <= 0 || selectedPoint > Program.MATRIX_SIZE * Program.MATRIX_SIZE ||
                     !TickAction.HasTick(grid, selectedPoint, Tick.Empty));

            TickAction.SetTick(grid, selectedPoint, tickTemplate);
        }

        public static void SetTick(Tick[,] grid, int point, Tick tick)
        {
            GetCoords(point, out int i, out int j);
            SetTick(grid, i, j, tick);
        }

        public static void SetTick(Tick[,] grid, int i, int j, Tick tick)
        {
            grid[i, j] = tick;
        }

    }

    struct Grid
    {

        public static bool Check(Tick[,] grid, Tick tick, int i, int j)
        {
            return CheckHorizontal(grid, tick, i, j) || CheckVertical(grid, tick, i, j) ||
                   CheckDiagonal(grid, tick, i, j) || CheckSecondDiagonal(grid, tick, i, j);
        }

        public static bool CheckHorizontal(Tick[,] grid, Tick tick, int i, int j)
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

        public static bool CheckVertical(Tick[,] grid, Tick tick, int i, int j)
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

        public static bool CheckDiagonal(Tick[,] grid, Tick tick, int i, int j)
        {
            for (int k = 0; k < Program.MATRIX_SIZE; k++)
            {
                if (i == k && j == k)
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

        public static bool CheckSecondDiagonal(Tick[,] grid, Tick tick, int i, int j)
        {
            for (int k = 0; k < Program.MATRIX_SIZE; k++)
            {
                int ii = k;
                int jj = Program.MATRIX_SIZE - k - 1;

                if (ii == i && jj == j)
                {
                    continue;
                }


                if (grid[ii, jj] != tick)
                {
                    return false;
                }
            }

            return true;
        }

        public static void PrintGrid(Tick[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write("{0}\t", grid[i, j] == Tick.Empty ? Program.EMPTY : grid[i, j]);
                }

                Console.WriteLine();
            }
        }

        public static void FillGrid(Tick[,] grid, Tick tick)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = tick;
                }
            }
        }
    }
}
