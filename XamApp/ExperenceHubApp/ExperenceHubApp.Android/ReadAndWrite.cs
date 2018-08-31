using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ExperenceHubApp.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(ReadAndWrite))]
namespace ExperenceHubApp.Droid
{
    public class ReadAndWrite : IFileReadAndWrite
    {
        public async Task<string> ReadFromFile(string filepath)
        {
            string result = string.Empty;
            TextReader reader = null;

            try
            {
                var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var filePath = Path.Combine(documentsPath, filepath);

                reader = new StreamReader(filePath);

                if (File.Exists(filePath))
                    result = await reader.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("File Reading Error Occured", ex.InnerException);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return result;
        }

        public async Task<bool> WriteToFile(string filepath, string text)
        {
            bool result = true;
            TextWriter writer = null;
            try
            {
                var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var filePath = Path.Combine(documentsPath, filepath);

                writer = new StreamWriter(filePath);
                await writer.WriteAsync(text);

            }
            catch (Exception ex)
            {
                throw new Exception("File Writing Error Occured", ex.InnerException);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
            return result;
        }
    }
}
