// See https://aka.ms/new-console-template for more information
using Aestrella;
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
            manhattan.printLaberinto();
            manhattan.AestrellaManhattan();

            manhattan.printSol();
            manhattan.printLaberinto();
            a = 1;
            break;

        case 2:
            Laberinto escalada = new Laberinto(pathrelativo);
            escalada.printLaberinto();
            escalada.AestrellaManhattan();

            escalada.printSol();
            escalada.printLaberinto();
            a = 1;
            break;
    }

} while (a == 0);
