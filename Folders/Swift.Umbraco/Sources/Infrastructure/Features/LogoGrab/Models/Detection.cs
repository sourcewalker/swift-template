using System.Collections.Generic;

namespace $safeprojectname$.$safeprojectname$.LogoGrab.Models
{
    public class Detection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public string Size { get; set; }
        public double Area { get; set; }
        public double? AreaPercentage { get; set; }
        public List<double?> ValidationFlags { get; set; }
        public double? ConfidenceALE { get; set; }
        public List<double> Coordinates { get; set; }
    }
}
