using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetClinicApi.Data;
using PetClinicApi.Entities;
using PetClinicApi.Models;

namespace PetClinicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly PetClinicContext _db;
        private static readonly string[] ValidSpecies = [ "Kutya", "Macska", "Nyúl", "Egyéb"];

        public PetsController(PetClinicContext db) => _db = db;

            [HttpGet]
            public async Task<ActionResult<List<PetListDto>>> GetAll([FromQuery] string? species,
                [FromQuery] string? search)
            {
                var query = _db.Pets.Include(p => p.Owner).AsQueryable();

                if (!string.IsNullOrEmpty(species))
                    query = query.Where(p => p.Species == species);

                if (!string.IsNullOrEmpty(search))
                    query = query.Where(p => p.Name.Contains(search));

                var pets = await query
                    .OrderBy(p => p.Name)
                    .Select(p => new PetListDto(
                        p.Id, p.Name, p.Species, p.Breed, p.BirthYear, p.Owner!.LastName + " " + p.Owner.FirstName))
                    .ToListAsync();
                return Ok(pets);
            
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<PetListDto>> GetById(int id)
            {
                var p = await _db.Pets
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Id == id);

                if (p is null) return NotFound();

                return Ok(new PetListDto(p.Id, p.Name, p.Species, p.Breed, p.BirthYear,
                p.Owner!.LastName + " " + p.Owner.FirstName));
            }

        [HttpPost]
        public async Task<ActionResult<PetListDto>> Create(PetCreateDto dto)
        {
            if (!ValidSpecies.Contains(dto.Species))
                return BadRequest($"Érvénytelen faj! Megengedett értékek: { string.Join(", ", ValidSpecies)}");

            var owner = await _db.Owners.FindAsync(dto.OwnerId);

            if (owner is null)
                return BadRequest($"Nem létezik gazda {dto.OwnerId} azonosítóval.");

            var pet = new Pet
            {
                Name = dto.Name,
                Species = dto.Species,
                Breed = dto.Breed,
                BirthYear = dto.BirthYear,
                OwnerId = dto.OwnerId
            };

            _db.Pets.Add(pet);
            await _db.SaveChangesAsync();

            var result = new PetListDto(pet.Id, pet.Name, pet.Species, pet.Breed,
            pet.BirthYear, owner.LastName + " " + owner.FirstName);

            return CreatedAtAction(nameof(GetById), new { id = pet.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PetCreateDto dto)
        {
            if (!ValidSpecies.Contains(dto.Species))
                return BadRequest($"Érvénytelen faj! Megengedett értékek:{ string.Join(", ", ValidSpecies)}");

            var pet = await _db.Pets.FindAsync(id);

            if (pet is null) return NotFound();

            var owner = await _db.Owners.FindAsync(dto.OwnerId);

            if (owner is null)
                return BadRequest($"Nem létezik gazda {dto.OwnerId} azonosítóval.");

            pet.Name = dto.Name; pet.Species = dto.Species;
            pet.Breed = dto.Breed; pet.BirthYear = dto.BirthYear;
            pet.OwnerId = dto.OwnerId;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pet = await _db.Pets.FindAsync(id);

            if (pet is null) return NotFound();

            _db.Pets.Remove(pet);

            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
