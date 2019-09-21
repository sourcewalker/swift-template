using System.Collections.Generic;

namespace $safeprojectname$.$safeprojectname$.LogoGrab.Models
{
    public class Data
    {
        public string SessionId { get; set; }
        public int Status { get; set; }
        public ProcessTime ProcessTime { get; set; }
        public List<Detection> Detections { get; set; }
    }
}
