using System;

namespace ClearMeasure.Bootcamp.Core.Model
{
    public class AuditEntry
    {
        public AuditEntry()
        {
        }

        public AuditEntry(Employee employee, DateTime date, ExpenseReportStatus beginStatus, ExpenseReportStatus endStatus)
        {
            Employee = employee;
            EmployeeName = Employee.GetFullName();
            Date = date;
            BeginStatus = beginStatus;
            EndStatus = endStatus;
        }

        public virtual Employee Employee { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string EmployeeName { get; set; }
        public virtual ExpenseReportStatus BeginStatus { get; set; }
        public virtual ExpenseReportStatus EndStatus { get; set; }
        public Guid ExpenseReportId { get; set; }
        public int Sequence { get; set; }
        public Guid? EmployeeId { get; set; }

    }
}