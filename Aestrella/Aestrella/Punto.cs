using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aestrella
{
    internal struct Punto//Clase Puntos para facilitar trabajo con indice 2D
    {   //pensado como matriz donde
        //i=fila j=columna
        public short i;
        public short j;
        public Punto()
        {
            this.i = 0;
            this.j = 0;
        }

        public Punto(int i, int j)
        {
            this.i = (short)i;
            this.j = (short)j;
        }
        //Metodos usados por el diccionario para trabajar con indices
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
                  
            if(obj is Punto input)
            {
               if(input.i==this.i && input.j==this.j)
                    return true;
            }
            
            return false;

        }
        public override int GetHashCode()
        {
            return this.i.GetHashCode() ^ this.j.GetHashCode();//operacion hecha para que sean distintos los valores de cada elemento
                                                                // Es una operacion que trabaja con el valor binario de los enteros 
                                                                //Nos sirve porque entrega numeros distintos rapido
        }

    

   

    }
    internal class Nodo//Clase que se va a usar para guardar los puntos, se usa unsafe para usar punteros
    {
        private Punto pto;
        private float heuristica;
        private float fTotal;
        //heuristica y Ftotal son double por si llega a haber decimal con calculo de heuristica distinta a Manhattan
        private int costoAcumulado;
        public Nodo? padre;
        public Punto Pto { get => pto; set => pto = value; }
        public float Heuristica { get => heuristica; set => heuristica = value; }
        public float FTotal { get => fTotal; set => fTotal = value; }
        public int CostoAcumulado { get => costoAcumulado; set => costoAcumulado = value; }
        
        public Nodo(Nodo padre, Punto pto, float heuristica)//Constructor para nodos ingresados despues
        {
            
            this.pto = pto;//se ingresa manual
            this.heuristica = heuristica;//se calcula en la clase
            this.padre = padre;//Direccion a padre
            this.costoAcumulado = padre.CostoAcumulado + 1;//Costo del padre + el costo de movimiento padre a nodo ( en este caso es 1)
            this.fTotal = (float)costoAcumulado+heuristica;
            
            
        }
        public  Nodo(Punto p, float heuristica)//Constructor del primer nodo
        {   

            this.pto = p;
            this.heuristica = heuristica;
            this.fTotal = heuristica;
            this.costoAcumulado = 0;
            this.padre = null;
        }
        public Nodo()
        {

            this.pto = new Punto();
            this.heuristica = 0;
            this.fTotal = 0;
            this.costoAcumulado = 0;
            this.padre = null;
        }
}

}
