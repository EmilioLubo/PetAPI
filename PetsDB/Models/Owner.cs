using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsDB.Models
{
    public class Owner
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OwnerId { get; set; }
        [Required, StringLength(maximumLength:20)]
        public string Name { get; set; }
        [Required, StringLength(maximumLength:30)]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime Birth { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
