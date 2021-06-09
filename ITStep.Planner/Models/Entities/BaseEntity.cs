using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITStep.Planner.Models.Entities
{
    public class BaseEntity<TKey> where TKey : struct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }
        
        public DateTime CreatedAt { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
        
        public DateTime ModifiedAt { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
        
        public DateTime? DeletedAt { get; set; }

        [StringLength(50)]
        public string DeletedBy { get; set; }
    }
}