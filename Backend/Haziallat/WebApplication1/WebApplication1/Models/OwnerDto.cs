namespace PetClinicApi.Models
{
    public record OwnerListDto(int Id, string FirstName, string LastName, string? Phone, string? Email);

    public record OwnerDetailDto(
        int Id,
        string FirstName,
        string LastName,
        string? Phone,
        string? Email,
        List<PetSimpleDto> Pets
        );

    public record OwnerCreateDto(string FirstName, string LastName, string? Phone, string? Email);

}
