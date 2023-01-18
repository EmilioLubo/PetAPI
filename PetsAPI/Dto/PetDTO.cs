using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PetsDB.Models;

namespace PetsAPI.Dto
{
    public class PetDTO
    {
        [Key]
        public int PetId { get; set; }
        [Required, StringLength(maximumLength: 20)]
        public string PetName { get; set; }
        [Required, StringLength(maximumLength: 10)]
        public string Type { get; set; }
        [Required, StringLength(maximumLength: 40)]
        public string Breed { get; set; }
        [Required]
        public char Genre { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }
    }
}
