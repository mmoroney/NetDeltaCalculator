// See https://aka.ms/new-console-template for more information
using NetDeltaCalculator;
using System.Runtime.CompilerServices;

if (args.Length < 1 || args.Length > 2) {
    Console.WriteLine("Usage:");
    Console.WriteLine("NetDeltaCalculator.exe company [fileName]");
    Environment.Exit(-1);
}

long timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
double netDelta = Calculator.Get(args[0]);
string output = $"{timestamp}, {netDelta}";
Console.WriteLine(output);

if (args.Length == 2) {
    File.AppendAllLines(args[1], new string[] { output });
}
