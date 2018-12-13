using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.InputModels
{
    public class AddItemInputModel
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [StringLength(70)]
        [Display(Name = "Acquired From")]
        public string AcquiredFrom { get; set; }

        [Required]
        public string Usage { get; set; }
    }
}
