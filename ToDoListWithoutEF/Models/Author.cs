using System.ComponentModel.DataAnnotations;

namespace ToDoListWithoutEF.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        public string? Note { get; set; }
    }
}
