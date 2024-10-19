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
    }
}
