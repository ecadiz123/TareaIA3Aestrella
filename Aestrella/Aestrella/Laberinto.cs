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
        private Nodo actual;

        private List<Nodo> entrada = new List<Nodo>();//lista que va a guardar nodos que se van a evaluar
        private List<Nodo> salida = new List<Nodo>();//lista que va a guardar camino final

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
                this.inicial = new Nodo(this.inicio, this.inicio.Manhattan(this.final));//Se inicializa nodo inicial con constructor

                this.actual = this.inicial;






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


        
        private List<Nodo> vecinosALista(Nodo actual)//metodo que recibe un nodo y revisa que los vecinos de este sean admisibles. Retorna una lista con los vecinos que si lo son
        {
            List<Nodo> vecinos = new List<Nodo>();
            if (MovValido(Arriba(actual.Pto)))
            {   //si es valido se crea como nodo y se agrega
                Nodo arriba = new Nodo(actual, Arriba(actual.Pto), Arriba(actual.Pto).Manhattan(this.final));
                vecinos.Add(arriba);
            }
            if (MovValido(Abajo(actual.Pto)))
            {   //si es valido se crea como nodo y se agrega
                Nodo abajo = new Nodo(actual, Abajo(actual.Pto), Abajo(actual.Pto).Manhattan(this.final));
                vecinos.Add(abajo);
            }
            if (MovValido(Izquierda(actual.Pto)))
            {   //si es valido se crea como nodo y se agrega
                Nodo izquierda = new Nodo(actual, Izquierda(actual.Pto), Izquierda(actual.Pto).Manhattan(this.final));
                vecinos.Add(izquierda);
            }
            if (MovValido(Derecha(actual.Pto)))
            {   //si es valido se crea como nodo y se agrega
                Nodo derecha = new Nodo(actual, Derecha(actual.Pto), Derecha(actual.Pto).Manhattan(this.final));
                vecinos.Add(derecha);
            }
            return vecinos;
        }

        private bool revisaPtoEnLista(Nodo input, List<Nodo> lista)
        {
            foreach (Nodo nodo in lista)
            {
                if (input.Pto.i == nodo.Pto.i && input.Pto.j == nodo.Pto.j)
                    return true;
            }
            return false;
        }


        //metodo heuristica manhattan normal
        private double heuristicaManhattan(Nodo nodo) //metodo que calcula el valor de la heuristica de Manhattan del nodo ingresado y lo retorna
        {
            return Convert.ToDouble(nodo.Pto.Manhattan(this.final));

        }

        //metodo que al correr tendra el camino mas corto en salida 
        public void AestrellaManhattan()
        {
            entrada.Add(inicial);//se inicializa la entrada con el nodo inicial
           
            while (entrada[0].Pto.i != this.final.i || entrada[0].Pto.j!=this.final.j)//mientras el punto del nodo actual sea distinto del punto final
            {
                
                
                actual = entrada[0];//el actual se vuelve el menor de la lista
               
                entrada.Remove(actual);//se saca de la lista entrada
                
                salida.Add(actual);//se añade a la salida
               
                    //ahora se revisan los 4 vecinos mediante lista

                //actual pasa por metodo que manda vecinos validos a lista
                List<Nodo> vecinos = vecinosALista(actual);
               

                //Se revisan vecinos validos
                
                
                foreach (Nodo nodovecino in vecinos)
                {
                    
                    if (revisaPtoEnLista(nodovecino, entrada))//si el pto del nodo vecino esta en entrada
                    {
                        
                        Nodo auxentrada = new Nodo(nodovecino.Pto, nodovecino.Pto.Manhattan(this.final));
                        auxentrada = entrada.Find(x => x.Pto.i == nodovecino.Pto.i && x.Pto.j == nodovecino.Pto.j);//punto del nodo vecino que esta en entrada declarado como variable

                        if (auxentrada.CostoAcumulado > nodovecino.CostoAcumulado)//En el caso de que el nodo de la entrada que tiene el mismo punto que el vecino tenga mayor costo
                        {
                            //Se elimina porque que cuesta mas
                            entrada.Remove(auxentrada);
                            
                        }


                    }
                    if (revisaPtoEnLista(nodovecino, salida))//si el pto del nodo vecino esta en salida
                    {
                        
                        Nodo auxsalida = new Nodo(nodovecino.Pto, nodovecino.Pto.Manhattan(this.final));
                        auxsalida = salida.Find(x => x.Pto.i == nodovecino.Pto.i && x.Pto.j==nodovecino.Pto.j);
                        if (auxsalida.CostoAcumulado > nodovecino.CostoAcumulado)//En el caso de que el nodo de la salida que tiene el mismo punto que el vecino tenga mayor costo
                        {
                            //Se elimina porque que cuesta mas
                            salida.Remove(auxsalida);
                            

                        }


                    }
                    if (!revisaPtoEnLista(nodovecino,entrada) && !revisaPtoEnLista(nodovecino,salida))
                    {
                        //si el vecino no se encuentra en ninguna de las dos listas es porque no ha sido visitado
                        //por lo que se agrega a la entrada para que pueda serlo
                        entrada.Add(nodovecino);
                        ;
                    }
                    
                    
                    
                }
         
                //una vez termina el loop se limpia lista para la siguiente iteracion
                vecinos.Clear();
                entrada.Sort((x, y) => x.FTotal.CompareTo(y.FTotal)); //Metodo sort que ordena lista de menor a mayor dependiendo del Ftotal
                
            }
            
            //salida se ordena de menor a mayor costo
            this.llegada = entrada[0];
        }
        public void printSalida()
        {
            //Printeo de lista salida 
            foreach (Nodo nodo in salida)
            {
                Console.WriteLine($"punto: {nodo.Pto.i},{nodo.Pto.j}");
            }
        }
        public void printSol()
        {
            List<Nodo> solucion = new List<Nodo>();
            Nodo? aux = llegada;
            while(aux!=null)
            {
                solucion.Add(aux);
                aux = aux.Padre;
            }
            foreach (Nodo nodo in solucion)
            {
                Console.WriteLine($"punto: {nodo.Pto.i},{nodo.Pto.j}");
            }
        }
    }
}
