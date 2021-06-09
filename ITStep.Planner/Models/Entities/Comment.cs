using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITStep.Planner.Models.Entities
{
    public class Comment : BaseEntity<Guid>
    {
        public Guid AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))] 
        public virtual ApplicationUser Author { get; set; }
        public Guid JobId { get; set; }
        [ForeignKey(nameof(JobId))]
        public virtual Job Job { get; set; }
        [StringLength(1000)]
        public string Text { get; set; }
    }
}