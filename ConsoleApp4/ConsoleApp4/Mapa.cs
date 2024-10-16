using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Mapa
    {
        // Se crea la ruta en la que se leerán los archivos, junto a un vector
        // que guardará las lineas. Filas y columnas se crean para guardar la cantidad de
        // valores que leyó, para así crear la matriz que contendrá todo.

        public string ruta = new string("../../../Laberintos/6_laberinto_10x10.txt");
        public string[] leerLineas;
        public int filas;
        public int columnas;
        public char[,] infoLaberinto;


        // Constructor de Mapa, en donde se inician los valores,
        public Mapa()
        {

            leerLineas = File.ReadAllLines(ruta);
            filas = leerLineas.Length;
            columnas = leerLineas[0].Length;
            infoLaberinto = new char[filas, columnas];

            // Matriz que se rellena de los datos del archivo
            for (int i = 0; i < filas; i++) 
            {
                for (int j = 0; j < columnas; j++)
                {
                    infoLaberinto[i, j] = leerLineas[i][j];
                }
            }
        }

        // Se pone doble int para que el método pueda retornar dos
        // valores, los cuales serán las coordenadas.

        public (int, int) Coordenadas(char punto)
        {
            for (int i = 0 ; i < filas ; i++)
            {
                for (int j = 0 ; j < columnas ; j++)
                {
                    if (infoLaberinto[i, j] == punto)
                    {
                        return (i, j);
                    }
                }
            }

            return (-1, -1);
        }

        //´Prueba para ver que el mapa se imprima correctamente
        public void leerCosas()
        {

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    //Write para que no haga un salto de linea y continue 
                    //imprimiendo para la misma fila.
                    Console.Write(infoLaberinto[i, j]);
                }
                //Salto de linea para dar lugar a la siguiente fila.
                Console.WriteLine();
            }
        }



    }
}
