using System;

namespace ITStep.Planner.Models.Entities
{
    public class File : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public byte[] Source { get; set; }
        public double Size { get; set; }
        public FileExtension Extension { get; set; }
    }
}