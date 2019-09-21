using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace $safeprojectname$
{
    [Table("Participant")]
    public class Participant : EntityBase<Guid>
    {
        [Required]
        [MaxLength(450)]
        public string EmailHash { get; set; }

        public Guid? ParticipationId { get; set; }

        [ForeignKey(nameof(ParticipationId))]
        public Participation Participation { get; set; }

        public string ConsumerId { get; set; }
    }
}
