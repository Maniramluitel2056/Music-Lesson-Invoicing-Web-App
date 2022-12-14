using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CDUCommunityMusic.Models;

namespace CDUCommunityMusic.Data
{
    public class CDUCommunityMusicContext : DbContext
    {
        public CDUCommunityMusicContext (DbContextOptions<CDUCommunityMusicContext> options)
            : base(options)
        {
        }

        public DbSet<CDUCommunityMusic.Models.Students> Student { get; set; } = default!;

        public DbSet<CDUCommunityMusic.Models.Lessons> Lesson { get; set; }

        public DbSet<CDUCommunityMusic.Models.Instrument> Instrument { get; set; }

        public DbSet<CDUCommunityMusic.Models.Tutors> Tutors { get; set; }

        public DbSet<CDUCommunityMusic.Models.Durations> Durations { get; set; }

        public DbSet<CDUCommunityMusic.Models.Genders> Genders { get; set; }
        public object Students { get; internal set; }
        public DbSet<CDUCommunityMusic.Models.Letter> Letter { get; set; }
    }
}

