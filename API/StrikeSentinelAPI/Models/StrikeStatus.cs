using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StrikeSentinelAPI.Models
{
    [Table("StrikeStatus")]
    public class StrikeStatus
    {
        public StrikeStatus()
        {
            Strike = new HashSet<Strike>();
        }

        [Key]
        public int StrikeStatusId { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<Strike> Strike { get; set; }
    }
}
