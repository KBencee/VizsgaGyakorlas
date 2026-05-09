Backend vészhelyzet esetére: 
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
