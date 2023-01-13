using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListWithoutEF.Models
{
    public class Job
    {
        [Key]
        [DisplayName("Job ID")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Job Title")]
        public string Title { get; set; }
        [DisplayName("Job Description")]
        public string? Description { get; set; }

        [Required]
        [DisplayName("Is Completed")]
        public bool IsCheck { get; set; }

        public string? Target { get; set; }

        public int? Level { get; set; } = 0;

        [Required]
        public string AuthorName { get; set;}
        public int AuthorId { get; set; }
    }
}
