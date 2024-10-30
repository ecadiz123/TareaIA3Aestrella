// See https://aka.ms/new-console-template for more information
using Aestrella;
string pathrelativo = @"../../../Laberintos/";
Console.WriteLine("Ingresar nombre archivo");
pathrelativo += Console.ReadLine();
Laberinto labprueba= new Laberinto(pathrelativo);

Console.WriteLine("Elija Heuristica a utilizar:");
Console.WriteLine("1. Manhattan");
Console.WriteLine("2. Alternativa");
int x;
x=int.Parse(Console.ReadLine());
switch (x)
{
    case 1:
        labprueba.AestrellaManhattan();
        break;
   
   

}

labprueba.printNodosSol();
labprueba.printLaberintoCamino();
