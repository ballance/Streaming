using System;
using System.IO;

namespace Ballance.Streaming.Library.Tests
{
    public class StreamFactory
    {
        private static readonly Lazy<StreamFactory> _streamFactory = new Lazy<StreamFactory>(() => new StreamFactory());
        private Random rand;
        public static StreamFactory Instance {  get { return _streamFactory.Value; } }

        public StreamFactory()
        {
            rand = new Random();
        }

        public MemoryStream CreateByteStream(int size)
        {
            var buffer = new byte[size];
            rand.NextBytes(buffer);
            var memoryStream = new MemoryStream(buffer);
            return memoryStream;
        }

        public MemoryStream CreateByteStreamExperimental(int size)
        {
            var memoryStream = new MemoryStream();
            memoryStream.Seek(size-1, SeekOrigin.Begin);
            var singleByte = new byte[1];
            rand.NextBytes(singleByte);
            memoryStream.WriteByte(singleByte[0]);
            return memoryStream;
        }
    }
}