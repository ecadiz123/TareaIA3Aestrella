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



        public int Manhattan(Punto punto)
        {
            return Math.Abs(this.i - punto.i) + Math.Abs(this.j - punto.j);
        }

        public double ManhattanEscalado(Punto punto)
        {
            double distanciaManhattan = Math.Abs(this.i - punto.i) + Math.Abs(this.j - punto.j);
            return distanciaManhattan * 0.3;
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

        private direcciones_t? direccion;

        public Punto Pto
        { get => pto; set => pto = value; }
        public double Heuristica
        { get => heuristica; set => heuristica = value; }
        public double FTotal
        { get => fTotal; set => fTotal = value; }
        public int CostoAcumulado
        { get => costoAcumulado; set => costoAcumulado = value; }
        internal Nodo Padre
        { get => padre; set => padre = value; }
        internal direcciones_t? Direccion
        { get => direccion; set => direccion = value; }

        public Nodo(Nodo padre, Punto pto, double heuristica, direcciones_t direccion)//Constructor para nodos ingresados despues
        {
            
            this.pto = pto;//se ingresa manual
            this.heuristica = heuristica;//se calcula en la clase
            this.padre = padre;//Se ingresa manual
            this.direccion = direccion;
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
            this.direccion = null;
        }


    }

}
