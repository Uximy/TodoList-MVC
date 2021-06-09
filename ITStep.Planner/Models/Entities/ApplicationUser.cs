using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ITStep.Planner.Models.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Lastname { get; set; }
        
        public DateTime? Birthdate { get; set; }
        
        public virtual ICollection<Project> OwnedProjects { get; set; }
        
        public virtual ICollection<Project> ParticipateProjects { get; set; }

        public virtual ICollection<Job> OwnedJobs { get; set; }

        public virtual ICollection<Job> ParticipateJobs { get; set; }

        public ApplicationUser()
        {
            OwnedProjects = new List<Project>();
            ParticipateProjects = new List<Project>();
            OwnedJobs = new List<Job>();
            ParticipateJobs = new List<Job>();
        }
    }
}