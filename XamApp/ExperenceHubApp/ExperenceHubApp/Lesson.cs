using System;
using System.Collections.Generic;
using System.Text;

namespace ExperenceHubApp
{
    public class Lesson
    {
        public string name;
        public string path;
        public string localpath;
        public DateTime downloadtime;
        public DateTime recordtime;
        public byte[] picture;
        public float price;
        public string description;
        public string Creator;
        public Guid creatorID;
    }
}
