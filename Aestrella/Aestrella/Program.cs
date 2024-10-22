// See https://aka.ms/new-console-template for more information
using Aestrella;
string pathrelativo = @"../../../Laberintos/";
Console.WriteLine("Ingresar nombre archivo");
pathrelativo += Console.ReadLine();
Laberinto labprueba= new Laberinto(pathrelativo);

labprueba.AestrellaManhattan();

labprueba.printNodosSol();
labprueba.printLaberintoCamino();
