using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models
{
    public class TaskItem
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string? Description { get; set; }

       
        public DateTime? DueDate { get; set; }

        [Required]
        [RegularExpression("Low|Medium|High",
          ErrorMessage = "Priority must be Low, Medium, or High.")]
        public string Priority { get; set; } = "Medium";

        [Required]
        [RegularExpression("Not Started|In Progress|Completed",
           ErrorMessage = "Status must be Not Started, In Progress, or Completed.")]
        public string Status { get; set; } = "Not Started";
    }
}
