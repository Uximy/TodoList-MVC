using System;
namespace ITStep.Planner.Models
{
    public class EditProjectRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
