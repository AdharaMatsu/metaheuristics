namespace IterLocalSearch_TSP;

public static class IterLocalSearch
{
    public static Random rnd = new Random();
    public static int i, j, n;
    public static void Run(string filename)
    {
        int[][] grafo;
        int[] solucionInicial, solucionVecina;
        int NodoInicial, VO, VO_Vecina,iteraciones = 1000; 
        
        grafo = Create(filename);
        
        n = grafo.Length;
//        Print(grafo);
        
        NodoInicial = rnd.Next(0, n);
        solucionInicial = SolucionInicial(NodoInicial);
        solucionVecina = new int[n];
        //Console.Write("Solucion Inicial: ");
        //Print(solucionInicial);
        VO = FuncionObjetivo(solucionInicial, grafo);
        //Console.WriteLine("Funcion Objetivo: " + VO+"\n");

        while (iteraciones > 0)
        {
            solucionVecina = SolucionVecina(solucionInicial);
            VO_Vecina = FuncionObjetivo(solucionVecina, grafo);
            
            if (VO_Vecina < VO)
            {
                VO = VO_Vecina;
            }
            
            solucionInicial = genera_vecina_perturbada(solucionVecina);
            iteraciones -= 1;
        }
        
        //Console.Write("Best Solucion:");
        //Print(solucionVecina);
        //Console.Write("Best VO:");
        Console.WriteLine(VO );
    }
    
    private static int[] genera_vecina_perturbada(int[] solucion)
    {
        int[] nueva_solucion = new int[solucion.Length];
        nueva_solucion = solucion.ToArray();
        
        int nodo_rnd_A = rnd.Next(1, solucion.Length); 
        int nodo_rnd_B = rnd.Next(1, solucion.Length);

        if (nodo_rnd_A > nodo_rnd_B)
        {
            int temp = nodo_rnd_A;
            nodo_rnd_A = nodo_rnd_B;
            nodo_rnd_B = temp;
        }

        while (nodo_rnd_A < nodo_rnd_B)
        {
            int temp = nueva_solucion[nodo_rnd_A];
            nueva_solucion[nodo_rnd_A] = nueva_solucion[nodo_rnd_B];
            nueva_solucion[nodo_rnd_B] = temp;
            nodo_rnd_A++;
            nodo_rnd_B--;
        }

        return nueva_solucion;
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