namespace ClearMeasure.Bootcamp.DataAccessEF.Mappings
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using ClearMeasure.Bootcamp.Core.Model;

    public partial class DataContext: DbContext
    {
        public DataContext()
            : base("name=DatabaseSetting")
        {
        }

        public virtual DbSet<AuditEntry> AuditEntries { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<ExpenseReport> ExpenseReports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .ToTable("Employee");

            modelBuilder.Entity<Expense>()
                .ToTable("Expense");
            modelBuilder.Entity<Expense>()
                .HasKey(t => new { t.ExpenseReportId, t.Sequence });

            modelBuilder.Entity<ExpenseReport>()
                .ToTable("ExpenseReport");

            modelBuilder.Entity<AuditEntry>()
                .ToTable("AuditEntry");

            modelBuilder.Entity<AuditEntry>()
                .HasKey(t => new { t.ExpenseReportId, t.Sequence });
             
        }
    }
}
