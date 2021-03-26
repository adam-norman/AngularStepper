using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParentDetailsApp.ViewModel
{
    public class StepVM
    {
        public int? Id { get; set; }

        [Required, MaxLength(20)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
