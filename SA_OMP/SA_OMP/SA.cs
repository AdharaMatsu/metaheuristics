namespace SA_OMP;

public class SA {

    private Random rnd;
    private int[] solucion_mejor_actual;
    
    private int vo_mejor_actual;

    private double TempInicial;

    public SA(int n, double TempInicial){
        int i;

        this.TempInicial = TempInicial; //guarda la temperatura inicial
        solucion_mejor_actual = new int[n];

        //int []optima_local;

        rnd = new Random(5);

        ///generacion de la solucional inicial  - aleatoria
        for (i = 0; i < n; i++)
        {
            solucion_mejor_actual[i] = rnd.Next(2); //0 , 1
        }
        
    }

    public void run(int tot_ejecuciones_RC, int tot_make_recocido, double alfa) {
                
        int [] best_solucion = new int[solucion_mejor_actual.Length];
        int best_vo = -1;

        int it = 0;
        while(it<tot_ejecuciones_RC && TempInicial>0) { //Ejemplo: 10 ejecuciones de busqueda local iterada

            Console.WriteLine("\n\nRECOCIDO SIMULADO #" + (it+1).ToString()+ "\n");

            make_recocido(solucion_mejor_actual, tot_make_recocido); //Ejemplo: 80 veces a repetir la busqueda local

            if (best_vo<vo_mejor_actual) { //si la solucion obtenida en busqueda local es mejor que la anterior
                // , entonces me quedo con esa solucion...
                for (int j = 0; j < solucion_mejor_actual.Length; j++) {
                    best_solucion[j] = solucion_mejor_actual[j];
                }
                best_vo = vo_mejor_actual;
            }

            TempInicial = TempInicial *  alfa; //actualiza la temperatura 

            Console.WriteLine("\n\n");
            it++;
        }


        ////////////////////////////////////////////////////////////
                //IMPRIMIR LA MEJOR(best) SOLUCION....
                Console.WriteLine("Best Solucion por Recocido Simulado:");
                imprimir(best_solucion);
                Console.WriteLine("Best VO:");                
                Console.WriteLine(best_vo);
                Console.WriteLine("Temp. Restante: " + TempInicial + "\n\n");
                ////////////////////////////////////////////////////////////

    }

    public void make_recocido(int[] solucion, int max_iteraciones)
    {

        int cont = 0;
        vo_mejor_actual = funcion_objetivo(solucion);
        int[] solucion_vecina;
        int vo_vecina;
        while (cont < max_iteraciones)
        {
            solucion_vecina = genera_vecina(solucion);

            vo_vecina = funcion_objetivo(solucion_vecina);

            double deltaF = vo_vecina-vo_mejor_actual;

            if (vo_vecina > vo_mejor_actual)
            {
                for (int i = 0; i < solucion.Length; i++)
                {
                    solucion[i] = solucion_vecina[i];
                }
                vo_mejor_actual = vo_vecina;
                ////////////////////////////////////////////////////////////
                //IMPRIMIR LA MEJOR SOLUCION ACTUAL....
                Console.WriteLine("Mejor Solucion Actual:");
                imprimir(solucion);
                Console.WriteLine("Mejor VO Actual:");
                Console.WriteLine(vo_mejor_actual + "\n\n");
                ////////////////////////////////////////////////////////////
            }else{
                double t = rnd.NextDouble();
                double val = Math.Pow(Math.E,deltaF/TempInicial);
                if (t < val){
                    for (int i = 0; i < solucion.Length; i++)
                    {
                        solucion[i] = solucion_vecina[i];
                    }
                    vo_mejor_actual = vo_vecina;
                    }

            }

            cont++; 
        }

                ////////////////////////////////////////////////////////////
                //IMPRIMIR LA MEJOR SOLUCION....                
                Console.WriteLine("Mejor Solucion Encontrada por Recocido:");
                imprimir(solucion);
                Console.WriteLine("Mejor VO Encontrado:");
                Console.WriteLine(vo_mejor_actual);
                ////////////////////////////////////////////////////////////

    }

    public void imprimir(int[] vector){
        for (int i = 0;i<vector.Length; i++){
            Console.Write(vector[i] + "\t");
        }
        Console.WriteLine();
    }

    public int funcion_objetivo(int[] vector)
    {
        int vo = 0;  //valor objetivo  --- fitness
        for (int i = 0; i < vector.Length; i++)
        {
            vo += vector[i];
        }
        return vo;
    }

    public int[] genera_vecina(int[] solucion)
    {
        int[] nueva_solucion = new int[solucion.Length];
        //copiar la original en la vecina
        for (int i = 0; i < solucion.Length; i++)
        {
            nueva_solucion[i] = solucion[i]; //
        }

        int index_gen_a_mutar = rnd.Next(solucion.Length);

        //mutacion uniforme de un gen
        if (rnd.Next(100) > 50)
        {
            nueva_solucion[index_gen_a_mutar] = 1;
        }
        else
        {
            nueva_solucion[index_gen_a_mutar] = 0;
        }

        return nueva_solucion;
    }

    public int[] genera_vecina_perturbada(int[] solucion)
    {
        int[] nueva_solucion = new int[solucion.Length];
        //copiar la original en la vecina
        for (int i = 0; i < solucion.Length; i++)
        {
            nueva_solucion[i] = solucion[i]; //
        }

        //mutacion uniforme de la solucion
        for (int i = 0; i < solucion.Length; i++)
        {
            if (rnd.Next(100) > 50)
            {
                nueva_solucion[i] = 1;
            }
            else
            {
                nueva_solucion[i] = 0;
            }
        }

        return nueva_solucion;
    }


}