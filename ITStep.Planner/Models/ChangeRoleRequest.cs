using System;
using System.Collections.Generic;
using ITStep.Planner.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace ITStep.Planner.Models
{
    public class ChangeRoleRequest
    {
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public List<ApplicationRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }

        public ChangeRoleRequest()
        {
            AllRoles = new List<ApplicationRole>();
            UserRoles = new List<string>();
        }
    }
}