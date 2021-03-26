using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Step
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}