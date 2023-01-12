using Npgsql;
using System;
using System.Data.SqlClient;




Console.WriteLine("START");

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

Console.WriteLine("KONIEC");
Console.ReadLine();