using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kieru.Models
{
    public class Secret
    {
        [Key]
        [Required]
        [Display(Name ="Secret GUID")]
        public Guid Id { get; set; }
        
        [Required]
        [Display(Name = "Secret")]
        public string Phrase { get; set; }

        public Guid OwnerId { get; set; }

        [DefaultValue(-1)]
        public short WasViewed { get; set; }
    }
}
