using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using static System.Console;
using Newtonsoft.Json;

internal class Program
{
    static string SerializeDrawingData(List<PositionData> drawingData)
    {
        return JsonConvert.SerializeObject(drawingData);
    }

    public class PositionData
{
    public int X { get; set; }
    public int Y { get; set; }
    public char Character { get; set; }
    public string Color { get; set; } = string.Empty;
    }

    static char selectedChar = '█';
    static ConsoleColor selectedColor = ConsoleColor.White;
    static List<string> savedWorks = new List<string>();
    static int workCounter = 0;
    static int currentEditingIndex = -1;

    static void LoadDrawingsFromDatabase()
    {
        using (var context = new DrawingContext())
        {
            var drawings = context.Drawings.ToList();
            foreach (var drawing in drawings)
            {
                Console.WriteLine($"Rajz {drawing.Id}: {drawing.CreatedAt}");
                Console.WriteLine($"Adatok: {drawing.Data}");
            }
        }
    }

    static void LoadDrawingFromFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            string drawingData = File.ReadAllText(fileName);
            ContinueDrawing(drawingData);
        }
        else
        {
            Console.WriteLine("A megadott fájl nem található.");
        }
    }

    static void SaveDrawingToDatabase(string drawingData, string drawingName)
    {
        using (var context = new DrawingContext())
        {
            var existingDrawing = context.Drawings.FirstOrDefault(d => d.Data == drawingData);
            if (existingDrawing == null)
            {
                var drawing = new Drawing
                {
                    Data = drawingData,
                    CreatedAt = DateTime.Now,
                    Name = drawingName 
                };
                context.Drawings.Add(drawing);
                context.SaveChanges();
                Console.WriteLine($"Rajz mentve az adatbázisba: {drawing.Name}"); 
            }
            else
            {
                Console.WriteLine("Ez a rajz már létezik az adatbázisban.");
            }
        }
    }

   

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
            CenterText(currentY + 1, options[i], menuWidth, x);
        }
        Console.WriteLine();
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
            CenterText(previousY + 1, options[previous - 1], menuWidth, x);
        }

        int currentY = y + (current - 1) * 5;
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Black;
        DrawBox(x, currentY, menuWidth, menuHeight);
        CenterText(currentY + 1, options[current - 1], menuWidth, x);
        Console.WriteLine();
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
                Environment.Exit(0);
                break;
        }

        Console.Clear();
    }

    static void CreateDrawing()
    {
        Console.Clear();
        Console.CursorVisible = false;

        currentEditingIndex = -1;

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
#if WINDOWS
        if (windowheight <= Console.LargestWindowHeight && windowwidth <= Console.LargestWindowWidth)
        {
            Console.SetBufferSize(windowwidth, windowheight);
            Console.SetWindowSize(windowwidth, windowheight);
        }
#endif
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

    static void SaveDrawing(string drawingData, string drawingName)
    {
        if (currentEditingIndex == -1)
        {
            savedWorks.Add(drawingData);
            workCounter++;
            Console.WriteLine($"Munka mentve! [{drawingName}]");
        }
        else
        {
            savedWorks[currentEditingIndex] = drawingData;
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

                Console.WriteLine($"Munka {i + 1}");
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
                    currentEditingIndex = selectedWorkIndex;
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
        Keret(Console.WindowWidth, Console.WindowHeight);

        var drawing = drawingData.Split(']');
        foreach (var item in drawing)
        {
            if (!string.IsNullOrEmpty(item))
            {
                var details = item.Trim('[', ' ').Split(',');
                int x = int.Parse(details[0]);
                int y = int.Parse(details[1]);
                char character = char.Parse(details[2]);

                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = selectedColor;
                Console.Write(character);
            }
        }

        DrawInConsole();
    }

    static string GetDrawingName()
    {
        Console.WriteLine("Add meg a rajz nevét:");
        string? drawingName = Console.ReadLine();
        return drawingName ?? string.Empty;
    }

    static void DrawInConsole(int cursorX = -1, int cursorY = -1)
    {
        if (cursorX == -1 || cursorY == -1)
        {
            cursorX = Console.WindowWidth / 2;
            cursorY = Console.WindowHeight / 2;
        }

        Console.SetCursorPosition(cursorX, cursorY);
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
                  
                    string drawingName = GetDrawingName();

                    SaveDrawingToDatabase(currentDrawing.ToString(), drawingName);
                    SaveDrawing(currentDrawing.ToString(), drawingName);
                    drawing = false;
                    break;

                case ConsoleKey.UpArrow:
                    if (cursorY > 1) cursorY--;
                    break;
                case ConsoleKey.DownArrow:
                    if (cursorY < Console.WindowHeight - 2) cursorY++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (cursorX > 1) cursorX--;
                    break;
                case ConsoleKey.RightArrow:
                    if (cursorX < Console.WindowWidth - 2) cursorX++;
                    break;

                case ConsoleKey.Spacebar:
                    Console.SetCursorPosition(cursorX, cursorY);
                    Console.ForegroundColor = selectedColor;
                    Console.Write(selectedChar);
                    Console.ResetColor();
                    currentDrawing.Append($"[{cursorX},{cursorY},{selectedChar}]");
                    break;
            }

            Console.SetCursorPosition(cursorX, cursorY);
        }

        Console.Clear();
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

                Console.WriteLine($"Munka {i + 1}");
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
                    DeleteDrawingFromDatabase(selectedDrawing);
                    savedWorks.RemoveAt(selectedWorkIndex);
                    Console.WriteLine($"Munka törölve! [{selectedWorkIndex + 1}]");
                    deleting = false;
                    break;
                case ConsoleKey.Escape:
                    deleting = false;
                    break;
            }
        }
    }
    static void DeleteDrawingFromDatabase(string drawingData)
    {
        using (var context = new DrawingContext())
        {
            var drawing = context.Drawings.FirstOrDefault(d => d.Data == drawingData);
            if (drawing != null)
            {
                string drawingName = drawing.Name;
                context.Drawings.Remove(drawing);
                context.SaveChanges();
                Console.WriteLine($"Rajz törölve az adatbázisból: {drawingName}");
            }
            else
            {
                Console.WriteLine("A rajz nem található az adatbázisban.");
            }
        }
    }

    static void Main(string[] args)
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
