﻿using System;
using System.Collections.Generic;

namespace $safeprojectname$.Ecommerce.Response
{
    public class EcommerceBatchSyncResponse
    {
        public DateTime CompletedDate { get; set; }

        public TimeSpan Duration { get; set; }

        public List<SyncStatus> Status { get; set; }
    }
}
