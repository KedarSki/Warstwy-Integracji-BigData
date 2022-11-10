using CsvHelper;
using FileHelpers;


string[] names = System.IO.File.ReadAllLines(@"C:\Git\Warstwy-Integracji-BigData\Warstwy-Integracji-BigData\Data\ImionaNadawaneDzieciom.csv");
string[] surNamesM = System.IO.File.ReadAllLines(@"C:\Git\Warstwy-Integracji-BigData\Warstwy-Integracji-BigData\Data\NazwiskaMeskie.csv");
string[] surNamesF = System.IO.File.ReadAllLines(@"C:\Git\Warstwy-Integracji-BigData\Warstwy-Integracji-BigData\Data\NazwiskaZenskie.csv");
string fullNames = @"C:\Git\Warstwy-Integracji-BigData\Warstwy-Integracji-BigData\Data\Full-Names.csv";

string person = String.Empty;
Random random = new Random();
string delimiter = ",";

for(int i = 0; i < 40000000; i++)
{
    int indexName = random.Next(names.Length);
    var nameTab = names[indexName].Split(",");
    var name = nameTab[0];
    var sex = nameTab[2];

    if(sex == "M")
    {
        int indexSurnameM = random.Next(names.Length);
        person = name + delimiter + surNamesM[indexSurnameM].Split(",")[0];
    }

    else
    {
        int indexSurnameF = random.Next(names.Length);
        person = name + delimiter + surNamesF[indexSurnameF].Split(",")[0];
    }
    string appendText = i.ToString() + delimiter + person + Environment.NewLine;

    File.AppendAllText(fullNames, appendText);

    Console.WriteLine(appendText);

}
