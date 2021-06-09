using System;
using System.Collections.Generic;

namespace ITStep.Planner.Models.Entities
{
    public class Project : BaseEntity<Guid>
    {
        public byte[] Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<ApplicationUser> Employees { get; set; }

        public Project()
        {
            Jobs = new HashSet<Job>();
            Employees = new List<ApplicationUser>();
        }
    }
}