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
    class Par//Clase simple que guarda un par, uno es el pto el otro es la heuristica. Usado solo en el metodo para encontrar menor heuristica
    {
        private Punto pto = new Punto();
        private int heuristica = 0;
        public Punto Pto { get => pto; set=> pto=value; }
        public int Heuristica { get =>heuristica; set => heuristica=value; }
    }
    enum direcciones_t//direcciones guardadas para que despues se impriman
    {
	UP,DOWN,LEFT,RIGHT
    }
    class Laberinto
    {   //Campos
        private char[][]? laberinto;//signo de pregunta por si acaso porque StreamReader tiende a tirar valores null cuando no deberia
        private Punto inicio;
        private Punto final;
        private Punto actual;
	    private int visitado;//la cantidad total de nodos que visita
	    private int largo_camino;//es el laargo del camino final
        private int size;//guarda el tamaño en un numero, si es 10, laberinto es 10x10
        private List<Punto> visitados=new List<Punto>();//Lista donde se van a guardar puntos que ya hayan sido visitados
        private List<Punto> caminoCorto = new List<Punto>();//Donde se van a guardar los puntos del camino mas corto
	    private List<direcciones_t> camino = new List<direcciones_t>();//Se van a guardar donde pasa el camino, agregar cuando visita, eliminar si visita y se devuelve
        
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
        { return new Punto(actual.i, actual.j - 1);}
        public Punto Derecha(Punto actual)  
        { return new Punto(actual.i, actual.j + 1); }

        //Metodo que revisa si es posible realizar un mov desde un punto
        private bool posibleMov(Punto punto)
        {
            //revisa cuatro Ptos alrededor si es que se puede ir y si no han sido visitados
            Punto arriba = Arriba(punto);
            Punto abajo = Abajo(punto);
            Punto izquierda = Izquierda(punto);
            Punto derecha = Derecha(punto);
            //si por lo menos uno de los cuatro movimientos es posible y no ha sido visitado, se devuelve true
            if ((MovValido(arriba) && !visitados.Contains(arriba)) ||
                (MovValido(abajo) && !visitados.Contains(abajo)) ||
                (MovValido(izquierda) && !visitados.Contains(izquierda)) ||
                (MovValido(derecha) && !visitados.Contains(derecha))
                )
            { return true; }
            else return false;

        }

        /*Funcion recursiva que evaluará si los alrededores se pueden visitar
         * y no han sido visitados ya. Lo hará mediante el metodo posibleMov().
         * En caso de que no se pueda ir a ninguno de los cuatro puntos, 
         * se volverá al anterior y se hará el mismo procedimiento hasta que llegue a un
         * punto que tenga alguna posbilidad de movimiento, el cual será devuelto.
         * 
         * 
         * La idea principal de backtrack es tener un metodo que le permita devolverse
         * Cuando visite puntos que despues no le llevan al camino de menor heuristica
        */
        private Punto backtrack(Punto ptoEval, int indiceVisitados)//el inidiceVisitados se puede conseguir con el metodo IndexOf
        {


            if (posibleMov(ptoEval))//si es posible moverse a cualquiera de los cuatro puntos se devuelve el punto
            {

                return ptoEval;

            }
            else
            {
                visitado++;//Como se va a considerar que se devuelve 1, se le va a sumar a los visitados, este despues se suma al size de la lista visitados
                largo_camino--;//se le quita al largo camino para despues ajustar su lista y sacar los visitados
                return backtrack(visitados[indiceVisitados - 1], indiceVisitados - 1);//usando la lista que guardara los visitados se recurre al punto que vino antes.
            }       
            }
        //metodo heuristica manhattan normal
        private int heuristicaManhattan(Punto pto)
        {
            return pto.Manhattan(final);

        }

        //metodo de f total con la que va a trabajar algoritmo.
        private int fTotalManhattan(Punto pto)
        {
            int heuristica = heuristicaManhattan(pto);
            int costoMovimiento = 1;
            return costoMovimiento + heuristica; 
        }
        private Punto menorHeuristica(Punto pto)
        {
            //Metodo que compara heuristicas de puntos alrededor al punto entregado, devuelve el punto con menor heuristica.

            //c# es medio enredado con su forma de usar los sort, y me dio lata seguir buscando
            // asi que use un sort en una lista cualquiera.
            // si se te ocurre algo mejor dale nomas
            //recuerda mantener siempre la entrada y la salida de la funcion

            Punto arriba = Arriba(actual);
            Punto abajo = Abajo(actual);
            Punto izquierda = Izquierda(actual);
            Punto derecha = Derecha(actual);
            List<Par> menorHeuristica = new List<Par>();
            if (posibleMov(abajo))
            {
                Par aux = new Par();
                aux.Pto = abajo;
                aux.Heuristica = fTotalManhattan(abajo);
                menorHeuristica.Add(aux);
            }
            if (posibleMov(arriba))
            {
                Par aux2 = new Par();
                aux2.Pto = arriba;
                aux2.Heuristica = fTotalManhattan(arriba);
                menorHeuristica.Add(aux2);
            }
            if (posibleMov(izquierda))
            {
                Par aux3 = new Par();
                aux3.Pto = izquierda;
                aux3.Heuristica = fTotalManhattan(izquierda);
                menorHeuristica.Add(aux3);
            }
            if (posibleMov(derecha))
            {
                Par aux4 = new Par();
                aux4.Pto = derecha;
                aux4.Heuristica = fTotalManhattan(derecha);
                menorHeuristica.Add(aux4);
            }
            
            menorHeuristica = menorHeuristica.OrderBy(x => x.Heuristica).ToList();
            return menorHeuristica[0].Pto;
        }
        public void AestrellaHManhattan()
        {
            largo_camino = 1;//se parte de nodo inicial, costo 1
            while (actual!=final)
            { 
            //Se añade actual a la lista de visitados
            visitados.Add(actual);
            //se actualiza el largo del camino corto actual con su tamaño
            
            //Se pasa por el metodo backtrack que asegura que va a estar en un lugar que le va a permitir moverse
            actual = backtrack(actual, visitados.IndexOf(actual));

            //la lista que guardará los puntos del camino mas corto, es decir la que no contara con todos los visitados,
            //copia la lista de todos los visitados, pero solo hasta la cantidad de la variable largo_camino
            // la cual se ajusta con el metodo backtrack.
            //La idea es que se mantiene el largo menor del camino.

            //Mientras mas atras se devuelve se le quita uno al largo del camino final
            //quedando el largo del camino hasta el punto que si le permite moverse

            //finalmente Con el metodo getrange queda una lista del inicio hasta el lugar 
            // que le va a permitir moverse despues de haber pasado por el backtrack
            caminoCorto = visitados.GetRange(0, largo_camino);
                largo_camino += caminoCorto.Count();
                //movimiento del punto actual
                actual = menorHeuristica(actual);
           




            }



        }
    }
}
