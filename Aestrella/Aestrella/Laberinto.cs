using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Data;

namespace Aestrella
{
    internal class Punto//Clase Puntos para facilitar trabajo con indice 2D
    {   //pensado como matriz donde
        //i=fila j=columna
        public int i;
        public int j;
        public Punto()
        {
            this.i = 0;
            this.j = 0;
        }
    }
    class Laberinto
    {   /*
         * Heuristica: el menor costo en el mejor de los casos, en nuestro caso va a ser cuando
         * no tienes obstaculos. Si heuristica es mayor a costo real, no es admisible.
         * Siempre debe ser menor igual a costo
         * */
        private char[][]? laberinto;
        private Punto Inicio;
        private Punto Final;
        private Punto Actual;
        private int size;//guarda el tamaño en un numero, si es 10, laberinto es 10x10
        public Laberinto(String path)//constructor donde se le entrega solo el path del archivo de entrada
        {
            if (File.Exists(path) == true)//si existe el archivo
            {
                StreamReader aux = new StreamReader(path);

                //Lectura de la primera linea manual
               
                char[] charaux = aux.ReadLine().ToCharArray();


                

                int size = charaux.Length;//tamaño obtenido manualmente
                this.size = size;
                //como laberintos van a ser cuadrados se usa largo de primera linea
                //el arreglo es de tipo escalonado o jagged, donde la cantidad de filas
                //es estatica, pero las columnas son dinamicas, por eso solo se declara las 
                //filas como tamaño
                char[][] auxlab = new char[size][];
                auxlab[0] = charaux;
                int i = 1;//i, contador que se va a usar para rellenar arreglo
                while (aux.Peek()!=-1) //cuando metodo peek de StreamReader devuelve -1, no hay mas caracteres en el archivo
                {                       

                    charaux = aux.ReadLine().ToCharArray();
                    //inicializacion Pto Inicial
                    
                    
                  
                    auxlab[i] = charaux;
                    i++;
                }
                this.laberinto = auxlab;
                aux.Close();//ya teniendo el laberinto ingresado se cierra el archivo

                //inicializaciones del Punto inicio y punto final
                //se va a recorrer la matriz entera para definir puntos
                Punto auxPto1 = new Punto();//Inicio
                Punto auxPto2 = new Punto();//Final
                for (int k=0; k<size; k++)
                {
                    if (auxlab[k].Contains('A'))
		    {
			//Sabiendo que A esta en la fila k, se obtiene la columna mediante metodo que devuelve el indice de arreglo
                        auxPto1.i = k;
                        auxPto1.j = Array.FindIndex(auxlab[k], x => x == 'A');
                        this.Inicio= auxPto1;
                        this.Actual = auxPto1;
                    }
                    if (auxlab[k].Contains('B'))
                    {
			//Sabiendo que B esta en la fila k, se obtiene la columna mediante metodo que devuelve el indice de arreglo
                        auxPto2.i = k;
                        auxPto2.j = Array.FindIndex(auxlab[k], x => x == 'B');
                        this.Final= auxPto2;
                    }
                }
                
               


            }

        }
        public void printLaberinto()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(laberinto[i][j]);
                }
                Console.WriteLine();
            }
            
            Console.WriteLine($"Inicio: {Inicio.i},{Inicio.j}");
            Console.WriteLine($"Final: {Final.i},{Final.j}");
        }
    }
}
