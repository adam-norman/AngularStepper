using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Title { get; set; }
        [Required, MaxLength(200)]
        public string Description { get; set; }
        public int StepId { get; set; }
        [NotMapped]
        [ForeignKey("StepId")]
        public virtual Step Step { get; set; }
    }
}