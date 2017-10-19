using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ClearMeasure.Bootcamp.DataAccessEF.Models
{

    [Table("ExpenseReportFact")]
    public partial class ExpenseReportFact
    {
        public Guid Id { get; set; }

        [StringLength(5)]
        public string Number { get; set; }

        public DateTime? TimeStamp { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        [StringLength(200)]
        public string Status { get; set; }

        [StringLength(200)]
        public string Submitter { get; set; }

        [StringLength(200)]
        public string Approver { get; set; }
    }
}
