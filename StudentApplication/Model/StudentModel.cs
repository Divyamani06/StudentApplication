using System.ComponentModel.DataAnnotations;

namespace StudentApplication.Model
{
    public class StudentModel
    {
        [Key]
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string Std { get; set; }
        [Required]
        public int Mark { get; set; }
        
    }
}
