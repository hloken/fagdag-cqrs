using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MetalPay.Payroll.SalaryCalculation
{
    public class SalaryCalculationDbContext : DbContext
    {
        private const string SchemaName = "salarycalculation";

        public SalaryCalculationDbContext(DbConnection connection)
            : base(connection, false)
        {
            Database.SetInitializer<SalaryCalculationDbContext>(null);
        }

        public DbSet<Paycode> Paycodes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<UnionDues> UnionDues { get; set; }
        public DbSet<SalaryInterval> SalaryIntervals { get; set; }
        public DbSet<SalaryIntervalForEmployee> SalaryIntervalsForEmployees { get; set; }
        public DbSet<Additions.Addition> Additions { get; set; }
        public DbSet<Additions.EmploymentAgreements> EmploymentAgreements { get; set; }

        public DbSet<Additions.Agreement> DontMindMe { get; set; }
        public DbSet<Transaction> DontMindMeEither { get; set; }
        public DbSet<Additions.AdditionBasis> IgnoreMePlz { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            ExplicitlyDeleteOrphansBecauseEfHasShittySupportForThis();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ExplicitlyDeleteOrphansBecauseEfHasShittySupportForThis()
        {
            var deletedTransactions = DontMindMeEither.Local.Where(a => a.SalaryIntervalForEmployeeId == Guid.Empty).ToArray();
            foreach (var orphan in deletedTransactions)
            {
                DontMindMeEither.Remove(orphan);
            }

            var deletedAgreements = DontMindMe.Local.Where(a => a.EmploymentAdditionAgreementsId == Guid.Empty).ToArray();
            foreach (var orphan in deletedAgreements)
            {
                DontMindMe.Remove(orphan);
            }

            var moreOrphans = IgnoreMePlz.Local.Where(a => a.AdditionId == Guid.Empty).ToArray();
            foreach (var orphan in moreOrphans)
            {
                IgnoreMePlz.Remove(orphan);
            }

        }

        public override int SaveChanges()
        {
            ExplicitlyDeleteOrphansBecauseEfHasShittySupportForThis();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalaryIntervalForEmployee>().ToTable("SalaryIntervalForEmployee", SchemaName).HasKey(e => e.Id);
            modelBuilder.Entity<SalaryIntervalForEmployee>().HasRequired(t => t.Employee).WithMany().Map(c => c.MapKey("EmployeeId"));
            modelBuilder.Entity<SalaryIntervalForEmployee>().HasMany(e => e.Transactions).WithRequired().HasForeignKey(t => t.SalaryIntervalForEmployeeId);
            modelBuilder.Entity<SalaryIntervalForEmployee>().Property(x => x.Version).IsConcurrencyToken();

            modelBuilder.Entity<Transaction>().ToTable("Transaction", SchemaName).HasKey(t => new { t.Id });
            modelBuilder.Entity<Transaction>().HasRequired(t => t.Paycode).WithMany().Map(c => c.MapKey("PaycodeId"));
            modelBuilder.Entity<AdditionTransaction>().HasRequired(t => t.Basis).WithMany().Map(c => c.MapKey("BasisId"));

            modelBuilder.Entity<Paycode>().ToTable("Paycode", SchemaName).HasKey(x => x.Id);

            modelBuilder.Entity<Employee>().ToTable("Employee", SchemaName).HasKey(x => x.Id);
            modelBuilder.Entity<Employee>().HasMany(x => x.Employments).WithRequired(c => c.Employee).Map(x => x.MapKey("EmployeeId"));
            modelBuilder.Entity<Employee>().HasOptional(x => x.MainEmployment).WithOptionalDependent().Map(x => x.MapKey("MainEmploymentId"));
            modelBuilder.Entity<Employee>().HasOptional(x => x.UnionDues).WithOptionalDependent().Map(x => x.MapKey("UnionDuesId"));
            modelBuilder.Entity<Employee>().HasRequired(x => x.Company).WithMany().Map(x => x.MapKey("CompanyId"));
            modelBuilder.Entity<Employment>().ToTable("Employment", SchemaName).HasKey(x => x.Id);

            modelBuilder.Entity<Company>().ToTable("Company", SchemaName).HasKey(x => x.Id);
            modelBuilder.Entity<Company>().HasMany(c => c.Paycodes).WithRequired().Map(c => c.MapKey("CompanyId"));
            modelBuilder.Entity<Company>().HasMany(c => c.UnionDueses).WithRequired().HasForeignKey(u => u.CompanyId);
            modelBuilder.Entity<Company>().HasMany(c => c.SalaryIntervals).WithRequired().HasForeignKey(i => i.CompanyId);

            modelBuilder.Entity<SalaryInterval>().ToTable("SalaryInterval", SchemaName).HasKey(x => x.Id);
            modelBuilder.Entity<SalaryInterval>().Property(i => i.Version).IsConcurrencyToken();
            modelBuilder.Entity<SalaryInterval>().HasMany(i => i.SalaryIntervalsForEmployees).WithRequired().HasForeignKey(i => i.SalaryIntervalId);

            modelBuilder.Entity<UnionDues>().ToTable("UnionDues", SchemaName).HasKey(l => l.Id);

            modelBuilder.Entity<Additions.Addition>().ToTable("Addition", SchemaName).HasKey(l => l.Id);
            modelBuilder.Entity<Additions.Addition>().HasMany(i => i.Basis).WithRequired().HasForeignKey(i => i.AdditionId);
            modelBuilder.Entity<Additions.AdditionBasis>().ToTable("AdditionBasis", SchemaName).HasKey(l => l.Id);

            modelBuilder.Entity<Additions.EmploymentAgreements>().ToTable("EmploymentAdditionAgreements", SchemaName).HasKey(l => l.EmploymentId);
            modelBuilder.Entity<Additions.EmploymentAgreements>().HasMany(e => e.Agreements).WithRequired().HasForeignKey(a => a.EmploymentAdditionAgreementsId);
            modelBuilder.Entity<Additions.Agreement>().ToTable("EmploymentAdditionAgreement", SchemaName).HasKey(l => l.Id);
            modelBuilder.Entity<Additions.Agreement>().HasRequired(t => t.Addition).WithMany().Map(c => c.MapKey("AdditionId"));
        }
    }
}