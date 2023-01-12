using Npgsql;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/index", () =>
{
    var html = @"
<!DOCTYPE HTML>
<head>
<link rel=""stylesheet"" href=""https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css"" />
<script src=""https://code.jquery.com/jquery-3.6.3.min.js"" integrity=""sha256-pvPw+upLPUjgMXY0G+8O0xUf+/Im1MZjXxxgOcBQBXU="" crossorigin=""anonymous""></script>
<script src=""https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"" integrity=""sha256-lSjKY0/srUM9BE3dPm+c4fBo1dky2v27Gdjm2uoZaL0="" crossorigin=""anonymous""></script>
<script>
$(()=>{
	$( ""#input_main"" ).autocomplete({
      source: ""/fullnames-autocomplete"",
      minLength: 3,
      select: function( event, ui ) {
        console.log( ""Selected: "" + ui.item.value + "" aka "" + ui.item.id );
      }
    });
});
</script>
</head>
<body>
<center><input type=""text"" id=""input_main"" style=""width: 60%; margin-top: 50px;"" /></center>
</body>
</html>
    ";
    return Results.Text(html, "text/html");
});


app.MapGet("/fullnames-autocomplete", async (string term) =>
{
    List<string> result = new List<string>();

    var connectionString = "Host=localhost;Username=postgres;Password=Pa$$w0rd;Database=big_data_db";
    await using var conn = new NpgsqlConnection(connectionString);
    await conn.OpenAsync();

    await using var cmd = new NpgsqlCommand("select id, name, surname from fullnames where surname like '%' || @search || '%' or name like '%' || @search || '%' limit 1000", conn); /*|| '%' limit 20"*/
    cmd.Parameters.AddWithValue("@search", term.ToUpper());

    await using (var reader = await cmd.ExecuteReaderAsync())
    {
        while (await reader.ReadAsync())
        {
            result.Add(reader["name"].ToString() + " " + reader["surname"].ToString());
        }
    }

    result.Add(term);
    return result.ToArray();
});


app.Run();

/*internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}*/