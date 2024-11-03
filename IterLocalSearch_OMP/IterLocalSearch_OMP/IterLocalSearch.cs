namespace IterLocalSearch_OMP;

public static class IterLocalSearch
{
    private static int FunObjetivo(int[] instancias){
        int funObj = 0; // valor objetivo --- fitness 
        for(int values = 0; values < instancias.Length; values++){
            funObj += instancias[values];
        }
        return funObj;
    }

    private static void printInstances(int[] instancias){
        for(int i =0; i < instancias.Length; i++){
            Console.Write(instancias[i]+" ");
        }
        Console.WriteLine();
    }

    public static void Run()
    {
        int num_instancias = 10; //n 
        int[] instancia = new int[num_instancias]; //solucion
        int i, fobj; 

        Random rnd = new Random();
        for(i = 0; i < num_instancias; i++){
            instancia[i] = rnd.Next(0,2);
        }

        fobj = FunObjetivo(instancia);
        Console.WriteLine(fobj);
        printInstances(instancia);
        
        int it = 0, idx, prob, fobj_new = 0;
        int[] instancia_copy;
        while(it < 100 && fobj_new != num_instancias){
            idx = rnd.Next(0, num_instancias);
            prob = rnd.Next(0, 100);
            instancia_copy = instancia.ToArray();
            if(prob > 50){
                instancia_copy[idx] = 1;
            }else{
                instancia_copy[idx] = 0;
            }
            fobj_new = FunObjetivo(instancia_copy);
            if(fobj < fobj_new){
                fobj = fobj_new;
                instancia = instancia_copy;
            }
            it++;
        }
        Console.WriteLine("Iteracion: "+it+" - 100");
        printInstances(instancia);

    }
}