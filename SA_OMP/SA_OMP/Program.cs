using SA_OMP;

int n = 10;
int tot_busquedas = 80;
int tot_ejecuciones_RC = 50;
double TemperaturaInicial = 100; 
double alfa = 0.95;
SA sa = new SA(n, TemperaturaInicial);
sa.run(tot_ejecuciones_RC,tot_busquedas, alfa);
