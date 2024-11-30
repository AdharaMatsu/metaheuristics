﻿using IterLocalSearch_Insercion;

IterLocalSearch.Run("coordenadas.csv");
int i, j, num_pruebas = 30;

string carpeta = "/Users/adharacavazos/Desktop/Instancias CSV";
string[] archivosCsv = Directory.GetFiles(carpeta, "*.csv"); // Obtiene todos los archivos .csv en la carpeta

for (i = 0; i < archivosCsv.Length; i++)
{
    for (j = 0; j < num_pruebas; j++)
    {
        //IterLocalSearch.Run("coordenadas.csv");
        //IterLocalSearch.Run("rnd_coordinate.csv");
        IterLocalSearch.Run(archivosCsv[i]);
    }
}