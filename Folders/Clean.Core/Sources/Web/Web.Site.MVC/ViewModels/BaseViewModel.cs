using System;

namespace $safeprojectname$.ViewModels
{
    public class BaseViewModel
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
