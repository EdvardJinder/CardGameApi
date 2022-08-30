using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CardGameAPI.Models;

namespace CardGameAPI.Data
{
    public class CardGameAPIContext : DbContext
    {
        public CardGameAPIContext (DbContextOptions<CardGameAPIContext> options)
            : base(options)
        {
        }

        public DbSet<CardGameAPI.Models.Highscore> Highscore { get; set; } = default!;
    }
}
