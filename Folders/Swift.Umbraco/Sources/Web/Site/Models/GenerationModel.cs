using Models.DTO;
using System;
using System.Collections.Generic;

namespace $safeprojectname$.Models
{
    public class GenerationModel
    {
        public GenerationModel()
        {
            Allocable = new List<Allocable>();
        }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan OpenTime { get; set; }

        public TimeSpan CloseTime { get; set; }

        public GeneratorLimitOptions LimitOption { get; set; }

        public int LimitNumber { get; set; }

        public List<Allocable> Allocable { get; set; }
    }
}