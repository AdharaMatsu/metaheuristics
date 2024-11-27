namespace VNS;

public static class OMP
{
    private static Random rnd;
    private static int[] solucion;
    private static int[] nueva_solucion;
    private static int best_VOactual;

    // Función principal para realizar la búsqueda VNS
    public static void VNSBusqueda(int n, int iteraciones)
    {
        int cont = 0;
        rnd = new Random();

        // Inicializar la solución aleatoria
        solucion = new int[n];
        for (int i = 0; i < n; i++)
        {
            solucion[i] = rnd.Next(2); // 0 o 1 aleatoriamente
        }

        // Evaluamos la función objetivo de la solución inicial
        best_VOactual = funcion_objetivo(solucion);
    
        // Copiar la solución inicial
        nueva_solucion = solucion.ToArray();

        Console.Write("Solución Inicial: ");
        print(solucion);
        Console.Write("Mejor VO: "+best_VOactual);
        Console.WriteLine();

        while (cont < iteraciones)
        {
            bool mejoraEncontrada = false;

            // Iterar sobre los vecindarios
            for (int i = 0; i < 3; i++) // Tres vecindarios: Swap, Scramble, Move
            {
                int[] solucion_temp = nueva_solucion.ToArray();
                solucion_temp = generaVecina(i, solucion_temp); // Generar una vecina
                solucion_temp = BusquedaLocal(solucion_temp); // Realizar la búsqueda local

                int VO_Vecina = funcion_objetivo(solucion_temp);
                if (VO_Vecina > best_VOactual) // Si encontramos una mejora
                {
                    best_VOactual = VO_Vecina;
                    nueva_solucion = solucion_temp.ToArray();
                    mejoraEncontrada = true;
                    break; // Salir si encontramos una mejora
                }
            }

            // Si no hubo mejora, reiniciar con la mejor solución conocida
            if (!mejoraEncontrada)
            {
                nueva_solucion = solucion.ToArray(); // Reiniciar con la mejor solución encontrada
            }

            cont++;
        }

        Console.Write("Mejor solución encontrada: ");
        print(nueva_solucion);
        Console.Write("Mejor VO: "+best_VOactual);
        Console.WriteLine();
    }

    // Función para imprimir un vector
    private static void print(int[] lista)
    {
        foreach (var item in lista)
        {
            Console.Write(item + " ");
        }

        Console.WriteLine();
    }

    // Función objetivo: contar los 1's en el vector (One Max Problem)
    private static int funcion_objetivo(int[] vector)
    {
        return vector.Sum(); // La cantidad de unos en el vector
    }

    // Generar una vecina según el vecindario (Swap, Scramble, Move)
    private static int[] generaVecina(int vecindario, int[] solucion)
    {
        int[] solVecina = solucion.ToArray();
        switch (vecindario)
        {
            case 0: // Vecindario Swap: Cambiar dos bits
                Swap(solVecina);
                break;
            case 1: // Vecindario Scramble: Reordenar una subsecuencia
                Scramble(solVecina);
                break;
            case 2: // Vecindario Move: Cambiar un bit
                Move(solVecina);
                break;
        }

        return solVecina;
    }

    // Operación Swap: Cambiar dos bits aleatorios
    private static void Swap(int[] solVecina)
    {
        int i = rnd.Next(solVecina.Length);
        int j = rnd.Next(solVecina.Length);
        int temp = solVecina[i];
        solVecina[i] = solVecina[j];
        solVecina[j] = temp;
    }

    // Operación Scramble: Reordenar aleatoriamente una subsecuencia del vector
    private static void Scramble(int[] solVecina)
    {
        int start = rnd.Next(solVecina.Length);
        int length = rnd.Next(1, solVecina.Length - start);
        var subsequence = solVecina.Skip(start).Take(length).ToArray();
        subsequence = subsequence.OrderBy(x => rnd.Next()).ToArray();
        subsequence.CopyTo(solVecina, start);
    }

    // Operación Move: Cambiar el valor de un solo bit
    private static void Move(int[] solVecina)
    {
        int i = rnd.Next(solVecina.Length);
        solVecina[i] = solVecina[i] == 1 ? 0 : 1; // Cambiar de 0 a 1 o de 1 a 0
    }

    // Búsqueda local: Asegurarse de que la vecina sea la mejor posible
    private static int[] BusquedaLocal(int[] solucion)
    {
        // En este caso, la búsqueda local es simple: siempre intentamos mejorar la solución
        return solucion;
    }

    // Ejemplo de ejecución
    public static void Main(string[] args)
    {
        int n = 10; // Longitud del vector binario
        int iteraciones = 1000; // Número de iteraciones
        VNSBusqueda(n, iteraciones); // Ejecutar el algoritmo VNS
    }
}