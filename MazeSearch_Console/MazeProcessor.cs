namespace MazeSearch_Console
{
    public class MazeProcessor
    {
        private readonly int _row = 10;
        private readonly int _column = 10;
        private readonly int[,] array = new int[10, 10];
        private int StartState;
        private int FinalState;
        private List<int> unpassables = new ();
        private Stack<int> states = new();
        private Dictionary<int,int> path= new ();
        private List<int> pathDFS = new();

        public void Process()
        {
            StartState = 0;
            FinalState= 0;
            unpassables.Clear();
            states.Clear();
            pathDFS.Clear();
            Console.Title = "Maze Search Console Application";
            PopulateArray();
            Display();
            SetStates();
            FindPath();
        }

        private void PopulateArray()
        {
            var value = 0;
            var limit = _row * _column;
            for (int r = 0; r < _row; r++)
            {
                for (int c = 0; c < _column; c++)
                {
                    value += 1;
                    array[r, c] = value;
                }
            }
        }

        private void Display()
        {
            Console.Clear();
            DisplayLabel();
            for (int r = 0; r < _row; r++)
            {
                for (int c = 0; c < _column; c++)
                {
                    if (c == 0)
                    {
                        Console.Write("\t");
                    }

                    Console.Write("|  ");
                    if(array[r, c] == StartState)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }else if (array[r, c] == FinalState)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }else if (unpassables.Contains(array[r, c])){
                        Console.BackgroundColor = ConsoleColor.Red;
                    }else if (pathDFS.Contains(array[r, c]))
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                    }

                    Console.Write(array[r, c] + "\t");
                    Console.ResetColor();

                    if (c == _column - 1)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
            }
        }
        private static void DisplayLabel()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n\tThis is your Maze: ");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write("\t\t0");
            Console.ResetColor();
            Console.Write(" - Start State");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("\t\t\t\t0");
            Console.ResetColor();
            Console.WriteLine(" - End State");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("\t\t0");
            Console.ResetColor();
            Console.Write(" - Blocked/Unpassable");
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.Write("\t\t\t0");
            Console.ResetColor();
            Console.WriteLine(" - Path\n");
        }

        private void SetStates() {
            Console.WriteLine("Input start state: ");
            StartState = Convert.ToInt16(Console.ReadLine());
            states.Push(StartState);
            Console.WriteLine("Input end state: ");
            FinalState = Convert.ToInt16(Console.ReadLine());
            Display();
            SetUnpassables();
        }
        private void SetUnpassables()
        {
            int num;
            do
            {
                Console.WriteLine("Input unpassable states: \n Enter 0 to stop");
                num = Convert.ToInt16(Console.ReadLine());
                if (num == StartState)
                {
                    Console.WriteLine("You cannot make your start state unpassable!" +
                        "\nPlease input another number.");
                }
                else if (num == FinalState)
                {
                    Console.WriteLine("You cannot make your final state unpassable!" +
                        "\nPlease input another number.");
                }
                else {
                    unpassables.Add(num);
                    Display();
                }
            } while (num > 0 && num < 101);
            
        }

        private void FindPath()
        {
            int currentState=StartState;
            int r, c;
            while (currentState != FinalState)
            {
                currentState = states.Pop();
                pathDFS.Add(currentState);
                r = GetRow(currentState);
                c = GetColumn(currentState);
                if(r>0 && r<_row-1 && c>0 && c < _column - 1)
                {
                    pushUp(currentState);
                    pushDown(currentState);
                    pushLeft(currentState);
                    pushRight(currentState);
                }
                else
                {
                    if(r==0)
                    {
                        if (c == 0)
                        {
                            pushDown(currentState);
                            pushRight(currentState);
                        }
                        else if (c == 9)
                        {
                            pushDown(currentState);
                            pushLeft(currentState);
                        }
                        else if (c > 0 && c < _column - 1)
                        {
                            pushDown(currentState);
                            pushLeft(currentState);
                            pushRight(currentState);
                        }
                    }
                    else if(c==0)
                    {
                        if (r > 0 && r < _row - 1)
                        {
                            pushUp(currentState);
                            pushDown(currentState);
                            pushRight(currentState);
                        }
                        else if (r == 9)
                        {
                            pushUp(currentState);
                            pushRight(currentState);
                        }
                    }
                    else if(c==9 && r == 9)
                    {
                        pushUp(currentState);
                        pushLeft(currentState);
                    }
                }
                
            }
            Display();
            
        }

        private int GetRow(int number)
        {
            for (int r = 0; r < _row; r++)
            {
                for (int c = 0; c < _column; c++)
                {
                    if (array[r, c] == number)
                    {
                        return r;
                    }
                }
            }
            return -1;

        }
        private int GetColumn(int number)
        {
            for (int r = 0; r < _row; r++)
            {
                for (int c = 0; c < _column; c++)
                {
                    if (array[r, c] == number)
                    {
                        return c;
                    }
                }
            }
            return -1;

        }

        private void pushUp(int num)
        {
            var r = GetRow(num);
            var c = GetColumn(num);
            var up = array[r - 1, c];
            if (!unpassables.Contains(up) && !states.Contains(up) && !pathDFS.Contains(up))
            {
                states.Push(up);
            }
        }
        private void pushDown(int num)
        {
            var r = GetRow(num);
            var c = GetColumn(num);
            var down = array[r + 1, c];
            if (!unpassables.Contains(down) && !states.Contains(down) && !pathDFS.Contains(down))
            {
                states.Push(down);
            }
        }
        private void pushLeft(int num)
        {
            var r = GetRow(num);
            var c = GetColumn(num);
            var left = array[r, c-1];
            if (!unpassables.Contains(left) && !states.Contains(left) && !pathDFS.Contains(left))
            {
                states.Push(left);
            }
        }
        private void pushRight(int num)
        {
            var r = GetRow(num);
            var c = GetColumn(num);
            var right = array[r, c + 1];
            if (!unpassables.Contains(right) && !states.Contains(right) && !pathDFS.Contains(right))
            {
                states.Push(right);
            }
        }
        private void DisplayPAth()
        {
            if (pathDFS.Last() == FinalState)
            {
                Console.Write("\tPath:\n\t\t");
                foreach (int val in pathDFS)
                {
                    Console.Write(val);
                    if (val != pathDFS.Last())
                    {
                        Console.Write(" -> ");
                    }
                    else
                    {
                        Console.WriteLine("\n");
                    }
                }
            }
            else
            {
                Console.WriteLine("\tPath NOT found!");
            }
        }
    }
}
