namespace PetClinicApi.Models
{
    public record PetListDto(int Id, string Name, string Species, string Breed, int? BirthYear, string OwnerName);

    public record PetSimpleDto(int Id, string Name, string Species, string Breed, int? BirthYear);

    public record PetCreateDto(
        string Name,
        string Species,
        string? Breed,
        int BirthYear,
        int OwnerId
        );
}
