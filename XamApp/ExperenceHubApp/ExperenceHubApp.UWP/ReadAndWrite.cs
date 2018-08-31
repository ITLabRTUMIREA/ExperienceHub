using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ExperenceHubApp.UWP
{
    class ReadAndWrite : IFileReadAndWrite
    {
        public async Task<string> ReadFromFile(string filepath)
        {
            string result = string.Empty;
            try
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile sampleFile = await storageFolder.GetFileAsync(filepath);
                result = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
            }
            catch (Exception ex)
            {
                throw new Exception("File Reading Error Occured", ex.InnerException);
            }


            return result;
        }

        public async Task<bool> WriteToFile(string filepath, string text)
        {
            bool result = true;
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile sampleFile = await localFolder.CreateFileAsync(filepath, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(sampleFile, text);
            }
            catch (Exception ex)
            {
                throw new Exception("File Writing Error Occured", ex.InnerException);
            }

            return result;
        }
    }
}

