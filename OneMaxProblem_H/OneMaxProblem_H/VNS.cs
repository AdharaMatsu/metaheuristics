namespace OneMaxProblem_H;

    public static class VNSBusqueda
    {
        private static Random rnd = new Random();
        private static int[] solucion;
        private static int[] nuevaSolucion;
        private static int bestVOActual;

        public static void Ejecutar(int n, int iteraciones)
        {
            int cont = 0;
            solucion = new int[n];
            for (int i = 0; i < n; i++)
            {
                solucion[i] = rnd.Next(2); // 0 o 1 aleatoriamente
            }

            bestVOActual = Optimizacion.FuncionObjetivo(solucion);
            nuevaSolucion = solucion.ToArray();

            Console.Write("Solución Inicial: ");
            Optimizacion.Imprimir(solucion);
            Console.WriteLine("Mejor VO: " + bestVOActual);

            while (cont < iteraciones)
            {
                bool mejoraEncontrada = false;
                for (int i = 0; i < 3; i++) // Tres vecindarios: Swap, Scramble, Move
                {
                    int[] solucionTemp = nuevaSolucion.ToArray();
                    solucionTemp = GenerarVecina(i, solucionTemp);
                    solucionTemp = BusquedaLocal(solucionTemp);

                    int VO_Vecina = Optimizacion.FuncionObjetivo(solucionTemp);
                    if (VO_Vecina > bestVOActual)
                    {
                        bestVOActual = VO_Vecina;
                        nuevaSolucion = solucionTemp.ToArray();
                        mejoraEncontrada = true;
                        break;
                    }
                }

                if (!mejoraEncontrada)
                {
                    nuevaSolucion = solucion.ToArray(); // Reiniciar con la mejor solución
                }

                cont++;
            }

            Console.Write("Mejor solución encontrada: ");
            Optimizacion.Imprimir(nuevaSolucion);
            Console.WriteLine("Mejor VO: " + bestVOActual);
        }

        private static int[] GenerarVecina(int vecindario, int[] solucion)
        {
            int[] solVecina = solucion.ToArray();
            switch (vecindario)
            {
                case 0: Swap(solVecina); break;
                case 1: Scramble(solVecina); break;
                case 2: Move(solVecina); break;
            }
            return solVecina;
        }

        private static void Swap(int[] solVecina)
        {
            int i = rnd.Next(solVecina.Length);
            int j = rnd.Next(solVecina.Length);
            int temp = solVecina[i];
            solVecina[i] = solVecina[j];
            solVecina[j] = temp;
        }

        private static void Scramble(int[] solVecina)
        {
            int start = rnd.Next(solVecina.Length);
            int length = rnd.Next(1, solVecina.Length - start);
            var subsequence = solVecina.Skip(start).Take(length).ToArray();
            subsequence = subsequence.OrderBy(x => rnd.Next()).ToArray();
            subsequence.CopyTo(solVecina, start);
        }

        private static void Move(int[] solVecina)
        {
            int i = rnd.Next(solVecina.Length);
            solVecina[i] = solVecina[i] == 1 ? 0 : 1;
        }

        private static int[] BusquedaLocal(int[] solucion)
        {
            return solucion; // En este ejemplo, la búsqueda local no realiza cambios adicionales
        }
    
}
