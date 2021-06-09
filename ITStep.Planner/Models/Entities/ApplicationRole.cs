using System;
using Microsoft.AspNetCore.Identity;

namespace ITStep.Planner.Models.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole(string name)
        {
            this.Name = name;
        }
    }
}