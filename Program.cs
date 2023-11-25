using OTUS_FileParallelLoad;
using System.Diagnostics;

Stopwatch timer = new();
FileLoader fileLoader = new();
string currentDir = Directory.GetCurrentDirectory();

Console.WriteLine("Создание файлов");
fileLoader.FileCreate($"{currentDir}/1.txt", "Some interesting text will be here");
fileLoader.FileCreate($"{currentDir}/2.txt", "Some more interesting text will be here");
fileLoader.FileCreate($"{currentDir}/3.txt", "Some more and more and more interesting text will be here");

timer.Start();
var spaces = fileLoader.CheckSpacesIn3Files($"{currentDir}/1.txt", $"{currentDir}/2.txt", $"{currentDir}/3.txt");
timer.Stop();
Console.WriteLine($"Результат работы с тремя файлами: {spaces}, Затраченное время: {timer.Elapsed}");
timer.Reset();

timer.Start();
spaces = fileLoader.CheckSpacesInFilesInPath(currentDir);
timer.Stop();
Console.WriteLine($"Результат работы с файлами в текущей директории: {spaces}, Затраченное время: {timer.Elapsed}");
