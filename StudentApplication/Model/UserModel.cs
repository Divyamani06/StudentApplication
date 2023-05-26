using System.ComponentModel.DataAnnotations;

namespace StudentApplication.Model
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
