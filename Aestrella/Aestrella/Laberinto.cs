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
    enum direcciones_t//direcciones guardadas para que despues se impriman
    {
	UP,DOWN,LEFT,RIGHT
    }
    class Laberinto
    {   /*
         * Heuristica: el menor costo en el mejor de los casos, en nuestro caso va a ser cuando
         * no tienes obstaculos. Si heuristica es mayor a costo real, no es admisible.
         * Siempre debe ser menor igual a costo
         * */
        private char[][]? laberinto;//signo de pregunta por si acaso porque StreamReader tiende a tirar valores null cuando no deberia
        private Punto inicio;
        private Punto final;
        private Punto actual;
	private int visitado;//se le va sumando uno por cada nodo que visita
	private int largo_camino;//es el laargo del camino final
        private int size;//guarda el tamaño en un numero, si es 10, laberinto es 10x10
	private List<direcciones_t> camino = new List<direcciones_t>();//Se van a guardar donde pasa el camino, agregar cuando visita, eliminar si visita y se devuelve
        public Laberinto(String path)//constructor donde se le entrega solo el path del archivo de entrada
        {
            if (File.Exists(path) == true)//si existe el archivo
            {
                StreamReader aux = new StreamReader(path);

                //Lectura de la primera linea manual
               
                char[] charaux = aux.ReadLine().ToCharArray();//ToCharArray metodo transforma string a arreglo de char


                

                int size = charaux.Length;//tamaño obtenido manualmente
                this.size = size;//

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
                    
                    
                  
                    auxlab[i] = charaux;
                    i++;
                }
                this.laberinto = auxlab;
                aux.Close();//ya teniendo el laberinto ingresado se cierra el archivo

                //inicializaciones del Punto inicio y punto final
                //se va a recorrer la matriz entera para definir puntos
                Punto auxPto1 = new Punto();//inicio
                Punto auxPto2 = new Punto();//final
                for (int k=0; k<size; k++)
                {
                    if (auxlab[k].Contains('A'))
		    {
			//Sabiendo que A esta en la fila k, se obtiene la columna mediante metodo que devuelve el indice de arreglo
                        auxPto1.i = k;
                        auxPto1.j = Array.FindIndex(auxlab[k], x => x == 'A');
                        this.inicio= auxPto1;
                        this.actual = auxPto1;
                    }
                    if (auxlab[k].Contains('B'))
                    {
			//Sabiendo que B esta en la fila k, se obtiene la columna mediante metodo que devuelve el indice de arreglo
                        auxPto2.i = k;
                        auxPto2.j = Array.FindIndex(auxlab[k], x => x == 'B');
                        this.final= auxPto2;
                    }
                }
		//Inicializacion Contadores
		this.visitado=0;
		this.largo_camino=0;


                
               


            }

        }
        public void printLaberinto()//metodo de prueba usado para printear laberinto
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(laberinto[i][j]);
                }
                Console.WriteLine();
            }
            
            Console.WriteLine($"inicio: {inicio.i},{inicio.j}");
            Console.WriteLine($"final: {final.i},{final.j}");
	    
        }
    }
}
