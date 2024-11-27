using Best_Improvement;
using System;
using System.IO;
using System.Text.RegularExpressions;
//BestImprov.Run("instancia.csv");
string inputFile = "berlin52.tsp"; // Especifica la ruta del archivo de entrada
        string outputFile = "archivo_salida.csv"; // Especifica la ruta del archivo CSV de salida

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
            Console.WriteLine("El archivo no existe.");
        }