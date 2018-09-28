﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StrikeSentinelAPI.Models
{
    [Table("StrikeNews")]
    public class StrikeNews
    {
        public StrikeNews()
        {
            Strike = new HashSet<Strike>();
        }

        [Key]
        public int StrikeNewsId { get; set; }

        [Required]
        public DateTime LastVisit { get; set; }

        public bool FalsePositive { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        [Required]
        public string SourceLink { get; set; }

        public virtual ICollection<Strike> Strike { get; set; }
    }
}
