namespace OneMaxProblem_H;

public class Local_Search
{
        private static Random rnd = new Random();

        public static void Ejecutar(int n, int iteraciones)
        {
            int[] solucion = new int[n];
            for (int i = 0; i < n; i++)
            {
                solucion[i] = rnd.Next(2); // 0 o 1 aleatoriamente
            }

            int bestVOActual = Optimizacion.FuncionObjetivo(solucion);

            Console.Write("Solución Inicial: ");
            Optimizacion.Imprimir(solucion);
            Console.Write("Mejor VO: "+bestVOActual+"\n");

            for (int cont = 0; cont < iteraciones; cont++)
            {
                int[] solucionTemp = GenerarVecina(solucion);
                int voVecina = Optimizacion.FuncionObjetivo(solucionTemp);

                if (voVecina > bestVOActual)
                {
                    solucion = solucionTemp.ToArray();
                    bestVOActual = voVecina;
                }
            }

            Console.Write("Mejor solución encontrada: ");
            Optimizacion.Imprimir(solucion);
            Console.WriteLine("Mejor VO: " + bestVOActual);
        }

        private static int[] GenerarVecina(int[] solucion)
        {
            int[] nuevaSolucion = solucion.ToArray();
            int index = rnd.Next(solucion.Length);
            nuevaSolucion[index] = nuevaSolucion[index] == 1 ? 0 : 1; // Cambiar un bit
            return nuevaSolucion;
        }
    
}
