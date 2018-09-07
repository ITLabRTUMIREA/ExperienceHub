using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VRTeleportator.ViewModels
{
    public class PurchaseViewModel
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public Guid UserId { get; set; }
        public Guid LessonId { get; set; }
    }
}
