using System;
using System.Collections.Generic;
using System.Text;
using GameInfo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameInfo.Data
{
    public class GameInfoContext : IdentityDbContext<GameInfoUser>
    {
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Dungeon> Dungeons { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<NPC> NPCs { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<RaceProfession> RaceProfessions { get; set; }

        public GameInfoContext(DbContextOptions<GameInfoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RaceProfession>()
                .HasKey(rp => new { rp.RaceId, rp.ProfessionId });
            builder.Entity<NPC>()
                .HasMany(x => x.Loot)
                .WithOne(x => x.NPC)
                .HasForeignKey(x => x.NPCId);
            builder.Entity<NPC>()
                .HasMany(x => x.Quests)
                .WithOne(x => x.QuestGiver)
                .HasForeignKey(x => x.QuestGiverId);
            base.OnModelCreating(builder);
        }
    }
}
