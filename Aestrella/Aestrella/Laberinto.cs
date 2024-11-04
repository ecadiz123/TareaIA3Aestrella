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
        private char[,]? laberinto;//signo de pregunta por si acaso porque StreamReader tiende a tirar valores null cuando no deberia
        private Punto inicio;
        private Punto final;
        private Nodo inicial;
       
        private Dictionary<Punto,Nodo> entrada = new Dictionary<Punto, Nodo>();//Diccionario que va a guardar nodos a visitar
        private Dictionary<Punto,Nodo> salida = new Dictionary<Punto, Nodo>();//Diccionario que va a guardar nodos visitados
        private List<Nodo> solucion = new List<Nodo>();//Donde se va a guardar nodos de camino mas corto partiendo del inicio al final
        private int size;//guarda el tamaño en un numero, si es 10, laberinto es 10x10
        private Nodo llegada;

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

                char[,] auxlab = new char[size,size];
                for (int j = 0;j<size;j++)
                {
                    auxlab[0,j]= charaux[j];
                }
                int i = 1;//i, contador que se va a usar para rellenar arreglo
                while (aux.Peek() != -1) //cuando metodo peek de StreamReader devuelve -1, no hay mas caracteres en el archivo
                {

                    charaux = aux.ReadLine().ToCharArray();
                    for (int j = 0; j < size; j++)
                    {
                        auxlab[i,j] = charaux[j];
                    }

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
                    for (int j = 0; j < size; j++)
                    {
                        if (auxlab[k, j] == 'A')
                        {
                            //Sabiendo que A esta en la fila k, se obtiene la columna mediante metodo que devuelve el indice de arreglo
                            auxPto1.i = (short)k;
                            auxPto1.j = (short)j;
                            this.inicio = auxPto1;

                        }
                        if (auxlab[k, j] == 'B')
                        {
                            //Sabiendo que B esta en la fila k, se obtiene la columna mediante metodo que devuelve el indice de arreglo
                            auxPto2.i = (short)k;
                            auxPto2.j = (short)j;
                            this.final = auxPto2;
                        }
                    }
                }
                






            }

        }
        public void printLaberinto()//metodo de prueba usado para printear laberinto
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(laberinto[i,j]);
                }
                Console.WriteLine();
            }


        }
        public void printInicioFinal()
        {

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
            else if (laberinto[punto.i,punto.j] == '#') { return false; }

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


        public int Manhattan(Punto puntoInicial, Punto puntoFinal)
        {
            return Math.Abs(puntoInicial.i - puntoFinal.i ) + Math.Abs(puntoInicial.j - puntoFinal.j);
        }
        public float heuristicaAlt(Punto puntoInicial, Punto puntoFinal)
        {
            return 0.3f * (float)Manhattan(puntoInicial,puntoFinal);

        }
        private Nodo[] vecinosAArreglo(Nodo actual) 
        {
            Nodo[] vecinos = new Nodo[4];//Arreglo de tamaño maximo 4
            if (MovValido(Arriba(actual.Pto)))
            {   //si es valido se crea como nodo y se agrega
                Nodo arriba = new Nodo(actual, Arriba(actual.Pto), Manhattan(Arriba(actual.Pto),this.final));
                vecinos[0]=arriba;
            }else
            {
                vecinos[0] = new Nodo(new Punto(-1, -1), 0);
            }
            if (MovValido(Abajo(actual.Pto)))
            {   //si es valido se crea como nodo y se agrega
                Nodo abajo = new Nodo(actual, Abajo(actual.Pto), Manhattan(Abajo(actual.Pto),this.final));
                vecinos[1]=abajo;
            }else
            {
                vecinos[1] = new Nodo(new Punto(-1, -1), 0);
            }
            if (MovValido(Izquierda(actual.Pto)))
            {   //si es valido se crea como nodo y se agrega
                Nodo izquierda = new Nodo(actual, Izquierda(actual.Pto), Manhattan(Izquierda(actual.Pto),this.final));
                vecinos[2]=izquierda;
            }else
            {
                vecinos[2] = new Nodo(new Punto(-1, -1), 0);
            }
            if (MovValido(Derecha(actual.Pto)))
            {   //si es valido se crea como nodo y se agrega
                Nodo derecha = new Nodo(actual, Derecha(actual.Pto), Manhattan(Derecha(actual.Pto),this.final));
                vecinos[3]=derecha;
            }else
            {
                vecinos[3] = new Nodo(new Punto(-1, -1), 0);
            }
            return vecinos;//Se devuelve arreglo con nodos asignados, los que no fueron asignados se devuelven como valor -1
        }

        //metodo que devuelve camino mas corto en lista
        public void AestrellaManhattan()
        {
            //inicializacion de nodos con heuristica de Manhattan
            this.inicial = new Nodo(this.inicio, Manhattan(this.inicio,this.final));//Se inicializa nodo inicial con constructor
            PriorityQueue<Nodo,double> menorF = new PriorityQueue<Nodo, double>();//fila que se va a usar para consultar rapido el menor valor de F(Es de complejidad log(n) porque usa heap)

            menorF.Enqueue(this.inicial, this.inicial.FTotal);//Se añade a fila
            entrada.Add(this.inicial.Pto,this.inicial);//Se añade a nodos a visitar
            Nodo actual = new Nodo();//nodo actual vacio
            while (true)         
            {

                actual = menorF.Dequeue();//el actual se vuelve el con menor F
                if (actual.Pto.Equals(this.final))//Condicion de termino
                    break;


                entrada.Remove(actual.Pto);//Se saca de nodos a visitar
                salida.Add(actual.Pto, actual);

                //ahora se revisan los 4 vecinos mediante lista

                //actual pasa por metodo que manda vecinos validos a lista
                Nodo[] vecinos = vecinosAArreglo(actual);


                //Se revisan vecinos validos


                foreach (Nodo nodovecino in vecinos)
                {
                    if (nodovecino.Pto.i != -1)//Si el nodo contiene un punto al cual no se puede ir(es obstaculo o esta fuera de matriz), su punto es (-1,-1)
                    {
                        if (entrada.ContainsKey(nodovecino.Pto))
                        {
                            
                            if (entrada[nodovecino.Pto].CostoAcumulado > nodovecino.CostoAcumulado) //Se revisa que si G(costo) del vecino es mayor
                            {
                                //Se elimina de la entrada porque cuesta mas que el vecino
                                entrada.Remove(entrada[nodovecino.Pto].Pto);

                            }


                        }
                        if (salida.ContainsKey(nodovecino.Pto))
                        {
                            if (salida[nodovecino.Pto].CostoAcumulado > nodovecino.CostoAcumulado) //Se revisa que si G(costo) del vecino
                            {
                                //Se elimina de la entrada porque cuesta mas que el vecino
                                entrada.Remove(salida[nodovecino.Pto].Pto);

                            }


                        }
                        if (!salida.ContainsKey(nodovecino.Pto) && !entrada.ContainsKey(nodovecino.Pto))
                        {
                            //si el vecino no se encuentra en ninguna de las dos listas es porque no ha sido visitado
                            //por lo que se agrega a la entrada para que pueda serlo
                            entrada.Add(nodovecino.Pto, nodovecino);
                            //Ademas se agrega a la fila que se ordena por prioridad
                            menorF.Enqueue(nodovecino, nodovecino.FTotal);
                            /*Nota sobre la fila ordenada por prioridad:
                             * 
                             * Esta podria llegar a contener dos puntos iguales pero con distinta Ftotal
                             * pero como se trabaja con valor de heuristica, solo se le ingresarian
                             * puntos que tengan misma heuristica pero menor costo.
                             * 
                             * Lo que deberia ocurrir es que al trabajar con valores con menor Ftotal
                             * nunca se deberia llegar a visitar los nodos repetidos que tenian mayor F, ya que 
                             * se sigue acercando al objetivo con los nodos con menor F.
                             * 
                             * En otras palabras, si llegara a visitar el nodo que esta repetido en la fila
                             * solo visitaria la version de menor F y la de mayor F seguiria en la fila, pero no 
                             * se deberia llegar a esta antes que el nodo objetivo.
                             * 
                             * Este caso es para heuristicas admisibles, y como vamos a usar
                             * solo aquellas que lo son, este es nuestro caso. No estoy seguro
                             * que pasaria con heuristicas no admisibles

                             */
                        }


                    }
                }

                //una vez termina el loop se limpia arreglo para la siguiente iteracion
                Array.Clear(vecinos);


            }




            //Donde se guarda camino mas corto a solucion en lista
            Nodo? aux = actual;
            while (aux.padre != null)
            {
                this.solucion.Add(aux);
                aux = aux.padre;
            }
            this.solucion.Add(aux);//Se ingresa el primer nodo

            //Por la manera en que se ingresaron los datos
            //la lista esta del nodo final al inicial, entonces se invierte
            //para que comience del primer nodo
            this.solucion.Reverse();

        }


        public void printNodosSol()
        {
            //printeo de lista con camino al mas corto
            foreach (Nodo nodo in solucion)
            {
                Console.WriteLine($"punto: {nodo.Pto.i},{nodo.Pto.j}");

            }

          
        }
        public void printNumNodos()
        {
            Console.WriteLine($"Nodos salida: {salida.Count}");//nodos salida: nodos visitados por el algoritmo
            Console.WriteLine($"Costo final: {solucion.Count - 1}");//Costo final es cantidad solucion - 1 porque no hay que contar el nodo inicial
        }
        public void printLaberintoCamino()
        {
            char[,] labaux = this.laberinto;//se hace un laberinto auxiliar
            //se hace camino en todos los nodos de solucion excepto por A y B
            foreach (Nodo nodo in solucion)
            {
                if (!(nodo.Equals(solucion.Last()) || nodo.Equals(solucion[0])))//condicion para que no se reemplazen A y B
                    labaux[nodo.Pto.i,nodo.Pto.j] = 'x';//caracter del cual se va a hacer el camino
            }
            //copiado y pegado del metodo printLaberinto
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(labaux[i, j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Nodos salida: {salida.Count}");//nodos salida: nodos visitados por el algoritmo
            Console.WriteLine($"Costo final: {solucion.Count - 1}");//Costo final es cantidad solucion - 1 porque no hay que contar el nodo inicial
        }
        public void printFormatoProfe()
        {

        }
    }
}
