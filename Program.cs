using System.Drawing;
using System.Text;
using static System.Console;
namespace Console_Drawing
{
    internal class Program
    {
        static char selectedChar = '█';
        static ConsoleColor selectedColor = ConsoleColor.White;
        static List<string> savedWorks = new List<string>();
        static int workCounter = 0;
        static int currentEditingIndex = -1; // Az aktuálisan szerkesztett rajz indexe

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

                if (i + 1 == selected)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                DrawBox(x, currentY, menuWidth, menuHeight);
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
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                DrawBox(x, previousY, menuWidth, menuHeight);
                CenterText(previousY + 1, options[previous - 1] + ":", menuWidth, x);
            }

            int currentY = y + (current - 1) * 5;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            DrawBox(x, currentY, menuWidth, menuHeight);
            CenterText(currentY + 1, options[current - 1] + ":", menuWidth, x);

            Console.ResetColor();
        }

        static void ExecuteOption(int selected)
        {
            switch (selected)
            {
                case 1:
                    CreateDrawing();
                    break;

                case 2:
                    EditDrawing();
                    break;

                case 3:
                    DeleteDrawing();
                    break;

                case 4:
                    Console.WriteLine("Kilépés kiválasztva.");
                    Environment.Exit(0);
                    break;
            }

            Console.Clear();
        }

        static void CreateDrawing()
        {
            Console.Clear();
            Console.CursorVisible = false;

            currentEditingIndex = -1;  // Új rajz készítésekor nincs aktív szerkesztés

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

            Console.Clear();
            int frameWidth = 120;
            int frameHeight = 30;

            GameBox(frameWidth, frameHeight);
            Keret(frameWidth, frameHeight);
            DrawInConsole();
        }

        static void GameBox(int frameWidth, int frameHeight)
        {
            int windowheight = frameHeight;
            int windowwidth = frameWidth;
            if (windowheight <= Console.LargestWindowHeight && windowwidth <= Console.LargestWindowWidth)
            {
                Console.SetBufferSize(windowwidth, windowheight);
                Console.SetWindowSize(windowwidth, windowheight);
            }
        }

        static void Keret(int width, int height)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("┌" + new string('─', width - 2) + "┐");

            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("│" + new string(' ', width - 2) + "│");
            }

            Console.SetCursorPosition(0, height - 1);
            Console.Write("└" + new string('─', width - 2) + "┘");
        }

        static void DrawInConsole()
        {
            int cursorX = Console.WindowWidth / 2, cursorY = Console.WindowHeight / 2;
            Console.CursorVisible = false;
            bool drawing = true;
            StringBuilder currentDrawing = new StringBuilder();

            while (drawing)
            {
                Console.SetCursorPosition(1, 1);
                Console.Write($"X: {cursorX}, Y: {cursorY}, Karakter: {selectedChar}, Szín: {selectedColor}");
                Console.WriteLine();

                var keyinfo = Console.ReadKey(true);
                switch (keyinfo.Key)
                {
                    case ConsoleKey.Escape:
                        SaveDrawing(currentDrawing.ToString());
                        drawing = false;
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
                        Console.ForegroundColor = selectedColor;
                        Console.Write(selectedChar);
                        Console.ResetColor();
                        currentDrawing.Append($"[{cursorX},{cursorY},{selectedChar},{(int)selectedColor}]");
                        break;
                }

                Console.SetCursorPosition(cursorX, cursorY);
            }
            Console.Clear();
        }

        static void SaveDrawing(string drawingData)
        {
            if (currentEditingIndex == -1)
            {
                savedWorks.Add(drawingData);  // Új rajz esetén hozzáadjuk a listához
                workCounter++;
                Console.WriteLine($"Munka mentve! [{workCounter}]");
            }
            else
            {
                savedWorks[currentEditingIndex] = drawingData;  // Meglévő rajz szerkesztésekor felülírjuk
                Console.WriteLine($"Munka frissítve! [{currentEditingIndex + 1}]");
            }

            Console.WriteLine("Nyomj meg egy ESC-et a kilépéshez.");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
        }

        static void EditDrawing()
        {
            Console.Clear();
            if (savedWorks.Count == 0)
            {
                Console.WriteLine("Nincs elmentett munka szerkesztéshez.");
                return;
            }

            Console.WriteLine("Válassz egy munkát a szerkesztéshez:");
            int selectedWorkIndex = 0;
            bool editing = true;

            while (editing)
            {
                Console.Clear();
                for (int i = 0; i < savedWorks.Count; i++)
                {
                    if (i == selectedWorkIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.WriteLine($"[{i + 1}] Munka {i + 1}");
                }
                Console.ResetColor();
                Console.WriteLine("[ESC] Vissza");

                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedWorkIndex = (selectedWorkIndex - 1 + savedWorks.Count) % savedWorks.Count;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedWorkIndex = (selectedWorkIndex + 1) % savedWorks.Count;
                        break;
                    case ConsoleKey.Enter:
                        string selectedDrawing = savedWorks[selectedWorkIndex];
                        currentEditingIndex = selectedWorkIndex;  // Elmentjük az aktuális szerkesztés indexét
                        ContinueDrawing(selectedDrawing);
                        editing = false;
                        break;
                    case ConsoleKey.Escape:
                        editing = false;
                        break;
                }
            }
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
        static void ContinueDrawing(string drawingData)
        {
            Console.Clear();
            Console.WriteLine($"Rajz folytatása [{currentEditingIndex + 1}]");
            DrawInConsole();
        }

        static void DeleteDrawing()
        {
            Console.Clear();
            if (savedWorks.Count == 0)
            {
                Console.WriteLine("Nincs elmentett munka törléshez.");
                return;
            }

            Console.WriteLine("Válassz egy munkát a törléshez:");
            int selectedWorkIndex = 0;
            bool deleting = true;

            while (deleting)
            {
                Console.Clear();
                for (int i = 0; i < savedWorks.Count; i++)
                {
                    if (i == selectedWorkIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.WriteLine($"[{i + 1}] Munka {i + 1}");
                }
                Console.ResetColor();
                Console.WriteLine("[ESC] Vissza");

                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedWorkIndex = (selectedWorkIndex - 1 + savedWorks.Count) % savedWorks.Count;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedWorkIndex = (selectedWorkIndex + 1) % savedWorks.Count;
                        break;
                    case ConsoleKey.Enter:
                        savedWorks.RemoveAt(selectedWorkIndex);
                        Console.WriteLine($"Munka {selectedWorkIndex + 1} törölve.");
                        deleting = false;
                        break;
                    case ConsoleKey.Escape:
                        deleting = false;
                        break;
                }
            }
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
                        if (currentSelection < 1) currentSelection = 4;
                        break;
                    case ConsoleKey.DownArrow:
                        previousSelection = currentSelection;
                        currentSelection++;
                        if (currentSelection > 4) currentSelection = 1;
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