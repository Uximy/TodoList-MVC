using System;

namespace ITStep.Planner.Models.Entities
{
    public class AttachmentType : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}