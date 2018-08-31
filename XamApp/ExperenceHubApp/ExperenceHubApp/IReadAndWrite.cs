using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExperenceHubApp
{
    interface IReadAndWrite
    {
        Task<bool> WriteToFile(string FileName, string text);
        Task<string> ReadFromFile(string FileName);
    }
}
