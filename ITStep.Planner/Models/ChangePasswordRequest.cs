using System;

namespace ITStep.Planner.Models
{
    public class ChangePasswordRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}