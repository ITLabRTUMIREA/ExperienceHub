using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExperenceHubApp
{
    public class FileService
    {
        IFileReadAndWrite fileReadAndWrite;

        public FileService()
        {
            fileReadAndWrite = Xamarin.Forms.DependencyService.Get<IFileReadAndWrite>();
        }

        public async Task<bool> WriteToJson(string filename, dynamic Object)
        {
            bool result = false;

            try
            {
                string serialized = JsonConvert.SerializeObject(Object);
                await fileReadAndWrite.WriteToFile(filename, serialized);
            } catch (Exception ex) { }
            return result;
        }
    }
}
