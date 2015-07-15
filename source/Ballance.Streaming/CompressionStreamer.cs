using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballance.Streaming
{
    public class CompressionStreamer
    {
        private const string _basePath = @"c:\home\temp\compression\";

        public static void CompressFile(string inputFile, string outputFile)
        {
            var inputPath = String.Concat(_basePath, inputFile);
            var outputPath = String.Concat(_basePath, outputFile);

            using (var inStream = new FileStream(inputPath, FileMode.Open))
            {
                using (var outStream = new FileStream(outputPath, FileMode.Create))
                {
                    using (var compressedStream = new DeflateStream(outStream, CompressionMode.Compress))
                    {
                        inStream.CopyTo(compressedStream);
                    }
                }
            }
        }

        public static void DecompressFile(string inputFile, string outputFile)
        {
            var inputPath = String.Concat(_basePath, inputFile);
            var outputPath = String.Concat(_basePath, outputFile);

            using (var inStream = new FileStream(inputPath, FileMode.Open))
            {
                using (var outStream = new FileStream(outputPath, FileMode.Create))
                {
                    using (var decompressedStream = new DeflateStream(outStream, CompressionMode.Decompress))
                    {
                        inStream.CopyTo(decompressedStream);
                    }
                }
            }
        }

        private static Stream Decompress(Stream compressedStream)
        {
            var decompressedFileStream = new MemoryStream();
            using (var decompressionStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            {
                decompressionStream.CopyTo(decompressedFileStream);
                return decompressionStream;
            }
        }

        private static Stream Compress(Stream input)
        {
            var compressionStream = new MemoryStream();
            var compressor = new DeflateStream(compressionStream, CompressionMode.Compress);
            {
                input.CopyTo(compressor);
                compressor.Flush();
                return compressionStream;
            }
        }
    }
}
