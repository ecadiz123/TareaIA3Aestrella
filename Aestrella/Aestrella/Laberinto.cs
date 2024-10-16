using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;

namespace Aestrella
{
    internal class Punto//Clase Puntos para facilitar trabajo con indice 2D
    {   //pensado como matriz donde
        //i=fila j=columna
        public int i;
        public int j;
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


                //inicializaciones del Punto inicio y punto final
                //se va a hacer el recorrido para cada fila del arreglo
                Punto auxPto = new Punto();
                auxPto.j = Array.FindIndex(charaux, x => x == 'A');//Si este metodo no encuentra el elemento en el arreglo, devuelve -1 en vez del indice 
                //Como arreglo 2D es jagged, el indice de este auxiliar corresponde a las columnas
                auxPto.i = 0;
                if (auxPto.j > 0)
                {
                    this.Inicio = auxPto;
                    this.Actual = this.Inicio;
                }
                //Se repite lo mismo para Final
                auxPto.j = Array.FindIndex(charaux, x => x == 'B');
                
                auxPto.i = 0;
                if (auxPto.j > 0)
                {
                    this.Final = auxPto;
                    
                }


                int size = charaux.Length;//tamaño obtenido manualmente
                this.size = size;
                //como laberintos van a ser cuadrados se usa largode primera linea
                //el arreglo es de tipo escalonado o jagged, donde la cantidad de filas
                //es estatica, pero las columnas son dinamicas, por eso solo se declara las 
                //filas como tamaño
                char[][] auxlab = new char[size][];
                auxlab[0] = charaux;
                int i = 1;//i, contador que se va a usar para rellenar arreglo
                while (aux.Peek()!=-1) 
                {                       

                    charaux = aux.ReadLine().ToCharArray();
                    //inicializacion Pto Inicial
                    auxPto.j= Array.FindIndex(charaux, x => x == 'A');
                    
                    if (auxPto.j > 0)
                    {
                        auxPto.i = i;
                        this.Inicio = auxPto;
                        this.Actual = this.Inicio;
                    }
                    auxPto.j = Array.FindIndex(charaux, x => x == 'B');

                    //Inicializacion Pto FInal
                    if (auxPto.j > 0)
                    {
                        auxPto.i = i;
                        this.Final = auxPto;
                    }
                    auxlab[i] = charaux;
                    i++;
                }
                this.laberinto = auxlab;
                aux.Close();



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
