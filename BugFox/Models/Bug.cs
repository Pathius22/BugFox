using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugFox.Models
{
    public class Bug
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Description { get; set; }
        public string SubmittedBy { get; set; }
        public string AssignedTo { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool isResolved { get; set; }
    }
}
