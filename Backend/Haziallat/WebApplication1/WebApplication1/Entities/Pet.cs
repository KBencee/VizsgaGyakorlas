namespace PetClinicApi.Entities
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string? Breed { get; set; }
        public int BirthYear { get; set; }
        public int OwnerId { get; set; }

        public Owner? Owner { get; set; }
    }
}
