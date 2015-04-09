using System.Data.Entity;
using MetalPay.Payroll.Contracts.Model;
using MetalPay.Payroll.Model;
using Paycode = MetalPay.Payroll.Model.Paycode;
using Transaction = MetalPay.Payroll.Model.Transaction;

namespace MetalPay.Payroll.Infrastructure
{
    public class PayrollDbContext : DbContext
    {
        public PayrollDbContext()
            : base(ApplicationSettings.Get("PayrollConnectionString"))
        {
            Database.SetInitializer<PayrollDbContext>(null);
        }

        public DbSet<PersonCompanyTransactions> EmploymentTransactions { get; set; }
        public DbSet<Payslip> Payslips { get; set; }
        public DbSet<Model.Person> Persons { get; set; }
        public DbSet<Paycode> Paycodes { get; set; }
        public DbSet<PersonCompany> PersonCompanies { get; set; }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<AuditLogEntry> AuditLogEntries { get; set; }

        public DbSet<PersonVersion> PersonVersions { get; set; }
        public DbSet<PayrollVersion> PayrollVersions { get; set; }
        public DbSet<PeriodTransactionsVersion> PeriodTransactionsVersions { get; set; }
        public DbSet<PayslipVersion> PayslipVersions { get; set; }
        public DbSet<Company> Companies { get; set; }
        
        public DbSet<Model.UnionDues> UnionDues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonCompanyTransactions>().ToTable("PersonCompanyTransactions").HasKey(e => e.Id);
            modelBuilder.Entity<PersonCompanyTransactions>().HasRequired(t => t.PersonCompany).WithOptional().Map(c => c.MapKey("PersonCompanyId"));
            modelBuilder.Entity<PersonCompanyTransactions>().HasMany(e => e.Transactions).WithRequired().HasForeignKey(t => t.EmploymentTransactionsId);

            modelBuilder.Entity<Transaction>().ToTable("Transaction").HasKey(t => new { t.Id, t.EmploymentTransactionsId });
            modelBuilder.Entity<Transaction>().HasRequired(t => t.Paycode).WithMany().Map(c => c.MapKey("PaycodeId"));

            modelBuilder.Entity<Payslip>().ToTable("Payslip").HasKey(x => x.Id);
            modelBuilder.Entity<Payslip>().HasMany(x => x.Lines).WithRequired().HasForeignKey(x => x.PayslipId);
            modelBuilder.Entity<PayslipLine>().ToTable("PayslipLine").HasKey(x => new { x.Id, x.PayslipId });
            modelBuilder.Entity<Model.Person>().ToTable("Person").HasKey(x => x.Id).HasMany(x => x.PersonCompanies).WithRequired(c => c.Person).Map(x => x.MapKey("PersonId"));
            modelBuilder.Entity<Paycode>().ToTable("Paycode").HasKey(x => x.Id);

            modelBuilder.Entity<PersonCompany>().ToTable("PersonCompany").HasKey(x => x.Id);
            modelBuilder.Entity<PersonCompany>().HasMany(x => x.Employments).WithRequired(c => c.PersonCompany).Map(x => x.MapKey("PersonCompanyId"));
            modelBuilder.Entity<PersonCompany>().HasOptional(x => x.MainEmployment).WithOptionalDependent().Map(x => x.MapKey("MainEmploymentId"));
            modelBuilder.Entity<PersonCompany>().HasOptional(x => x.UnionDues).WithOptionalDependent().Map(x => x.MapKey("UnionDuesId"));

            modelBuilder.Entity<Employment>().ToTable("Employment").HasKey(x => x.Id);

            modelBuilder.Entity<AuditLogEntry>().ToTable("AuditLog").HasKey(x => x.Id);

            modelBuilder.Entity<PersonVersion>().ToTable("PersonVersion").HasKey(x => x.PersonId);
            modelBuilder.Entity<PayrollVersion>().ToTable("PayrollVersion").HasKey(x => x.TenantId);
            modelBuilder.Entity<PeriodTransactionsVersion>().ToTable("PeriodTransactionsVersion").HasKey(x => x.PeriodTransactionsId);
            modelBuilder.Entity<PayslipVersion>().ToTable("PayslipVersion").HasKey(x => x.PeriodTransactionsId);

            modelBuilder.Entity<Company>().ToTable("Company").HasKey(x => x.Id);

            modelBuilder.Entity<Model.UnionDues>().ToTable("UnionDues").HasKey(l => l.Id);
            modelBuilder.Entity<Model.UnionDues>().HasRequired(x => x.Paycode).WithOptional().Map(x => x.MapKey("PaycodeId"));
        }
    }
}