using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExperenceHubApp
{
    interface IFileReadAndWrite
    {
        Task<bool> WriteToFile(string filepath, string text);
        Task<string> ReadFromFile(string filepath);
    }
}
