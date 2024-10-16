using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aestrella
{
    internal class Punto//Clase Puntos para facilitar trabajo con indice 2D
    {	//pensado como matriz donde
	//i=fila j=columna
	public int i;
	public int j;
    }
    internal class Laberinto
    {   /*
         * Heuristica: el menor costo en el mejor de los casos, en nuestro caso va a ser cuando
         * no tienes obstaculos. Si heuristica es mayor a costo real, no es admisible.
         * Siempre debe ser menor igual a costo
         * */
	char[][] laberinto;
	Punto Inicio;
	Punto Final;
	Punto Actual;
	Laberinto(String path)//constructor donde se le entrega solo el path del archivo de entrada
	{
	    if (File.exists(path.exists(path))==true)//si existe el archivo
	    {
		StreamReader aux=new StreamReader(path);
		string auxstring = aux.ReadLine();
		char[] charaux= auxstring.ToCharArray();
		int size= auxchar.Size();
		//como laberintos van a ser cuadrados se usa largode primera linea
		//el arreglo es de tipo escalonado o jagged, donde la cantidad de filas
		//es estatica, pero las columnas son dinamicas, por eso solo se declara las 
		//filas como tamaño
		char[][] auxlab= new char[size][];
		auxlab[0]= charaux;
		for(int i=1;i<size;i++)
		{
		    auxstring = aux.Readline();
		    charaux= auxstring.ToCharArray();
		    auxlab[i] = charaux;
		}
		aux.Close();



	    }

	}
    }
}
