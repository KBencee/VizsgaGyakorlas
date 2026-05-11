
### Backend vészhelyzet esetére: 
```txt
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
```

### két tábla
```cs
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
```

### minta

```cs
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
```
  
  ### Fetch apiból
```
useEffect(() => {
    fetch("https://pokeapi.co/api/v2/pokemon?limit=100")
        .then(res => res.json())
        .then(apiData => {
            setPokemonsData(apiData.results) // az API-tól függően más lehet a struktúra
        })
}, [])
```

### props
```tsx
// Komponens definiálása
type SeriesCardProps = {
  title: string;
  genre: string;
};

function SeriesCard({ title, genre }: SeriesCardProps) {
  return (
    <div>
      <h2>{title}</h2>
      <p>{genre}</p>
    </div>
  );
}

// Használat
<SeriesCard title="Breaking Bad" genre="Drama" />
```

### usestate 
```tsx
import { useState } from "react";

function Counter() {
  const [count, setCount] = useState(0); // kezdőérték: 0

  return (
    <div>
      <p>Szám: {count}</p>
      <button onClick={() => setCount(count + 1)}>Növel</button>
      <button onClick={() => setCount(0)}>Reset</button>
    </div>
  );
}
```

### useEffect
```tsx
import { useEffect } from "react";

function App() {
  useEffect(() => {
    console.log("Komponens betöltődött");
  }, []); // a [] azt jelenti: csak egyszer fusson le
}
```

### api fetch
```tsx
import { useState, useEffect } from "react";

type Series = {
  id: number;
  title: string;
  genre: string;
  releaseYear: number;
};

function SeriesList() {
  const [series, setSeries] = useState<Series[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch("https://localhost:7001/api/Series")
      .then((res) => res.json())
      .then((data) => {
        setSeries(data);
        setLoading(false);
      });
  }, []);

  if (loading) return <p>Betöltés...</p>;

  return (
    <ul>
      {series.map((s) => (
        <li key={s.id}>
          {s.title} – {s.genre}
        </li>
      ))}
    </ul>
  );
}
```

### post
```tsx
async function addSeries(newSeries: Series) {
  await fetch("https://localhost:7001/api/Series", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(newSeries),
  });
}
```

### fontos
```txt
A legfontosabb szabályok amiket ne felejtsd el

A komponens neve nagybetűvel kezdődik (SeriesCard, nem seriesCard)
useState és useEffect csak a komponensen belül hívható
Listáknál mindig kell key prop: <li key={s.id}>
TypeScript típust a type kulcsszóval definiálod
```
