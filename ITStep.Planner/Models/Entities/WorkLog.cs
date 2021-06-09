using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITStep.Planner.Models.Entities
{
    public class WorkLog : BaseEntity<Guid>
    {
        public Guid JobId { get; set; }
        
        [ForeignKey(nameof(JobId))]
        public virtual Job Job { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
    }
}