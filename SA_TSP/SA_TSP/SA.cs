namespace SA_TSP;

public static class SA
{
    private static Random rnd = new Random();
    private static int i, j, n, vo_mejor_actual;
    public static void Run(string filename)
    {
        int[][] grafo;
        int[] solucionInicial, solucionVecina, best_solucion;
        int NodoInicial, VO, VO_Vecina, iteraciones = 80, ejecuciones_RC = 50;
        double TemperaturaInicial = 100,alfa = 0.95;
        int it = 0, best_vo = -1;
        grafo = Create(filename);
        
        n = grafo.Length;
        //print(grafo);
        
        NodoInicial = rnd.Next(0, n);
        solucionInicial = SolucionInicial(NodoInicial);
        print(solucionInicial);
        Console.WriteLine("Funcion Objetivo: " + (FuncionObjetivo(solucionInicial, grafo))+"\n");
        solucionVecina = new int[n];
        best_solucion = new int[n];
        
        while (it < ejecuciones_RC && TemperaturaInicial > 0)
        {
            recocido(solucionInicial, solucionVecina,iteraciones,grafo, TemperaturaInicial);
            if (best_vo < vo_mejor_actual)
            {
                for (i = 0; i < n; i++)
                {
                    best_solucion[i]=solucionInicial[i];
                }
                best_vo = vo_mejor_actual;
            }
            TemperaturaInicial = TemperaturaInicial * alfa;
            
            it++;
        }
        
        Console.WriteLine("Best Solucion: ");
        print(best_solucion);
        Console.Write("Best VO: "+best_vo);
        


    }

    private static void recocido(int[] solucion,int[] solucion_vecina, int max_iteraciones, int[][]grafo, double temp_inicial)
    {
        int cont = 0, vo_vecina;
        vo_mejor_actual = FuncionObjetivo(solucion, grafo);
        while (cont < max_iteraciones)
        {
            solucion_vecina = SolucionVecina(solucion);
            vo_vecina = FuncionObjetivo(solucion_vecina,grafo);
            double deltaF = vo_vecina - vo_mejor_actual;

            if (vo_vecina > vo_mejor_actual)
            {
                for (i = 0; i < solucion.Length; i++)
                {
                    solucion[i] = solucion_vecina[i];
                }

                vo_mejor_actual = vo_vecina;
            }
            else
            {
                double t = rnd.NextDouble();
                double val = Math.Pow(Math.E,deltaF/temp_inicial);
                if (t < val)
                {
                    for (j = 0; j < solucion.Length; j++)
                    {
                        solucion[j] = solucion_vecina[j];
                    }
                    vo_mejor_actual = vo_vecina;
                }
            }
            cont++;
        }
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
            for (j = i+1; j < n; j++)
            {
                grafo[i][j] = Euclidean(distancias[i].Item1,distancias[i].Item2,distancias[j].Item1,distancias[j].Item2);
                grafo[j][i] = Euclidean(distancias[i].Item1,distancias[i].Item2,distancias[j].Item1,distancias[j].Item2);
            }
        }

        #endregion
        
        sr.Close();
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
    private static int Euclidean(int x1,int y1,int x2,int y2)
    {
        var eucDistance = Math.Sqrt(Math.Pow(x2-x1,2) + Math.Pow(y2-y1,2));
        return (int)eucDistance;
    }
    
    private static int[] SolucionInicial(int nodo)
    {
        int [] solucion = new int[n];
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
    
    private static int[] SolucionVecina(int[] solucion)
    {
        List<int> nodos = new List<int>();

        for (i = 1; i < solucion.Length; i++)
        {
            nodos.Add(i);
        }
        
        int index = rnd.Next(nodos.Count);   
        int rndNodoA = nodos[index];          
        nodos.RemoveAt(index);               

        int rndNodoB = nodos[rnd.Next(nodos.Count)];           
        
        int temp = solucion[rndNodoA];
        solucion[rndNodoA] = solucion[rndNodoB];
        solucion[rndNodoB] = temp;

        return solucion;
    }
    private static int FuncionObjetivo(int[] nodos, int[][] grafos)
    {
        int fo = 0;
        for (i = 0; i < nodos.Length-1; i++)
        {
            fo = fo + grafos[nodos[i]][nodos[i + 1]];
        }

        return fo;
    }
}