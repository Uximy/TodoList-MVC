using System;
using ITStep.Planner.Models.Entities;

namespace ITStep.Planner.Models
{
    public class EditJobRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid JobStatusId { get; set; }
        public virtual JobStatus JobStatus { get; set; }
    }
}
