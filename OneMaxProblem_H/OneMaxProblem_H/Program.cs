using OneMaxProblem_H;

int n = 10; // Longitud del vector binario
int iteraciones = 1000; // Número de iteraciones

Console.WriteLine("Ejecutando VNS...");
VNSBusqueda.Ejecutar(n, iteraciones);
Console.WriteLine();

Console.WriteLine("Ejecutando Tabu Search...");
TabuSearch.Ejecutar(n, iteraciones);
Console.WriteLine();

Console.WriteLine("Ejecutando Local Search...");
Local_Search.Ejecutar(n, iteraciones);