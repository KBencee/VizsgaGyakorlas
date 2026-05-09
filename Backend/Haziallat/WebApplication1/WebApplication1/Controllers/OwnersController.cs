using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetClinicApi.Data;
using PetClinicApi.Entities;
using PetClinicApi.Models;

namespace PetClinicApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly PetClinicContext _db;
        public OwnersController(PetClinicContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<OwnerListDto>>> GetAll([FromQuery] string? search)
        {
            var query = _db.Owners.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(o => o.FirstName.Contains(search) || o.LastName.Contains(search));

            var owners = await query
                .OrderBy(o => o.LastName).ThenBy(o => o.FirstName)
                .Select(o => new OwnerListDto
                (
                    o.Id,
                    o.FirstName,
                    o.LastName,
                    o.Phone,
                    o.Email
                )).ToListAsync();
            return Ok(owners);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerDetailDto>> GetById(int id)
        {
            var owner = await _db.Owners
                .Include(o => o.Pets)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (owner is null) return NotFound();

            return Ok(new OwnerDetailDto(
                owner.Id,
                owner.FirstName,
                owner.LastName,
                owner.Phone,
                owner.Email,
                owner.Pets.Select(p =>
                new PetSimpleDto(
                    p.Id,
                    p.Name,
                    p.Species,
                    p.Breed,
                    p.BirthYear
                )).ToList()
                ));
        }

        [HttpPost]
        public async Task<ActionResult<OwnerListDto>> Create(OwnerCreateDto dto)
        {
            var owner = new Owner
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                Email = dto.Email
            };

            _db.Owners.Add(owner);
            await _db.SaveChangesAsync();

            var result = new OwnerListDto(owner.Id, owner.FirstName, owner.LastName,
            owner.Phone, owner.Email);

            return CreatedAtAction(nameof(GetById), new { id = owner.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OwnerCreateDto dto)
        {
            var owner = await _db.Owners.FindAsync(id);

            if (owner is null) return NotFound();

            owner.FirstName = dto.FirstName;
            owner.LastName = dto.LastName;
            owner.Phone = dto.Phone;
            owner.Email = dto.Email;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var owner = await _db.Owners
                .Include(o => o.Pets)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (owner is null) return NotFound();

            if (owner.Pets.Count  > 0)
                return BadRequest($"A gazda nem törölhető, mert {owner.Pets.Count} háziállat tartozik hozzá!");

            _db.Owners.Remove(owner);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
