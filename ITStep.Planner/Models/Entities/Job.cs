using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITStep.Planner.Models.Entities
{
    public class Job : BaseEntity<Guid>
    {
        public Guid JobTypeId { get; set; }

        [ForeignKey(nameof(JobTypeId))]
        public virtual JobType JobType { get; set; }
        
        public Guid ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }
        
        [StringLength(155)]
        public string Title { get; set; }
        
        [StringLength(1000)]
        public string Description { get; set; }
        
        public Guid JobStatusId { get; set; }

        [ForeignKey(nameof(JobStatusId))]
        public virtual JobStatus JobStatus { get; set; }
        
        public Guid AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual ApplicationUser Author { get; set; }
        
        public virtual ICollection<ApplicationUser> Employees { get; set; }
        
        public DateTime Deadline { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
        
        public ICollection<WorkLog> WorkLogs { get; set; } 

        public Job()
        {
            Employees = new HashSet<ApplicationUser>();
            Comments = new HashSet<Comment>();
            WorkLogs = new HashSet<WorkLog>();
        }
    }
}