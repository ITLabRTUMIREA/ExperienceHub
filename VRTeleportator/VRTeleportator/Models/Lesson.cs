using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VRTeleportator.Models
{
    public class Lesson
    {
        public Guid LessonId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public byte[] Picture { get; set; }

    }
}
