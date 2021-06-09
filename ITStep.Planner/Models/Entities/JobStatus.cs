using System;

namespace ITStep.Planner.Models.Entities
{
    public class JobStatus : BaseEntity<Guid>
    {
        public string Title { get; set; }
    }
}