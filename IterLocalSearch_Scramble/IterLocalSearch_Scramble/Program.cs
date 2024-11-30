using IterLocalSearch_Scramble;

ILS_Scramble.Run("rnd_coordinate.csv");

int i, j, num_pruebas = 30;

string carpeta = "/Users/adharacavazos/Desktop/Instancias CSV";
string[] archivosCsv = Directory.GetFiles(carpeta, "*.csv"); // Obtiene todos los archivos .csv en la carpeta



for (i = 0; i < archivosCsv.Length; i++)
{
    for (j = 0; j < num_pruebas; j++)
    {
        //ILS_Scramble.Run("coordenadas.csv");
        //ILS_Scramble.Run("rnd_coordinate.csv");
        ILS_Scramble.Run(archivosCsv[i]);
    }
}