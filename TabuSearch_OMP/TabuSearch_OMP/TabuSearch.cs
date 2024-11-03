namespace TabuSearch_OMP;

public static class TabuSearch
{
    private static int TiemposTabu = 4;
    private static int i, j, index, best_VOactual;
    
    private static Random rnd;
    
    private static int[] solucion;
    private static int[] ListaTabu;
    private static int[] nueva_solucion;

    public static void busqueda_tabu(int n, int iteraciones)
    {
        int cont = 0, VO_Vecina;
        solucion = new int[n];//{0,0,1,0,1,0};
        ListaTabu = new int[n];
        nueva_solucion = new int[n];
        
        rnd = new Random();
        
        for (i = 0; i < n; i++)
        {
            solucion[i] = rnd.Next(2); //0 , 1
        }
        
        best_VOactual = funcion_objetivo(solucion);

        #region Crea copia de la solucion inicial

        for (i = 0; i < n; i++)
        {
            nueva_solucion[i] = solucion[i];
        }

        #endregion
        Console.Write("Solucion Inicial: ");
        print(solucion);
        Console.WriteLine();
        
        
        while (cont < iteraciones)
        {
            int[] solucion_actual = generarVecina(n);
            
            VO_Vecina = funcion_objetivo(solucion_actual);
            if (VO_Vecina == n)
            {
                nueva_solucion = solucion_actual.ToArray();
                Console.WriteLine("Stop in iteration:  "+cont+"  "+ iteraciones+"\n");
                break;
            }
            if (VO_Vecina > best_VOactual)
            {
                best_VOactual = VO_Vecina;
                nueva_solucion = solucion_actual;
                ListaTabu[index] = TiemposTabu+1;
            }
            else
            {
                for (i = 0; i < ListaTabu.Length; i++)
                {
                    if (ListaTabu[i] != 0)
                    {
                        ListaTabu[i] -= 1;
                    }
                }
            }
            cont++;
        }
        Console.Write("Nueva solucion: ");
        print(nueva_solucion);
        Console.WriteLine();
        
    }
    private static void print(int[] lista)
    {
        for (i = 0; i < lista.Length; i++)
        {
            Console.Write(lista[i]+" ");
        }
        
    }
    private static int funcion_objetivo(int[] vector)
    {
        var vo = 0;
        for (i = 0; i < vector.Length; i++)
        {
            vo+=vector[i];
        }
        return vo;
    }
    
    private static int[] generarVecina(int n)
    {
        int[] solVecina = new int[n];
        
        searchInVector(n);
        
        Console.Write("Lista Tabu: ");
        print(ListaTabu);
        Console.WriteLine();
        
        int newValue = 1;
        for (i = 0; i < n; i++)
        {
            solVecina[i] = nueva_solucion[i];
        }
        solVecina[index] = newValue;
        
        Console.Write("Nueva Solucion: ");
        print(solVecina);
        Console.WriteLine("\n");
        return solVecina;
    }
    
    private static void searchInVector(int n)
    {
        index = rnd.Next(n);
        if (ListaTabu[index] == 0)
        {
            ListaTabu[index] = index;
        }
        else
        {
            searchInVector(n);
        }
    }
}