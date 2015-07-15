using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ballance.Streaming
{
    public class NetworkStreamer
    {
        private const string _basePath = @"c:\home\temp\";

        public static Stream ReadStreamFromUrl(string url)
        {
            var memoryStream = new MemoryStream();

            var httpClient = new HttpClient();
            var streamTask = httpClient.GetStreamAsync(url);
            var networkStream = streamTask.Result;
            return networkStream;
        }
    }
}
