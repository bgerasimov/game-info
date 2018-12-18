using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Guide> Guides { get; set; } = new List<Guide>();

        public IEnumerable<Item> Items { get; set; } = new List<Item>();

        public IEnumerable<NPC> NPCs { get; set; } = new List<NPC>();

        public IEnumerable<Race> Races { get; set; } = new List<Race>();

        public IEnumerable<Profession> Professions { get; set; } = new List<Profession>();

        public IEnumerable<Quest> Quests { get; set; } = new List<Quest>();

        public IEnumerable<Dungeon> Dungeons { get; set; } = new List<Dungeon>();

        public IEnumerable<Achievement> Achievements { get; set; } = new List<Achievement>();
    }
}
