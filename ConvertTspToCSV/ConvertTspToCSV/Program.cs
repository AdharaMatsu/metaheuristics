using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string folderPath = @"/Users/adharacavazos/codes/InstanciasTSP"; 
        string outputFolder = @"/Users/adharacavazos/codes/InstanciasCSV"; 

        // Obtener todos los archivos .tsp en la carpeta
        string[] files = Directory.GetFiles(folderPath, "*.tsp");

        // Iterar sobre cada archivo .tsp
        foreach (var inputFile in files)
        {
            string outputFile = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(inputFile) + "_salida.csv"); // Definir el nombre del archivo de salida

            if (File.Exists(inputFile))
            {
                // Abrir el archivo para leer
                using (StreamReader reader = new StreamReader(inputFile))
                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    string line;
                    bool inCoordinatesSection = false;
                    int nodeCount = 0; // Variable para contar el número de nodos

                    // Primero, leer el archivo para contar el número de nodos
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();

                        if (line.Equals("NODE_COORD_SECTION"))
                        {
                            inCoordinatesSection = true;
                        }
                        else if (line.Equals("EOF"))
                        {
                            break;
                        }
                        else if (inCoordinatesSection)
                        {
                            // Extraer coordenadas
                            var coordMatch = Regex.Match(line, @"(\d+)\s+(-?\d+)\s+(-?\d+)");
                            if (coordMatch.Success)
                            {
                                nodeCount++; // Contar cada nodo encontrado
                            }
                        }
                    }

                    // Volver a leer el archivo desde el inicio para procesar los datos
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    reader.DiscardBufferedData();

                    // Escribir la cabecera con la cantidad de nodos
                    writer.WriteLine($"{nodeCount}");

                    // Volver a procesar las coordenadas y escribirlas en el archivo CSV
                    bool inCoordinatesSectionAgain = false;
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();

                        if (line.Equals("NODE_COORD_SECTION"))
                        {
                            inCoordinatesSectionAgain = true;
                        }
                        else if (line.Equals("EOF"))
                        {
                            break;
                        }
                        else if (inCoordinatesSectionAgain)
                        {
                            // Extraer coordenadas
                            var coordMatch = Regex.Match(line, @"(\d+)\s+(-?\d+)\s+(-?\d+)");
                            if (coordMatch.Success)
                            {
                                int x = int.Parse(coordMatch.Groups[2].Value);
                                int y = int.Parse(coordMatch.Groups[3].Value);

                                // Escribir las coordenadas en el archivo CSV
                                writer.WriteLine($"{x}, {y}");
                            }
                        }
                    }

                    Console.WriteLine($"Datos procesados y guardados en {outputFile}");
                }
            }
            else
            {
                Console.WriteLine($"El archivo {inputFile} no existe.");
            }
        }
    }
}
