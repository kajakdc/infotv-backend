using Microsoft.EntityFrameworkCore;

namespace infotv.Models
{
    public class InfoTVContext : DbContext
    {
        public InfoTVContext(DbContextOptions<InfoTVContext> options) : base(options)
        {

        }

        public DbSet<MotdItem> MotdItems { get; set; }
        public DbSet<NoteItem> NoteItems { get; set; }
        public DbSet<LunchItem> LunchItems { get; set; }
        
    }
    
}