using System.ComponentModel.DataAnnotations;

namespace PetsAPI.Dto
{
    public class PetAddDTO
    {
        [Required, StringLength(maximumLength: 20)]
        public string PetName { get; set; }
        [Required, StringLength(maximumLength: 10)]
        public string Type { get; set; }
        [Required, StringLength(maximumLength: 40)]
        public string Breed { get; set; }
        [Required]
        public char Genre { get; set; }
        [Required]
        public DateTime PetBirth { get; set; }
        [Required]
        public int OwnerId { get; set; }
    }
}
