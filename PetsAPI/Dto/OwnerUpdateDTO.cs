using System.ComponentModel.DataAnnotations;

namespace PetsAPI.Dto
{
    public class OwnerUpdateDTO
    {

        [Required, StringLength(maximumLength: 20)]
        public string Name { get; set; }
        [Required, StringLength(maximumLength: 30)]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime Birth { get; set; }
    }
}
