using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballance.Streaming
{
    public class FileStreamer
    {
        private const string _basePath = @"c:\home\temp\";
        public static void WriteStreamToFile_CopyTo(MemoryStream memoryStream,
                                                    string fileName = "uninitalized.file",
                                                    string basePath = _basePath)
        {
            using (var fileStreamWriter = new FileStream(string.Concat(basePath, fileName), FileMode.Create))
            {
                try
                {
                    memoryStream.CopyTo(fileStreamWriter);
                }
                finally
                {
                    memoryStream.Flush();
                }
            }
        }

        public static void WriteStreamToFile_BlockCopy(MemoryStream inputStream,
                                                    string fileName = "uninitalized.file",
                                                    string basePath = _basePath)
        {
            using (var fileStreamWriter = new FileStream(string.Concat(basePath, fileName), FileMode.Create))
            {
                try
                {
                    var buffer = new byte[4096];  // Adjust buffer size as needed for performance / memory optimization
                    int read;
                    while ((read = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStreamWriter.Write(buffer, 0, read);
                    }
                }
                finally
                {
                    inputStream.Flush();
                    fileStreamWriter.Flush();
                }
            }
        }

        public static Stream ReadSteamFromFile(string fileName, string basePath = _basePath)
        {
            return File.Open(string.Concat(basePath, fileName), FileMode.Open);
        }

        public static void WriteStreamToFile(string filePath, Stream networkStream)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                networkStream.CopyTo(fileStream);
            } 
        }
    }
}
