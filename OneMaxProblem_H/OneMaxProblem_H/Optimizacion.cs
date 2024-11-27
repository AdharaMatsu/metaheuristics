namespace OneMaxProblem_H;

public static class Optimizacion
{
    // Función objetivo: contar los 1's en el vector
    public static int FuncionObjetivo(int[] vector)
    {
        return vector.Sum(); // La cantidad de unos en el vector
    }

    // Función para imprimir un vector
    public static void Imprimir(int[] vector)
    {
        foreach (var item in vector)
        {
            Console.Write(item + " ");
        }

        Console.WriteLine();
    }
}