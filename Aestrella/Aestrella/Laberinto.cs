using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Data;
using System.ComponentModel;

namespace Aestrella
{
    
    enum direcciones_t//direcciones guardadas para que despues se impriman
    {
        UP, DOWN, LEFT, RIGHT
    }
    class Laberinto
    {   //Campos
        private char[][]? laberinto;//signo de pregunta por si acaso porque StreamReader tiende a tirar valores null cuando no deberia
        private Punto inicio;
        private Punto final;
        private Nodo inicial;
        
        private List<Nodo> entrada= new List<Nodo>();//lista que va a guardar nodos que se van a evaluar
        private List<Nodo> salida = new List<Nodo>();//lista que va a guardar camino final
        
        private int size;//guarda el tamaño en un numero, si es 10, laberinto es 10x10
       

        //Metodos
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
                while (aux.Peek() != -1) //cuando metodo peek de StreamReader devuelve -1, no hay mas caracteres en el archivo
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
                for (int k = 0; k < size; k++)
                {
                    if (auxlab[k].Contains('A'))
                    {
                        //Sabiendo que A esta en la fila k, se obtiene la columna mediante metodo que devuelve el indice de arreglo
                        auxPto1.i = k;
                        auxPto1.j = Array.FindIndex(auxlab[k], x => x == 'A');
                        this.inicio = auxPto1;
                        
                    }
                    if (auxlab[k].Contains('B'))
                    {
                        //Sabiendo que B esta en la fila k, se obtiene la columna mediante metodo que devuelve el indice de arreglo
                        auxPto2.i = k;
                        auxPto2.j = Array.FindIndex(auxlab[k], x => x == 'B');
                        this.final = auxPto2;
                    }
                }
                this.inicial = new Nodo(this.inicio, this.inicio.Manhattan(this.final));//Se inicializa nodo inicial







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

        public bool MovValido(Punto punto)
        {
            // Verifica que el siguiente punto al que se quiere mover esté dentro del 
            // límite establecido por el laberinto (o sea, la matriz).
            // Si está fuera, entonces retorna falso.
            if (punto.i >= size) { return false; }
            else if (punto.j >= size) { return false; }
            else if (punto.i < 0) { return false; }
            else if (punto.j < 0) { return false; }

            // Verifica que el siguiente punto no sea un obstáculo, en caso de serlo, 
            // retorna falso.
            else if (laberinto[punto.i][punto.j] == '#') { return false; }

            // Si todas las condiciones se cumplen, retorna true, es decir, el movimiento es válido.
            return true;
        }

        // Retorna las coordenadas dependiendo de lo que se llame, permitiendo
        // el movimiento hacia las cuatro direcciones.
        public Punto Arriba(Punto actual)
        { return new Punto(actual.i - 1, actual.j); }
        public Punto Abajo(Punto actual)
        { return new Punto(actual.i + 1, actual.j); }
        public Punto Izquierda(Punto actual)
        { return new Punto(actual.i, actual.j - 1); }
        public Punto Derecha(Punto actual)
        { return new Punto(actual.i, actual.j + 1); }



       

       
        //metodo heuristica manhattan normal
        private double heuristicaManhattan(Nodo nodo) //metodo que calcula el valor de la heuristica de Manhattan del nodo ingresado y lo retorna
        {
            return Convert.ToDouble(nodo.Pto.Manhattan(this.final));
            
        }

        
       
        
    }
}
