using System;
using System.IO;

namespace Ballance.Streaming
{
    public class DummyDataCreator
    {
        public static void CreateDummyTextFileUsingStream(int sizeInMegabytes)
        {
            var fs = new FileStream(@"c:\home\temp\huge_dummy_file_Stream", FileMode.Create);
            fs.Seek(1048L * 1024 * sizeInMegabytes, SeekOrigin.Begin);
            fs.WriteByte(0);
            fs.Close();
        }

        public static void CreateDummyTextFileUsingBuffer(int sizeInMegabytes)
        {
            var rand = new Random();
            using (var fs = new FileStream(@"c:\home\temp\huge_dummy_file_Buffer", FileMode.Create))
            {
                var dummyByteArray = new byte[1048L * 1024 * sizeInMegabytes];

                rand.NextBytes(dummyByteArray);
                fs.Write(dummyByteArray, 0, dummyByteArray.Length);
                fs.Flush();
            }
        }
    }
}