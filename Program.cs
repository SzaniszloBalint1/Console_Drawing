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

        static void DeleteSelect(int selected)
        {
            int menuWidth = 30;
            int menuHeight = 1;
            int x = (Console.WindowWidth - menuWidth) / 2;
            int y = (Console.WindowHeight - menuHeight) / 2 - 8;

            for (int i = 0; i < savedWorks.Count; i++)
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
                CenterText(currentY + 1, savedWorks[i], menuWidth, x);
            }

            Console.ResetColor();
        }
        static void DeleteMenuSelection(int previous, int current)
        {
            
            int menuWidth = 30;
            int menuHeight = 1;
            int x = (Console.WindowWidth - menuWidth) / 2;
            int y = (Console.WindowHeight - menuHeight) / 2 - 8;

            if (previous > 0)
            {
                int previousY = y + (previous - 1) * 5;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                CenterText(previousY + 1, savedWorks[previous - 1] + ":", menuWidth, x);
            }

            int currentY = y + (current - 1) * 5;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            CenterText(currentY + 1, savedWorks[current - 1] + ":", menuWidth, x);

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
                    DeleteDrawing(10);
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
            Console.WriteLine("Rajzolás folyamatban. Nyomd meg az ESC-et a mentéshez.");
            DrawInConsole();
        }

        static void DrawInConsole()
        {
            int cursorX = Console.WindowWidth / 2, cursorY = Console.WindowHeight / 2;
            Console.CursorVisible = false;
            bool drawing = true;
            StringBuilder currentDrawing = new StringBuilder();

            while (drawing)
            {
                Console.SetCursorPosition(0, 0);
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
            savedWorks.Add(drawingData);
            workCounter++;
            Console.WriteLine($"Munka mentve! [{workCounter}]");
            Console.WriteLine("Nyomj meg egy ESC-et a kilépéshez.");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
        }

        static void EditDrawing()
        {
            Console.Clear();
            Console.WriteLine("Szerkesztés:");
            for (int i = 0; i < savedWorks.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] Munka {i + 1}");
            }
            Console.WriteLine("[ESC] Vissza");

            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape) return;

            if (int.TryParse(key.KeyChar.ToString(), out int selectedWork) && selectedWork > 0 && selectedWork <= savedWorks.Count)
            {
                string selectedDrawing = savedWorks[selectedWork - 1];
                ContinueDrawing(selectedDrawing);
            }
        }

        static void ContinueDrawing(string drawingData)
        {
            Console.Clear();
            Console.CursorVisible = false;

           
            var drawingInstructions = drawingData.Split(new[] { ']' }, StringSplitOptions.RemoveEmptyEntries);
       

       
            foreach (var instruction in drawingInstructions)
            {
                if (string.IsNullOrWhiteSpace(instruction)) continue;

             
                var parts = instruction.Trim('[', ' ').Split(',');
                if (parts.Length == 4 &&
                    int.TryParse(parts[0], out int x) &&
                    int.TryParse(parts[1], out int y) &&
                    char.TryParse(parts[2], out char character) &&
                    int.TryParse(parts[3], out int colorCode))
                {
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = (ConsoleColor)colorCode;
                    Console.Write(character);
                }
            }

            Console.ResetColor();
           
            DrawInConsole();
        }

        static void DeleteDrawing(int selected)
        {
            Console.Clear();
            Console.WriteLine("Törlés:");
            Console.CursorVisible = false;
            int currentSelection = 1;
            int previousSelection = -1;
           DeleteSelect(currentSelection);  
            var keyinfo=Console.ReadKey(true).Key;
            for (int i = 0; i < savedWorks.Count; i++)
            {

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
            }

            Console.ResetColor();
            for (int i = 0; i < savedWorks.Count; i++)
            {
                switch (keyinfo)
                {

                    case ConsoleKey.UpArrow:
                        previousSelection = currentSelection;
                        currentSelection--;
                        if (currentSelection < 1) currentSelection = 4;
                        Console.WriteLine($"[{i + 1}] Munka {i + 1}");
                        break;

                    case ConsoleKey.DownArrow:
                        previousSelection = currentSelection;
                        currentSelection++;
                        if (currentSelection > 1) currentSelection = 1;
                        Console.WriteLine($"[{i + 1}] Munka {i + 1}");
                        break;


                }
                if (currentSelection != previousSelection)
                {
                    DeleteMenuSelection(previousSelection, currentSelection);
                }


            }
            Console.WriteLine("Válassz egy munka törléséhez, vagy nyomj ESC-t a visszalépéshez.");

            
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape) return;
         
            if (int.TryParse(key.KeyChar.ToString(), out int selectedWork) && selectedWork > 0 && selectedWork <= savedWorks.Count)
            {

                savedWorks.RemoveAt(selectedWork - 1);
                Console.WriteLine($"Munka {selectedWork} törölve.");
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
