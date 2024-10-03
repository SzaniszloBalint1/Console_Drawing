using System.Drawing;

namespace Console_Drawing
{
    internal class Program
    {
        static void Letrehozas()
        {
            int menuWidth = 30;
            int menuHeight = 1;
            int x = (Console.WindowWidth - menuWidth) / 2;
            int y = (Console.WindowHeight - menuHeight) / 2;

            DrawBox(x, y-8, menuWidth, menuHeight);
            CenterText(y -7, "Létrehozás:", menuWidth, x);
        }
        static void Szerkeztes()
        {
            int menuWidth = 30;
            int menuHeight = 1;
            int x = (Console.WindowWidth - menuWidth) / 2;
            int y = (Console.WindowHeight - menuHeight) / 2;

            DrawBox(x, y-3, menuWidth, menuHeight);
            CenterText(y-2, "Szerkesztés:", menuWidth, x);
           
        }
        static void Torles()
        {
            int menuWidth = 30;
            int menuHeight = 1;
            int x = (Console.WindowWidth - menuWidth) / 2;
            int y = (Console.WindowHeight - menuHeight) / 2;

            DrawBox(x, y+2 , menuWidth, menuHeight);
            CenterText(y + 3, "Törlés:", menuWidth, x);
        }
        static void Kilepes()
        {
            int menuWidth = 30;
            int menuHeight = 1;
            int x = (Console.WindowWidth - menuWidth) / 2;
            int y = (Console.WindowHeight - menuHeight) / 2;

            DrawBox(x, y+7 , menuWidth, menuHeight);
            CenterText(y+8, "Kilepes:", menuWidth, x);
            
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
            int cursorX = Console.WindowWidth / 2, cursorY = Console.WindowHeight / 2;
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
                                    var choose = Console.ReadKey(true);

                                    switch (choose.Key)
                                    {
                                        
                                    }
                                   
                                    var colorKey = Console.ReadKey(true);
                                    switch (colorKey.Key)
                                    {
                                        case ConsoleKey.D1:
                                            currentColor = ConsoleColor.White;
                                            break;
                                        case ConsoleKey.D2:
                                            currentColor = ConsoleColor.Red;
                                            break;
                                        case ConsoleKey.D3:
                                            currentColor = ConsoleColor.Blue;
                                            break;
                                        case ConsoleKey.D4:
                                            currentColor = ConsoleColor.Green;
                                            break;
                                    }
                                    break;
                            }
                        }

                        Console.Clear();
                        break;
                }
            }

            Console.Clear();
        }
        }
}
