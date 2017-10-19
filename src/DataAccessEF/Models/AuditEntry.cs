using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ClearMeasure.Bootcamp.DataAccessEF.Models
{
    [Table("AuditEntry")]
    public partial class AuditEntry
    {
        [Key]
        [Column(Order = 0)]
        public Guid ExpenseReportId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sequence { get; set; }

        public Guid? EmployeeId { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(3)]
        public string EndStatus { get; set; }

        [StringLength(200)]
        public string EmployeeName { get; set; }

        [StringLength(3)]
        public string BeginStatus { get; set; }
    }
}
