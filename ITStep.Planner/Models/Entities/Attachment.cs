using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITStep.Planner.Models.Entities
{
    public class Attachment : BaseEntity<Guid>
    {
        public Guid AttachmentTypeId { get; set; }
        [ForeignKey(nameof(AttachmentTypeId))]
        public virtual AttachmentType Type { get; set; }
        public Guid FileId { get; set; }
        [ForeignKey(nameof(FileId))]
        public virtual File File { get; set; }
    }
}