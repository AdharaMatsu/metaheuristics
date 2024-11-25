namespace Best_Improvement;

public static class BestImprov
{
    private static Random rnd = new Random();
    private static int i, j, n;
    public static void Run(string filename)
    {
        int[][] grafo;
        int[] solucionInicial;
        
        grafo = ReadInstances(filename);
        //print(grafo);
        
        solucionInicial = new int[n];
        for (i = 0; i < n; i++)
        {
            solucionInicial[i] = i;
        }
        Console.Write("Solucion Inicial: ");
        print(solucionInicial);
        Console.WriteLine("VO: "+ CalculateVO(solucionInicial,grafo));
        
        best_improvement(0,solucionInicial,grafo);
        
        
        
    }

    private static void best_improvement(int nodoInicio, int[] solucion, int[][]grafos)
    {
        int[] solucionCopy, best_Solucion = new int[n], newSolucion;
        int temp, vo=0, best_vo = int.MaxValue;
        
        newSolucion = Copy(solucion);
        for (i = 0; i < n-1; i++) // Nodo inicio
        {
            for (j = i+1; j < n; j++)
            {
                solucionCopy = Copy(newSolucion); // Crea copia de la solucion inicial

                #region Swap

                temp = solucionCopy[i];
                solucionCopy[i] = solucionCopy[j];
                solucionCopy[j] = temp;
                
                #endregion

                vo = CalculateVO(solucionCopy, grafos);
                if (vo < best_vo)
                {
                    best_vo = vo;
                    best_Solucion = Copy(solucionCopy);
                }
            }
            newSolucion = Copy(best_Solucion);
        }
        Console.Write("Best Solution: ");
        print(best_Solucion);
        Console.WriteLine("Best VO: "+best_vo);
    }
    
    private static int[] Copy(int[] array)
    {
        int[] copy = new int[n];
        for (int k = 0; k < n; k++)
        {
            copy[k] = array[k];
        }

        return copy;
    }
    private static int CalculateVO(int[] nodos, int[][] grafos)
    {
        int fo = 0;
        for (int k = 0; k < nodos.Length-1; k++)
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