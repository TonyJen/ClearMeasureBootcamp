using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ClearMeasure.Bootcamp.DataAccessEF.Model
{

    [Table("ExpenseReport")]
    public partial class ExpenseReport
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(5)]
        public string Number { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(4000)]
        public string Description { get; set; }

        [Required]
        [StringLength(3)]
        public string Status { get; set; }

        public Guid SubmitterId { get; set; }

        public Guid? ApproverId { get; set; }

        public int? MilesDriven { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? FirstSubmitted { get; set; }

        public DateTime? LastSubmitted { get; set; }

        public DateTime? LastWithdrawn { get; set; }

        public DateTime? LastCancelled { get; set; }

        public DateTime? LastApproved { get; set; }

        public DateTime? LastDeclined { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }
    }
}
