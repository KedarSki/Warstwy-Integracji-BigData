using CsvHelper;
using FileHelpers;
using Warstwy_Integracji_BigData;

Names names = new();

var fileHelperEngine = new FileHelperEngine<Names>();
var records = fileHelperEngine.ReadFile(@"C:\Git\Warstwy-Integracji-BigData\Warstwy-Integracji-BigData\Data\ImionaNadawaneDzieciom.csv");


foreach (var name in records)
{
    Console.WriteLine(name.Name);
    Console.WriteLine(name.Quantity);
    Console.WriteLine(name.Sex);
}