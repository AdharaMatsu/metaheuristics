using LocalSearch_TSP;

// 6 coordenadas
//LocalSearch.Run("rnd_coordinate.csv");

int i, j, num_pruebas = 30;

string carpeta = "/Users/adharacavazos/Desktop/Instancias CSV";
string[] archivosCsv = Directory.GetFiles(carpeta, "*.csv"); // Obtiene todos los archivos .csv en la carpeta



for (i = 0; i < archivosCsv.Length; i++)
{
    Console.WriteLine(archivosCsv[i]);
    for (j = 0; j < num_pruebas; j++)
    {
        //LocalSearch.Run("coordenadas.csv");
        //LocalSearch.Run("rnd_coordinate.csv");
        LocalSearch.Run(archivosCsv[i]);
    }
}
