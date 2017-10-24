using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ClearMeasure.Bootcamp.DataAccessEF.Model;

namespace ClearMeasure.Bootcamp.DataAccessEF.Mappings
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DatabaseSetting")
        {
        }

        public virtual DbSet<AuditEntry> AuditEntries { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<ExpenseReport> ExpenseReports { get; set; }
        public virtual DbSet<ExpenseReportFact> ExpenseReportFacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditEntry>()
                .Property(e => e.EndStatus)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AuditEntry>()
                .Property(e => e.EmployeeName)
                .IsUnicode(false);

            modelBuilder.Entity<AuditEntry>()
                .Property(e => e.BeginStatus)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ExpenseReports)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.SubmitterId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ExpenseReports1)
                .WithOptional(e => e.Employee1)
                .HasForeignKey(e => e.ApproverId);

            modelBuilder.Entity<Expense>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ExpenseReport>()
                .Property(e => e.Status)
                .IsFixedLength();

            modelBuilder.Entity<ExpenseReport>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ExpenseReportFact>()
                .Property(e => e.Number)
                .IsUnicode(false);

            modelBuilder.Entity<ExpenseReportFact>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ExpenseReportFact>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<ExpenseReportFact>()
                .Property(e => e.Submitter)
                .IsUnicode(false);

            modelBuilder.Entity<ExpenseReportFact>()
                .Property(e => e.Approver)
                .IsUnicode(false);
        }
    }
}
