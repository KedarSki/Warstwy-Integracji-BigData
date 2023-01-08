using Npgsql;
using System;
using System.Data.SqlClient;


/*string[] names = System.IO.File.ReadAllLines(@"C:\Git\Warstwy-Integracji-BigData\Warstwy-Integracji-BigData\Data\ImionaNadawaneDzieciom.csv");
string[] surNamesM = System.IO.File.ReadAllLines(@"C:\Git\Warstwy-Integracji-BigData\Warstwy-Integracji-BigData\Data\NazwiskaMeskie.csv");
string[] surNamesF = System.IO.File.ReadAllLines(@"C:\Git\Warstwy-Integracji-BigData\Warstwy-Integracji-BigData\Data\NazwiskaZenskie.csv");
string fullNames2 = @"C:\Git\Warstwy-Integracji-BigData\Warstwy-Integracji-BigData\Data\Full-Names.csv";

string person = String.Empty;
Random random = new();
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

    File.AppendAllText(fullNames2, appendText);

}
*/

Console.WriteLine("OK");

var filename = @"C:\Git\Warstwy-Integracji-BigData\Warstwy-Integracji-BigData\Data\Full-Names.csv";
var cs = "Host=localhost;Username=postgres;Password=Pa$$w0rd;Database=big_data_db";

using (NpgsqlConnection conn = new NpgsqlConnection(cs))
{
    conn.Open();
    using (StreamReader sr = File.OpenText(filename))
    {
        string s = String.Empty;
        var i = 0;
        while ((s = sr.ReadLine()) != null)
        {
            i++;
            try
            {
                var parts = s.Split(',');
                if (parts.Length != 3)
                    Console.WriteLine("Blad - " + i.ToString());
                else
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO public.fullnames(id, name, surname)	VALUES(:id, :imie, :nazwisko)", conn))
                    {
                        cmd.Parameters.AddWithValue("id", Convert.ToInt64(parts[0]));
                        cmd.Parameters.AddWithValue("imie", parts[1]);
                        cmd.Parameters.AddWithValue("nazwisko", parts[2]);

                        try
                        {
                            var result = cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(i.ToString() + "(1) " + ex.Message);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(i.ToString() + " (2) " + ex.Message);
                return;
            }
            if (i % 1000 == 0)
            {
                Console.WriteLine(i.ToString() + "processed " + i.ToString() + " " + (i / 40000000 * 100).ToString());
            }
        }
    }
}

Console.WriteLine("OK");
Console.ReadLine();