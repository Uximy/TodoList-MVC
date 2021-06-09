using System;

namespace ITStep.Planner.Models.Entities
{
    public class JobType : BaseEntity<Guid>
    {
        public string Title { get; set; }
    }
}