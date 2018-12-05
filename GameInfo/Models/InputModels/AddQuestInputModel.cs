using GameInfo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.InputModels
{
    public class AddQuestInputModel
    {
        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Quest Text")]
        public string QuestText { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Completion Condition")]
        public string CompletionCondition { get; set; }

        public List<NPCsAllViewModel> NPCs { get; set; }

        public string QuestGiver { get; set; }
    }
}
