namespace Console_Drawing
{
    internal class Program
    {
        static void DrawMenu()
        {
            int menuWidth = 50;
            int menuHeight = 13;
            int x = (Console.WindowWidth - menuWidth) / 2;
            int y = (Console.WindowHeight - menuHeight) / 2;

            DrawBox(x, y, menuWidth, menuHeight);
            CenterText(y + 1, "Menü:", menuWidth, x);

            DrawButtonWithBox(x + 3, y + 3, "[1] Karakter módosítása (█, ▓, ▒, ░)");
            DrawButtonWithBox(x + 3, y + 6, "[2] Szín módosítása (Fehér, Piros, Kék, Zöld)");
            DrawButtonWithBox(x + 3, y + 9, "[3] Törlés");
            DrawButtonWithBox(x + 3, y + 12, "[4] Vissza a játékhoz");
        }

        static void DrawCharacterSelection()
        {
            int menuWidth = 50;
            int menuHeight = 13;
            int x = (Console.WindowWidth - menuWidth) / 2;
            int y = (Console.WindowHeight - menuHeight) / 2;

            Console.Clear();
            DrawBox(x, y, menuWidth, menuHeight);
            CenterText(y + 1, "Válaszd ki a karaktert (1-4):", menuWidth, x);

            DrawButtonWithBox(x + 3, y + 3, "[1] █");
            DrawButtonWithBox(x + 3, y + 6, "[2] ▓");
            DrawButtonWithBox(x + 3, y + 9, "[3] ▒");
            DrawButtonWithBox(x + 3, y + 12, "[4] ░");
        }

        static void DrawColorSelection()
        {
            int menuWidth = 50;
            int menuHeight = 13;
            int x = (Console.WindowWidth - menuWidth) / 2;
            int y = (Console.WindowHeight - menuHeight) / 2;

            Console.Clear();
            DrawBox(x, y, menuWidth, menuHeight);
            CenterText(y + 1, "Válaszd ki a színt (1-4):", menuWidth, x);

            DrawButtonWithBox(x + 3, y + 3, "[1] Fehér");
            DrawButtonWithBox(x + 3, y + 6, "[2] Piros");
            DrawButtonWithBox(x + 3, y + 9, "[3] Kék");
            DrawButtonWithBox(x + 3, y + 12, "[4] Zöld");
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

        static void DrawButtonWithBox(int x, int y, string label)
        {
            int boxWidth = label.Length + 2;


            Console.SetCursorPosition(x - 1, y - 1);
            Console.Write("┌" + new string('─', boxWidth) + "┐");

            Console.SetCursorPosition(x - 1, y);
            Console.Write("│");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" " + label + " ");
            Console.ResetColor();
            Console.Write("│");

            Console.SetCursorPosition(x - 1, y + 1);
            Console.Write("└" + new string('─', boxWidth) + "┘");
        }

        static void DrawMessage(string message)
        {
            int messageWidth = 50;
            int x = (Console.WindowWidth - messageWidth) / 2;
            int y = (Console.WindowHeight - 5) / 2;

            DrawBox(x, y, messageWidth, 3);
            CenterText(y + 1, message, messageWidth, x);
            Console.ReadKey(true);
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
                            DrawMenu();

                            var menuKey = Console.ReadKey(true);

                            switch (menuKey.Key)
                            {
                                case ConsoleKey.D1:
                                    DrawCharacterSelection();
                                    var charKey = Console.ReadKey(true);
                                    switch (charKey.Key)
                                    {
                                        case ConsoleKey.D1:
                                            currentChar = '█';
                                            break;
                                        case ConsoleKey.D2:
                                            currentChar = '▓';
                                            break;
                                        case ConsoleKey.D3:
                                            currentChar = '▒';
                                            break;
                                        case ConsoleKey.D4:
                                            currentChar = '░';
                                            break;
                                    }
                                    break;

                                case ConsoleKey.D2:
                                    DrawColorSelection();
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

                                case ConsoleKey.D3:
                                    Console.Clear();
                                    DrawMessage("A pálya törölve.");
                                    break;

                                case ConsoleKey.D4:
                                    inMenu = false;
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
