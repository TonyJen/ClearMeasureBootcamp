using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ClearMeasure.Bootcamp.DataAccessEF.Model
{
    [Table("Expense")]
    public partial class Expense
    {
        [Key]
        [Column(Order = 0)]
        public Guid ExpenseReportId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sequence { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
    }
}
