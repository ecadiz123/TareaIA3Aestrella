// See https://aka.ms/new-console-template for more information
using Aestrella;
using System.Diagnostics;
string pathrelativo = @"../../../Laberintos/";
Console.WriteLine("Ingresar nombre archivo");
pathrelativo += Console.ReadLine();
Laberinto labprueba= new Laberinto(pathrelativo);
Stopwatch stopwatch= new Stopwatch();
Console.WriteLine("Elija Heuristica a utilizar:");
Console.WriteLine("1. Manhattan");
Console.WriteLine("2. Alternativa");
int x;
x=int.Parse(Console.ReadLine());
switch (x)
{
    case 1:
        stopwatch.Start();
        labprueba.AestrellaManhattan();
        stopwatch.Stop();
        break;
   
   

}

Console.WriteLine($"Tiempo {stopwatch.Elapsed}");
