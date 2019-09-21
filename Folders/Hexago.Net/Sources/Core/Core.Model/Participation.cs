using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace $safeprojectname$
{
    [Table("Participation")]
    public class Participation : EntityBase<Guid>
    {
        public Guid? SiteId { get; set; }

        [ForeignKey(nameof(SiteId))]
        public Site Site { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        public string ApiStatus { get; set; }

        public string ApiMessage { get; set; }
    }
}
