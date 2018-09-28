using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StrikeSentinelAPI.Models
{
    [Table("Company")]
    public class Company
    {
        public Company()
        {
            Strike = new HashSet<Strike>();
        }
        
        [Key]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public virtual ICollection<Strike> Strike { get; set; }
    }
}
