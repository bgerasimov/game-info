using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.InputModels
{
    public class AddRaceInputModel
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
