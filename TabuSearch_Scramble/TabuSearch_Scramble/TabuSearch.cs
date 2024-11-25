using System.IO.Enumeration;
namespace TabuSearch_Scramble;

public static class TabuSearch
{
    public static Random rnd = new Random();
    public static int i, j, n, index, best_VOactual, tamañoTabu = 4;
    private static int[] nueva_solucion;
    private static List<(int, int)> ListaTabu = new List<(int, int)>();

    public static void Run(string filename)
    {
        int[][] grafo;
        int[] solucionInicial;
        int NodoInicial, FO, iteraciones = 100, VO_Vecina;

        grafo = Create(filename);

        n = grafo.Length;

        NodoInicial = rnd.Next(0, n);
        solucionInicial = SolucionInicialExp(NodoInicial);

        nueva_solucion = new int[n];
        best_VOactual = FuncionObjetivo(solucionInicial, grafo);

        #region Crea copia de la solucion inicial

        for (i = 0; i < n; i++)
        {
            nueva_solucion[i] = solucionInicial[i];
        }

        #endregion

        Console.Write("Solucion Inicial: ");
        Print(solucionInicial);
        FO = FuncionObjetivo(solucionInicial, grafo);
        Console.WriteLine("Funcion Objetivo: " + FO + "\n");

        while (iteraciones > 0)
        {
            int[] solucion_actual = generarVecina(solucionInicial);
            VO_Vecina = FuncionObjetivo(solucion_actual, grafo);

            if (VO_Vecina == n)
            {
                nueva_solucion = solucion_actual.ToArray();
                Console.WriteLine("Stop in iteration:  " + (100 - iteraciones) + "/100\n");
                break;
            }
            if (VO_Vecina > best_VOactual)
            {
                best_VOactual = VO_Vecina;
                nueva_solucion = solucion_actual;
            }

            iteraciones--;
        }
        Console.Write("Nueva solucion: ");
        Print(nueva_solucion);
        Console.Write("VO: " + best_VOactual);
    }

    private static int[] generarVecina(int[] solucion)
    {
        int[] solVecina = (int[])solucion.Clone();
        Random rnd = new Random();

        int opcion = rnd.Next(0, 2); // Decidir entre intercambio o scramble (0 = intercambio, 1 = scramble)

        if (opcion == 0)
        {
            // Operación de intercambio (swap)
            int ciudadA = rnd.Next(1, solucion.Length); // Excluye el primer nodo (punto de inicio)
            int ciudadB = rnd.Next(1, solucion.Length);

            // Asegurarse de que los nodos sean diferentes y no estén en la lista tabú
            while (ciudadA == ciudadB ||
                   ListaTabu.Contains((ciudadA, ciudadB)) ||
                   ListaTabu.Contains((ciudadB, ciudadA)))
            {
                ciudadA = rnd.Next(1, solucion.Length);
                ciudadB = rnd.Next(1, solucion.Length);
            }

            // Intercambiar las ciudades en la ruta
            int temp = solVecina[ciudadA];
            solVecina[ciudadA] = solVecina[ciudadB];
            solVecina[ciudadB] = temp;

            // Agregar el par de ciudades intercambiado a la lista tabú
            ListaTabu.Add((ciudadA, ciudadB));
            if (ListaTabu.Count > tamañoTabu) // Limitar el tamaño de la lista tabú
            {
                ListaTabu.RemoveAt(0);
            }
        }
        else
        {
            // Operación Scramble
            int inicio = rnd.Next(1, solucion.Length - 1);
            int fin = rnd.Next(inicio + 1, solucion.Length);

            // Extraer y mezclar la subsecuencia
            int[] subsecuencia = solVecina[inicio..fin];
            subsecuencia = subsecuencia.OrderBy(x => rnd.Next()).ToArray();

            // Reinsertar la subsecuencia mezclada
            Array.Copy(subsecuencia, 0, solVecina, inicio, subsecuencia.Length);

            // Agregar el rango modificado a la lista tabú
            ListaTabu.Add((inicio, fin));
            if (ListaTabu.Count > tamañoTabu)
            {
                ListaTabu.RemoveAt(0);
            }
        }

        return solVecina;
    }

    private static int FuncionObjetivo(int[] nodos, int[][] grafos)
    {
        int fo = 0;
        for (i = 0; i < nodos.Length - 1; i++)
        {
            fo = fo + grafos[nodos[i]][nodos[i + 1]];
        }

        return fo;
    }

    private static int[] SolucionInicialExp(int nodo)
    {
        int[] solucion = new int[n];
        solucion[0] = nodo;

        for (i = 0; i < n; i++)
        {
            if (i != nodo)
            {
                int index = rnd.Next(1, n);
                solucion[index] = i;
            }
        }

        return solucion;
    }

    private static int[][] Create(string filename)
    {
        StreamReader sr = new StreamReader("coordenadas.csv");

        int n;
        int[][] grafo;

        string linea;
        string[] columnas;

        n = Convert.ToInt32(sr.ReadLine());

        var distancias = new List<Tuple<int, int>>();

        for (i = 0; i < n; i++)
        {
            linea = sr.ReadLine() ?? string.Empty;
            columnas = linea.Split(",");
            distancias.Add(new Tuple<int, int>(Convert.ToInt32(columnas[0]), Convert.ToInt32(columnas[1])));
        }

        #region inicializar el grafo
        grafo = new int[n][];

        for (i = 0; i < n; i++)
        {
            grafo[i] = new int[n];
        }
        for (i = 0; i < n; i++)
        {
            for (j = i + 1; j < n; j++)
            {
                grafo[i][j] = Euclidean(distancias[i].Item1, distancias[i].Item2, distancias[j].Item1, distancias[j].Item2);
                grafo[j][i] = Euclidean(distancias[i].Item1, distancias[i].Item2, distancias[j].Item1, distancias[j].Item2);
            }
        }

        #endregion

        sr.Close();
        return grafo;
    }

    private static void Print(int[][] graph)
    {
        for (i = 0; i < graph.Length; i++)
        {
            for (j = 0; j < graph[i].Length; j++)
            {
                Console.Write(graph[i][j] + " ");
            }
            Console.WriteLine();
        }
    }

    private static void Print(int[] solucion)
    {
        for (j = 0; j < solucion.Length; j++)
        {
            Console.Write(solucion[j] + " ");
        }
        Console.WriteLine();
    }

    private static int Euclidean(int x1, int y1, int x2, int y2)
    {
        var eucDistance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        return (int)eucDistance;
    }
}
