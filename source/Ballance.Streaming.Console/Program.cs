using System;

namespace Ballance.Streaming.ConsoleRunner
{
    class Program
    {
        private const string _defaultFile = "SoftWareCatalog1991.png";
        static void Main(string[] args)
        {
            // DummyFileCreation();
            CompressionTesting();
            // DownloadDocFromHttp();
        }

        private static void DownloadDocFromHttp()
        {
            var networkStream = NetworkStreamer.ReadStreamFromUrl("http://127.0.0.1:8080/SoftwareCatalog1991.png");
            FileStreamer.WriteStreamToFile(@"C:\home\temp\downloaded\SoftWareCatalog1991_Downloaded.png", networkStream);
        }

        private static void DummyFileCreation()
        {
            Console.WriteLine("Press a key to create a dummy file.");
            Console.ReadKey();

            //DummyDataCreator.CreateDummyTextFileUsingStream(250);
            DummyDataCreator.CreateDummyTextFileUsingBuffer(250);

            Console.WriteLine("Done.");
            Console.ReadKey();
        }

        private static void CompressionTesting()
        {
            CompressionStreamer.CompressFile(_defaultFile, "SoftWareCatalog1991_Downloaded.zippd");

            CompressionStreamer.DecompressFile("SoftWareCatalog1991_Downloaded.zippd", "SoftWareCatalog1991_Downloaded_Unzipped.png");
        }
    }
}
