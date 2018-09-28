using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StrikeSentinelAPI.Models
{
    [Table("Strike")]
    public class Strike
    {
        [Key]
        public int StrikeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public int Company_CompanyId { get; set; }

        public int StrikeStatus_StrikeStatusId { get; set; }

        public int StrikeNews_StrikeNewsId { get; set; }

        public virtual Company Company { get; set; }

        public virtual StrikeNews StrikeNews { get; set; }

        public virtual StrikeStatus StrikeStatus { get; set; }
    }
}
