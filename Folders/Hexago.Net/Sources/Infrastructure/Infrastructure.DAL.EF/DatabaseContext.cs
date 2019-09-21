using Core.Model;
using Core.Shared.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace $safeprojectname$
{
    public class DatabaseContext : DbContext
    {
        #region Constructors

        public DatabaseContext() : base(Environments.Local.ToString())
        {
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
        }

        public DatabaseContext(string connectionName) : base(connectionName)
        {
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
        }

        #endregion

        #region DbSets

        public DbSet<Participation> Participations { get; set; }

        public IQueryable<Participation> ParticipationsQueryable
        {
            get => Participations.Include(v => v.Site);
            set => Participations = (DbSet<Participation>)value;
        }

        public DbSet<Participant> Participants { get; set; }

        public IQueryable<Participant> ParticipantsQueryable
        {
            get => Participants.Include(v => v.Participation);
            set => Participants = (DbSet<Participant>)value;
        }

        public DbSet<FailedTransaction> FailedTransactions { get; set; }

        public IQueryable<FailedTransaction> FailedTransactionsQueryable
        {
            get => FailedTransactions.Include(ft => ft.Participation);
            set => FailedTransactions = (DbSet<FailedTransaction>)value;
        }

        public DbSet<Site> Sites { get; set; }

        public IQueryable<Site> SitesQueryable
        {
            get => Sites;
            set => Sites = (DbSet<Site>)value;
        }

        #endregion

        #region configurations

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Participant>()
                    .Property(v => v.EmailHash)
                    .HasColumnAnnotation("Index",
                        new IndexAnnotation(new IndexAttribute("IX_Participant_EmailHash")));

            modelBuilder.Entity<Participation>()
                    .Property(v => v.Status)
                    .HasColumnAnnotation("Index",
                        new IndexAnnotation(new IndexAttribute("IX_Participation_Status")));

            modelBuilder.Entity<Site>()
                    .Property(v => v.Culture)
                    .HasColumnAnnotation("Index",
                        new IndexAnnotation(new IndexAttribute("IX_Site_Culture")));

            modelBuilder.Entity<Site>()
                    .Property(v => v.Domain)
                    .HasColumnAnnotation("Index",
                        new IndexAnnotation(new IndexAttribute("IX_Site_Domain")));
        }

        #endregion

    }
}
