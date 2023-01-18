using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsAPI.Dto
{
    public class OwnerDTO
    {
        [Key]
        public int OwnerId { get; set; }
        [Required, StringLength(maximumLength: 20)]
        public string Name { get; set; }
        [Required, StringLength(maximumLength: 30)]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
    }
}
