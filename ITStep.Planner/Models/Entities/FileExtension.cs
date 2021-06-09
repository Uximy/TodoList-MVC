using System;
using System.ComponentModel.DataAnnotations;

namespace ITStep.Planner.Models.Entities
{
    public class FileExtension : BaseEntity<Guid>
    {
        [StringLength(10)]
        public string Name { get; set; }
    }
}