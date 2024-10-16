using ConsoleApp4;

Mapa mapa = new Mapa();

var inicio = mapa.Coordenadas('A');
var objetivo = mapa.Coordenadas('B');

mapa.leerCosas();



Console.WriteLine(inicio);
Console.WriteLine(objetivo);
