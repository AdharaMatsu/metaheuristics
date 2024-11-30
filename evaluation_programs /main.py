import csv
import math as mat


def createDataset(filename):
    file = open(filename, "r")
    information = []
    for line in file:
        temp = line.strip().split(",")
        information.append(temp)

    information.pop(0)

    for i in range(len(information)):
       for j in range(1,len(information[i])):
           information[i][j] = float(information[i][j])

    return information

if __name__ == '__main__':
    evaluations_dataset = createDataset("evaluation.csv")

    # Test Friedman
    for i in range(len(evaluations_dataset)):
        print("Algoritmo",i, ' - ',evaluations_dataset[i][1:])
    print()

    iqr_algoritmo_1 = evaluations_dataset[0][1:]
    iqr_algoritmo_2 = evaluations_dataset[1][1:]
    iqr_algoritmo_3 = evaluations_dataset[2][1:]
    iqr_algoritmo_4 = evaluations_dataset[3][1:]
    iqr_algoritmo_5 = evaluations_dataset[4][1:]
    iqr_algoritmo_6 = evaluations_dataset[5][1:]
    iqr_algoritmo_7 = evaluations_dataset[6][1:]
    iqr_algoritmo_8 = evaluations_dataset[7][1:]

    from scipy import stats # Friedman Test

    res = stats.friedmanchisquare(iqr_algoritmo_1, iqr_algoritmo_2, iqr_algoritmo_3, iqr_algoritmo_4,
                                  iqr_algoritmo_5,iqr_algoritmo_6,iqr_algoritmo_7,iqr_algoritmo_8)
    # Ho = hipotesis nula...
    # NO EXISTE DIFERENCIA ESTADISTICA ENTRE LAS MUESTRAS (GRUPOS)
    # Ha = hipostesis alternativa
    # EXISTE DIFERENCIA ESTADISTICA ENTRE LAS MUESTRAS (GRUPOS)

    # EVALUACION DE LA PRUEBA....Si pvalue < 0.05 se rechaza Ho y se acepta Ha

    # ES DECIR SI pvalue >= 0.05 se acepta Ho y se rechaza Ha

    print(res)

    # Pruebas Posthoc

    ##
    # LAS PRUEBAS POSTHOC TIENEN UTILIDAD SOLO CUANDO EXISTE UNA DIFERENCIA
    # ESTADISTICA ENTRE LOS GRUPOS Y SE DESEA CONCOER AL GRUPO O GRUPOS
    # QUE SON DIFERENTES
    ############################################################
    import numpy as np

    data = np.array([iqr_algoritmo_1, iqr_algoritmo_2, iqr_algoritmo_3, iqr_algoritmo_4, iqr_algoritmo_5,
                     iqr_algoritmo_6, iqr_algoritmo_7, iqr_algoritmo_8])
    ############################################################

    from scikit_posthocs import posthoc_nemenyi_friedman  # pip install scikit-posthocs

    res = posthoc_nemenyi_friedman(data.T)
    print(res)

    # Pruebas Posthoc 2

    ##
    # LAS PRUEBAS POSTHOC TIENEN UTILIDAD SOLO CUANDO EXISTE UNA DIFERENCIA
    # ESTADISTICA ENTRE LOS GRUPOS Y SE DESEA CONCOER AL GRUPO O GRUPOS
    # QUE SON DIFERENTES
    ############################################################
    import numpy as np

    data = np.array([iqr_algoritmo_1, iqr_algoritmo_2, iqr_algoritmo_3, iqr_algoritmo_4, iqr_algoritmo_5,
                     iqr_algoritmo_6, iqr_algoritmo_7, iqr_algoritmo_8])
    ############################################################

    from scikit_posthocs import posthoc_conover_friedman

    res = posthoc_conover_friedman(data.T)
    print(res)


