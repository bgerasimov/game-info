using System;
using System.Collections.Generic;
using System.Text;
using GameInfo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameInfo.Data
{
    public class GameInfoContext : IdentityDbContext
    {
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Dungeon> Dungeons { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<NPC> NPCs { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Race> Races { get; set; }

        public GameInfoContext(DbContextOptions<GameInfoContext> options)
            : base(options)
        {
        }
    }
}
