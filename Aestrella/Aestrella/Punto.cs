﻿using System;
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
    }
    internal class Nodo//Clase que se va a usar para guardar los puntos en las listas
    {
        private Punto pto;
        private int heuristica;
        private int fTotal;
        private int costoAcumulado;
        private Nodo? padre;
        public Punto Pto { get => pto; set => pto = value; }
        public int Heuristica { get => heuristica; set => heuristica = value; }
        public int FTotal { get => fTotal; set => fTotal = value; }
        public int CostoAcumulado { get => costoAcumulado; set => costoAcumulado = value; }
        internal Nodo Padre { get => padre; set => padre = value; }
        public Nodo(Nodo padre, Punto pto, int heuristica)
        {
            
            this.Pto = pto;//se ingresa manual
            this.Heuristica = heuristica;//se calcula en la clase
            this.Padre = padre;//Se ingresa manual
            this.FTotal = costoAcumulado+heuristica;
            this.CostoAcumulado = padre.costoAcumulado+1;//Costo del padre + el costo de movimiento padre a nodo ( en este caso es 1)
            
        }
        public Nodo()//Constructor del primer nodo
        {   

            this.Pto = new Punto();
            this.Heuristica = 0;
            this.FTotal = 0;
            this.CostoAcumulado = 0;
            this.Padre = null;
        }
    }
}
