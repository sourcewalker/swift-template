﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace $safeprojectname$.Services.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}