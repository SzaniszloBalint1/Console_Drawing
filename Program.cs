using System.Drawing;
using static System.Console;
namespace Console_Drawing
{
    internal class Program
    {
        static void DrawMenu(int selected)
        {
            string[] options = { "Létrehozás", "Szerkesztés", "Törlés", "Kilépés" };
            int menuWidth = 30;
            int menuHeight = 1;
            int x = (Console.WindowWidth - menuWidth) / 2;
            int y = (Console.WindowHeight - menuHeight) / 2 - 8;

            for (int i = 0; i < options.Length; i++)
            {
                int currentY = y + i * 5;
                DrawBox(x, currentY, menuWidth, menuHeight);
                if (i + 1 == selected)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                CenterText(currentY + 1, options[i] + ":", menuWidth, x);
            }

            Console.ResetColor();
        }

        static void UpdateMenuSelection(int previous, int current)
        {
            string[] options = { "Létrehozás", "Szerkesztés", "Törlés", "Kilépés" };
            int menuWidth = 30;
            int menuHeight = 1;
            int x = (Console.WindowWidth - menuWidth) / 2;
            int y = (Console.WindowHeight - menuHeight) / 2 - 8;


            if (previous > 0)
            {
                int previousY = y + (previous - 1) * 5;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
                CenterText(previousY + 1, options[previous - 1] + ":", menuWidth, x);
            }


            int currentY = y + (current - 1) * 5;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            CenterText(currentY + 1, options[current - 1] + ":", menuWidth, x);

            Console.ResetColor();
        }

        static void ExecuteOption(int selected)
        {
            Console.Clear();
            switch (selected)
            {
                case 1:
                    Console.WriteLine("Létrehozás kiválasztva.");
                    break;
                case 2:
                    Console.WriteLine("Szerkesztés kiválasztva.");
                    break;
                case 3:
                    Console.WriteLine("Törlés kiválasztva.");
                    break;
                case 4:
                    Console.WriteLine("Kilépés kiválasztva.");
                    Environment.Exit(0);
                    break;
            }
            Console.WriteLine("Nyomj meg egy gombot a visszatéréshez...");
            Console.ReadKey();
        }

        static void DrawBox(int x, int y, int width, int height)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("┌" + new string('─', width) + "┐");

            for (int i = 1; i <= height; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("│" + new string(' ', width) + "│");
            }

            Console.SetCursorPosition(x, y + height + 1);
            Console.Write("└" + new string('─', width) + "┘");
        }

        static void CenterText(int y, string text, int containerWidth, int containerX)
        {
            int textX = containerX + (containerWidth - text.Length) / 2;
            Console.SetCursorPosition(textX, y);
            Console.Write(text);
        }


        static void Main()
        {


            Console.CursorVisible = false;
            int currentSelection = 1;
            int previousSelection = -1;

            DrawMenu(currentSelection);

            while (true)
            {
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        previousSelection = currentSelection;
                        currentSelection--;
                        if (currentSelection < 1)
                        {
                            currentSelection = 4;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        previousSelection = currentSelection;
                        currentSelection++;
                        if (currentSelection > 4)
                        {
                            currentSelection = 1;
                        }
                        break;
                    case ConsoleKey.Enter:
                        ExecuteOption(currentSelection);
                        DrawMenu(currentSelection);
                        break;
                    case ConsoleKey.Escape:
                        break;
                }

                if (currentSelection != previousSelection)
                {
                    UpdateMenuSelection(previousSelection, currentSelection);
                }




                /*
                 *  int cursorX = Console.WindowWidth / 2, cursorY = Console.WindowHeight / 2;
            int directionX = 0, directionY = 0;
            char currentChar = '█';
            ConsoleColor currentColor = ConsoleColor.White;

            bool running = true;
            Console.Clear();
            Console.CursorVisible = false;

            while (running)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write($"X: {cursorX}, Y: {cursorY}, Karakter: {currentChar}, Szín: {currentColor}");

                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        running = false;
                        break;

                    case ConsoleKey.UpArrow:
                        directionY = -1;
                        directionX = 0;
                        break;

                    case ConsoleKey.DownArrow:
                        directionY = 1;
                        directionX = 0;
                        break;

                    case ConsoleKey.LeftArrow:
                        directionX = -1;
                        directionY = 0;
                        break;

                    case ConsoleKey.RightArrow:
                        directionX = 1;
                        directionY = 0;
                        break;

                    case ConsoleKey.Spacebar:
                        if (cursorX + directionX >= 0 && cursorX + directionX < Console.WindowWidth &&
                            cursorY + directionY >= 0 && cursorY + directionY < Console.WindowHeight)
                        {
                            cursorX += directionX;
                            cursorY += directionY;
                            Console.SetCursorPosition(cursorX, cursorY);
                            Console.ForegroundColor = currentColor;
                            Console.Write(currentChar);
                        }
                        break;

                    case ConsoleKey.M:
                        bool inMenu = true;

                        while (inMenu)
                        {
                            Console.Clear();
                            Letrehozas();
                            Szerkeztes();
                            Torles();
                            Kilepes();
                            var menuKey = Console.ReadKey(true);

                            switch (menuKey.Key)
                            {
                               case ConsoleKey.UpArrow:
                                    for(int i = 0; i <directionY; i++)
                                    if (directionY==0)
                                    {
                                        Szerkeztes();
                                        Console.ForegroundColor= ConsoleColor.Yellow;
                                    }
                                   
                                    if(directionY==1)
                                    {
                                        Letrehozas();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        directionY++;
                                    }
                                    break;

                                    case ConsoleKey.DownArrow:
                                    if (directionY == 1)
                                    {
                                        directionY--;
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Szerkeztes() ;
                                    }
                                    break;
                            }
                        }

                        Console.Clear();
                        break;
                }
            }

            Console.Clear();
                */


            }
        }
            
        
    }
}
