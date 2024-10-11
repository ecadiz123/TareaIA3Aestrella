using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aestrella
{
    internal class Laberinto
    {   /*
         * Heuristica: el menor costo en el mejor de los casos, en nuestro caso va a ser cuando
         * no tienes obstaculos. Si heuristica es mayor a costo real, no es admisible.
         * Siempre debe ser menor igual a costo
         * */
        public int hManhattan( int x1,int y1,int x2,int y2)// Funcion que va a calcular heuristica de manhattan dado dos puntos, inicial y final (x1,y1) inicial-(x2,y2) final
        {
            int xval=Math.Abs(x1 - x2);
            int yval=Math.Abs(y1 - y2);
            return xval + yval;
        }
    }
}
