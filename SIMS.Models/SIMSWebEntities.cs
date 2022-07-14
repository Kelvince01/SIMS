using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SIMS.Models
{
    public class SIMSWebEntities : DbContext
    {
        public SIMSWebEntities()
            : base("name=SIMSWebEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) => throw new UnintentionalCodeFirstException();

        public virtual DbSet<UsersDesktop> UsersDesktops { get; set; }
        public virtual DbSet<DesktopMenu> DesktopMenus { get; set; }
        public virtual DbSet<ThemeSetting> ThemeSettings { get; set; }
        public virtual DbSet<GlobalSetup> GlobalSetups { get; set; }
        public virtual DbSet<UsersDesktopMenu> UsersDesktopMenus { get; set; }
        public virtual DbSet<StyleSize> StyleSizes { get; set; }
        public virtual DbSet<AttenantLog> AttenantLogs { get; set; }
        public virtual DbSet<SIMS.Models.Act_MasterChartOfAccount> Act_MasterChartOfAccount { get; set; }
        public virtual DbSet<SIMS.Models.Discriminator> Discriminators { get; set; }
    }
}
