using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace $safeprojectname$
{
    [Table("Site")]
    public class Site : EntityBase<Guid>
    {
        [Required]
        [MaxLength(10)]
        public string Culture { get; set; }

        public string Name { get; set; }

        [MaxLength(50)]
        public string Domain { get; set; }
    }
}
