using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrikeSentinelAPI.Models
{
    public class StrikeNews
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int StrikeNewsId { get; set; }
        public string SourceLink { get; set; }
        public bool FalsePositive { get; set; }
    }
}
