using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.InputModels
{
    public class AddGuideInputModel
    {
        [Required]
        [Display(Name = "Guide Title")]
        public string GuideTitle { get; set; }

        [Required]
        [Display(Name = "Guide Content")]
        [DataType(DataType.MultilineText)]
        public string GuideContent { get; set; }
    }
}
