namespace OneMaxProblem_H;

public static class TabuSearch
    {
        private static Random rnd = new Random();
        private static int[] solucion;
        private static int[] listaTabu;
        private static int[] nuevaSolucion;
        private static int bestVOActual;

        public static void Ejecutar(int n, int iteraciones)
        {
            int cont = 0;
            solucion = new int[n];
            listaTabu = new int[n];
            nuevaSolucion = new int[n];

            for (int i = 0; i < n; i++)
            {
                solucion[i] = rnd.Next(2); // 0 o 1 aleatoriamente
            }

            bestVOActual = Optimizacion.FuncionObjetivo(solucion);
            Array.Copy(solucion, nuevaSolucion, n);

            Console.Write("Solución Inicial: ");
            Optimizacion.Imprimir(solucion);
            Console.Write("Mejor VO: "+bestVOActual+"\n");

            while (cont < iteraciones)
            {
                int[] solucionTemp = GenerarVecina(n);
                int voVecina = Optimizacion.FuncionObjetivo(solucionTemp);

                if (voVecina > bestVOActual)
                {
                    bestVOActual = voVecina;
                    nuevaSolucion = solucionTemp.ToArray();
                    listaTabu[cont % n] = cont + 1; // Marca el movimiento como tabú
                }

                cont++;
            }

            Console.Write("Mejor solución encontrada: ");
            Optimizacion.Imprimir(nuevaSolucion);
            Console.WriteLine("Mejor VO: " + bestVOActual);
        }

        private static int[] GenerarVecina(int n)
        {
            int[] solVecina = nuevaSolucion.ToArray();
            int index = rnd.Next(n);

            // Evitar movimientos tabú
            if (listaTabu[index] == 0)
            {
                solVecina[index] = solVecina[index] == 1 ? 0 : 1; // Cambiar un bit
            }

            return solVecina;
        }
    }
