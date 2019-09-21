using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace $safeprojectname$
{
    [Table("FailedTransaction")]
    public class FailedTransaction : EntityBase<Guid>
    {
        public Guid? ParticipationId { get; set; }

        [ForeignKey(nameof(ParticipationId))]
        public Participation Participation { get; set; }

        public bool TermsConsent { get; set; }

        [Required]
        public bool NewsletterOptin { get; set; }
    }
}
