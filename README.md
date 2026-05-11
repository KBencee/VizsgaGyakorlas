
### Backend vészhelyzet esetére: 
<p>
3 nuget package(sima, tools, sqlite)
((((Tutorial: Teddy Smith asp))))
+2 könyvtár, models, data 
Modelben új osztály: Meccs.cs
Propertyk: int Id, stb stb (a meccs.cs-en belül)
NEM KELL KONSTRUKTOR
Datába új class: vmiDbContext
VmiDbContext : DbContext
Construktor: public foci fent van githubon
Public DbSet<foci.Models.Meccs> Meccsek{getset}
Program.cs
Add services rész
Builder.Services.AddDbContext
Appsettings.json
"ConnectionStrings":
"FocikapcsolatString": "Data Source = .\\Data\\fociAdatbazis db"
Felhő
Ott connect
Connecting string value
Data Source ...
Migrations (ha nem jó rebuild)
Connected services
Jobb click Update database
Pages jobb click 
Add razor page 
Crud ha összest
Aztán új üres razor page
Emptyt választom 
Layout.cshtml új nav item az oldal  névvel 
CsapatokListaja.cshtml.cs
Private readonly
Public ...(FociDbContext contest) Mutatják a legenerált oldalak amúgy 
Public IList<string>
Bemegy a public void OnGetbe
Csapatok = _context...
Csak hazait akarok
CsapatokListaja.cshtml
</p>

### két tábla
<p>
// Egy sorozat összes epizódja
[HttpGet("{id}/episodes")]
public IActionResult GetEpisodes(int id)
{
    var series = seriesList.FirstOrDefault(s => s.Id == id);
    if (series == null) return NotFound();

    var episodes = episodeList.Where(e => e.SeriesId == id).ToList();
    return Ok(episodes);
}

// Sorozatok az epizódokkal együtt (nested)
[HttpGet("with-episodes")]
public IActionResult GetAllWithEpisodes()
{
    var result = seriesList.Select(s => new
    {
        s.Id,
        s.Title,
        s.Genre,
        Episodes = episodeList.Where(e => e.SeriesId == s.Id).ToList()
    });
    return Ok(result);
</p>

### minta

<p>
  [ApiController]
[Route("api/[controller]")]
public class SeriesController : ControllerBase
{
    private static List<Series> seriesList = new List<Series>
    {
        new Series { Id = 1, Title = "Breaking Bad", Genre = "Drama", ReleaseYear = 2008, Rating = 9.5 },
        new Series { Id = 2, Title = "The Office", Genre = "Comedy", ReleaseYear = 2005, Rating = 8.9 }
    };

     // GET api/series
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(seriesList);
    }

     // GET api/series/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var series = seriesList.FirstOrDefault(s => s.Id == id);
        if (series == null) return NotFound();
        return Ok(series);
    }

    // POST api/series
    [HttpPost]
    public IActionResult Create([FromBody] Series newSeries)
    {
        newSeries.Id = seriesList.Max(s => s.Id) + 1;
        seriesList.Add(newSeries);
        return CreatedAtAction(nameof(GetById), new { id = newSeries.Id }, newSeries);
    }

    // PUT api/series/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Series updatedSeries)
    {
        var series = seriesList.FirstOrDefault(s => s.Id == id);
        if (series == null) return NotFound();

        series.Title = updatedSeries.Title;
        series.Genre = updatedSeries.Genre;
        series.ReleaseYear = updatedSeries.ReleaseYear;
        series.Rating = updatedSeries.Rating;

        return Ok(series);
    }

    // DELETE api/series/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var series = seriesList.FirstOrDefault(s => s.Id == id);
        if (series == null) return NotFound();

        seriesList.Remove(series);
        return NoContent();
    }
}
</p>
  
  ### Fetch apiból
<p>
useEffect(() => {
    fetch("https://pokeapi.co/api/v2/pokemon?limit=100")
        .then(res => res.json())
        .then(apiData => {
            setPokemonsData(apiData.results) // az API-tól függően más lehet a struktúra
        })
}, [])
</p>


