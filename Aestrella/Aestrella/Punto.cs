using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Punto(int i, int j)
        {
            this.i = i;
            this.j = j;
        }
        //Metodos usados por el diccionario para trabajar con indices
        public override bool Equals(object? obj)
        {   
            var input= obj as Punto;//Se va a tomar obj como un punto mediane la variable input
            if(input.i==this.i && input.j==this.j)
                return true;
            else
                return false;

        }
        public override int GetHashCode()
        {
            return this.i.GetHashCode() ^ this.j.GetHashCode();//operacion hecha para que sean distintos los valores de cada elemento
                                                                // Es una operacion que trabaja con el valor binario de los enteros 
                                                                //Nos sirve porque entrega numeros distintos rapido
        }

    

    public int Manhattan(Punto punto)
        {
            return Math.Abs(this.i - punto.i) + Math.Abs(this.j - punto.j);
        }
        public double heuristicaAlt(Punto punto)
        {
            //return Math.Sqrt(Math.Pow((this.i-punto.i), 2)+ Math.Pow((this.j - punto.j), 2));
            return 0.3*Manhattan(punto);
        }

    }
    internal class Nodo//Clase que se va a usar para guardar los puntos en las listas
    {
        private Punto pto;
        private double heuristica;
        private double fTotal;
        //heuristica y Ftotal son double por si llega a haber decimal con calculo de heuristica distinta a Manhattan
        private int costoAcumulado;
        private Nodo? padre;
        public Punto Pto { get => pto; set => pto = value; }
        public double Heuristica { get => heuristica; set => heuristica = value; }
        public double FTotal { get => fTotal; set => fTotal = value; }
        public int CostoAcumulado { get => costoAcumulado; set => costoAcumulado = value; }
        internal Nodo Padre { get => padre; set => padre = value; }
        public Nodo(Nodo padre, Punto pto, double heuristica)//Constructor para nodos ingresados despues
        {
            
            this.pto = pto;//se ingresa manual
            this.heuristica = heuristica;//se calcula en la clase
            this.padre = padre;//Se ingresa manual
            this.costoAcumulado = padre.costoAcumulado + 1;//Costo del padre + el costo de movimiento padre a nodo ( en este caso es 1)
            this.fTotal = Convert.ToDouble(costoAcumulado)+heuristica;
            
            
        }
        public Nodo(Punto p, double heuristica)//Constructor del primer nodo
        {   

            this.pto = p;
            this.heuristica = heuristica;
            this.fTotal = heuristica;
            this.costoAcumulado = 0;
            this.padre = null;
        }
        public Nodo()//Inicializacion de nodo vacio por si acaso se necesita
        {
            this.padre = null;
            this.pto = new Punto();
            this.heuristica = 0;
            this.costoAcumulado= 0;
            this.fTotal = 0;
            
           }

    }

}
