using System;
using System.IO;
using NUnit.Framework;

namespace Ballance.Streaming.Library.Tests
{
    [TestFixture]
    public class NetworkStreamerTests
    {
        private readonly StreamFactory _streamFactory;

        [TestFixtureSetUp]
        public void Setup() { }
        
        [Test]
        public void ShouldReadFileFromWebServer_ThenWriteToFile()
        {
            var networkStream = NetworkStreamer.ReadStreamFromUrl("http://127.0.0.1:8080/SoftwareCatalog1991.png");
            FileStreamer.WriteStreamToFile(@"C:\home\temp\SoftWareCatalog1991_Downloaded.png", networkStream);
        }

        [Test]
        public void ShouldCreate1MbByteStream_ThenWriteToFile_ThenReadBackAndVerify_WithCopyTo()
        {
            var fileName = string.Concat("ShouldCreate1MbByteStream_ThenWriteToFile_ThenReadBackAndVerify_WithCopyTo", ".file");
            var byteStream1Mb = StreamFactory.Instance.CreateByteStream(1024*1024);
            FileStreamer.WriteStreamToFile_CopyTo(byteStream1Mb, fileName);

            var streamReadFromFile = FileStreamer.ReadSteamFromFile(fileName);
            Assert.AreNotSame(byteStream1Mb, streamReadFromFile);
            Assert.AreEqual(byteStream1Mb, streamReadFromFile);
        }

        [Test]
        public void ShouldCreate1GbByteStream_ThenWriteToFile_ThenReadBackAndVerify_WithCopyTo()
        {
            var fileName = string.Concat("ShouldCreate1GbByteStream_ThenWriteToFile_ThenReadBackAndVerify_WithCopyTo", ".file");
            var byteStream1Mb = StreamFactory.Instance.CreateByteStream(1024 * 1024 * 1024);
            FileStreamer.WriteStreamToFile_CopyTo(byteStream1Mb, fileName);

            var streamReadFromFile = FileStreamer.ReadSteamFromFile(fileName);
            Assert.AreNotSame(byteStream1Mb, streamReadFromFile);
            Assert.AreEqual(byteStream1Mb, streamReadFromFile);
        }
    }
}
