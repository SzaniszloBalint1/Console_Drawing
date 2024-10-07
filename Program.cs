using System.Drawing;
using static System.Console;
namespace Console_Drawing
{
    internal class Program
    {
        static char selectedChar = '█';
        static ConsoleColor selectedColor = ConsoleColor.White;
        static bool settingsSaved = false; 

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
                Console.ForegroundColor = ConsoleColor.White;
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
            int cursorX = Console.WindowWidth / 2, cursorY = Console.WindowHeight / 2;
            int directionX = 0, directionY = 0;
            char currentChar = selectedChar;
            ConsoleColor currentColor = selectedColor;
            Console.Clear();
            bool running = true;

            switch (selected)
            {
                case 1:
                   
                    Console.Clear();
                    Console.CursorVisible = false;

                    while (running)
                    {
                        Console.SetCursorPosition(0, 0);
                        Console.Write($"X: {cursorX}, Y: {cursorY}, Karakter: {currentChar}, Szín: {currentColor}");
                        Console.WriteLine();

                        var keyinfo = Console.ReadKey(true);

                        switch (keyinfo.Key)
                        {
                            case ConsoleKey.Escape:
                                running = false;
                                break;

                            case ConsoleKey.UpArrow:
                                if (cursorY > 0) cursorY--; 
                                break;

                            case ConsoleKey.DownArrow:
                                if (cursorY < Console.WindowHeight - 1) cursorY++; 
                                break;

                            case ConsoleKey.LeftArrow:
                                if (cursorX > 0) cursorX--;
                                break;

                            case ConsoleKey.RightArrow:
                                if (cursorX < Console.WindowWidth - 1) cursorX++; 
                                break;

                            case ConsoleKey.Spacebar:
                               
                                Console.SetCursorPosition(cursorX, cursorY);
                                Console.ForegroundColor = currentColor;
                                Console.Write(currentChar);
                                Console.ResetColor(); 
                                break;
                        }

                        
                        Console.SetCursorPosition(cursorX, cursorY);
                    }

                    
                    Console.ResetColor();
                    break;

                case 2:
                    Console.WriteLine("Szerkesztés kiválasztva.");
                    Console.WriteLine("Válassz karaktert: █, ▓, ▒, ░");
                    Console.WriteLine("[1] █ ");
                    Console.WriteLine("[2] ▓ ");
                    Console.WriteLine("[3] ▒ ");
                    Console.WriteLine("[4] ░ ");

                    var keyChar = Console.ReadKey(true).Key;
                    switch (keyChar)
                    {
                        case ConsoleKey.D1:
                            selectedChar = '█';
                            break;
                        case ConsoleKey.D2:
                            selectedChar = '▓';
                            break;
                        case ConsoleKey.D3:
                            selectedChar = '▒';
                            break;
                        case ConsoleKey.D4:
                            selectedChar = '░';
                            break;
                    }

                    Console.WriteLine("Válassz színt: Piros, Zöld, Kék");
                    Console.WriteLine("[1] Piros");
                    Console.WriteLine("[2] Zöld");
                    Console.WriteLine("[3] Kék");

                    var keyColor = Console.ReadKey(true).Key;
                    switch (keyColor)
                    {
                        case ConsoleKey.D1:
                            selectedColor = ConsoleColor.Red;
                            break;
                        case ConsoleKey.D2:
                            selectedColor = ConsoleColor.Green;
                            break;
                        case ConsoleKey.D3:
                            selectedColor = ConsoleColor.Blue;
                            break;
                    }

                    settingsSaved = true;  
                    Console.WriteLine("Beállítások mentve. Nyomj meg egy ESC-et a kilépéshez.");
                    while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
                    break;

                case 3:
                    selectedChar = '█';
                    selectedColor = ConsoleColor.White;
                    settingsSaved = false; 
                    Console.WriteLine("Beállítások törölve.");
                    break;

                case 4:
                    Console.WriteLine("Kilépés kiválasztva.");
                    Environment.Exit(0);
                    break;
            }

            Console.Clear();
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
            }
        }
    }
}
