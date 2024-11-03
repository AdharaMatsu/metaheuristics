
using LocalSearch_OMP;

const int n = 10;
const int totalSearch = 80;


LocalSearch ls = new LocalSearch(n);
ls.Run(totalSearch);