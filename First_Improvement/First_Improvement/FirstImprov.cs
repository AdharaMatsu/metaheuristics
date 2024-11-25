namespace First_Improvement;

public static class FirstImprov
{
    private static Random rnd = new Random();
    private static int i, j, n;

    public static void Run(string filename)
    {
        int[][] grafo;
        int[] solucionInicial, solucionInicial_rnd;
        int vo;
        grafo = ReadInstances(filename);
        //print(grafo);

        solucionInicial = new int[n];
        solucionInicial_rnd = new int[n];
        for (i = 0; i < n; i++)
        {
            solucionInicial[i] = i;
        }

        print(solucionInicial);
        solucionInicial_rnd = random_positions(solucionInicial);
        Console.Write("Solucion Inicial: ");
        print(solucionInicial_rnd);
        vo = CalculateVO(solucionInicial_rnd, grafo);
        Console.WriteLine("VO: " + vo);

        first_improvement(solucionInicial_rnd, grafo, vo);
    }

    private static int[] random_positions(int[] array)
    {
        int values = array.Sum(), k = 0;
        int[] new_solution = new int[n];
        while (values > -5)
        {
            int position = rnd.Next(0, array.Length);
            if (array[position] != -1)
            {
                new_solution[k] = array[position];
                array[position] = -1;
                k++;
            }

            values = array.Sum();
        }

        return new_solution;
    }

    private static void first_improvement(int[] solucion, int[][] grafos, int VO_actual)
    {
        int[] solucionCopy, best_Solucion = new int[n], newSolucion;
        int it = 0, jt, vo, best_vo = int.MaxValue;
        bool state_bestSolution = false, state_lasted = false;
        List<int> solutions;
        solutions = CopyArray(solucion);
        
        for (jt = 1; jt < n; jt++)
        {
            int value = solutions[jt];
            solutions.RemoveAt(jt);
            solutions.Insert(it, value);
            
            vo = CalculateVO(solutions, grafos);
            if (vo < best_vo)
            {
                if (state_bestSolution == false)
                {
                     state_bestSolution = true;                               
                }
                else
                {
                    state_bestSolution = false;
                    jt -= 1;
                }
                best_vo = vo;
                best_Solucion = Copy(solutions);
                it += 1; 
                
            }

            if (jt == n - 1 && state_lasted == false)
            {
                it += 1;
                jt = jt - 1;
                solutions = CopyArray(best_Solucion);
                state_lasted = true;
            }
        } 
        Console.Write("Best Solution: ");
        print(best_Solucion);
        Console.WriteLine("Best VO: "+best_vo);
    }

    private static List<int> CopyArray(int[] array)
    {
        List<int> copy = new List<int>();
        for (int k = 0; k < n; k++)
        {
            copy.Add(array[k]);
        }

        return copy;
    }
    private static int[] Copy(List<int> array)
    {
        int[] copy = new int[n];
        for (int k = 0; k < n; k++)
        {
            copy[k] = array[k];
        }

        return copy;
    }

    private static int CalculateVO(List<int> nodos, int[][] grafos)
    {
        int fo = 0;
        for (int k = 0; k < nodos.Count - 1; k++)
        {
            fo = fo + grafos[nodos[k]][nodos[k + 1]];
        }

        return fo;
    }
    private static int CalculateVO(int[] nodos, int[][] grafos)
    {
        int fo = 0;
        for (int k = 0; k < nodos.Length - 1; k++)
        {
            fo = fo + grafos[nodos[k]][nodos[k + 1]];
        }

        return fo;
    }

    private static int[][] ReadInstances(string filename)
    {
        StreamReader reader = new StreamReader(filename);

        int[][] grafo;

        string linea;
        string[] columnas;

        n = Convert.ToInt32(reader.ReadLine());

        #region Leer la instancia

        grafo = new int[n][];
        for (i = 0; i < n; i++)
        {
            grafo[i] = new int[n];
        }

        for (i = 0; i < n; i++)
        {
            linea = reader.ReadLine() ?? string.Empty;
            columnas = linea.Split(',');
            for (j = 0; j < n; j++)
            {
                grafo[i][j] = Convert.ToInt32(columnas[j]);
            }
        }

        reader.Close();

        #endregion

        return grafo;
    }

    private static void print(int[][] graph)
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

    private static void print(int[] solucion)
    {
        for (j = 0; j < solucion.Length; j++)
        {
            Console.Write(solucion[j] + " ");
        }

        Console.WriteLine();
    }
}