using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using TuraRandi.Data; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// API controllerek hozzáadása
builder.Services.AddControllers();

// Endpoint API Explorer hozzáadása a Swagger-hez
builder.Services.AddEndpointsApiExplorer();

// Swagger szolgáltatások hozzáadása
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Túra Randi API",
		Version = "v1",
		Description = "API teszteléshez használható végpontok"
	});
});

// SQLite adatbázis konfigurálása a Data könyvtárban TuraRandi.db néven








var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();

	// Swagger middleware hozzáadása fejlesztõi környezetben
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Túra Randi API v1"));
}
else
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}
app.UseCors(builder =>
{
	builder.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader();
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
