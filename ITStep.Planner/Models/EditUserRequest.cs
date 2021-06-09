using System;

namespace ITStep.Planner.Models
{
    public class EditUserRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}