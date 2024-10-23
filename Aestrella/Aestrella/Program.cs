using Aestrella;
using System.Diagnostics;

string pathrelativo = @"../../../Laberintos/";
Console.WriteLine("Ingresar nombre archivo");
pathrelativo += Console.ReadLine();


int a = 0;

do
{

    Console.WriteLine("\n==============================");
    Console.WriteLine("\n¿Qué heurística desea usar?");
    Console.WriteLine("    1. Heurística Manhattan");
    Console.WriteLine("    2. Heurística Escalada");
    Console.WriteLine("\n==============================");
    int eleccion = int.Parse(Console.ReadLine());

    switch (eleccion)
    {
        case 1:


            Laberinto manhattan = new Laberinto(pathrelativo);
            manhattan.AestrellaManhattan();

            manhattan.printSol();
            a = 1;
            break;

        case 2:


            Laberinto escalada = new Laberinto(pathrelativo);
            escalada.AestrellaManhattan();

            escalada.printSol();
            a = 1;
            break;
    }

} while (a == 0);
