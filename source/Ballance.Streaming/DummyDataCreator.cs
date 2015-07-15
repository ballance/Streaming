using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballance.Streaming
{
    public class DummyDataCreator
    {
        public DummyDataCreator()
        {
            
        }
        public static void CreateDummyTextFile()
        {
            FileStream fs = new FileStream(@"c:\home\temp\huge_dummy_file", FileMode.Create);
            fs.Seek(2048L*1024, SeekOrigin.Begin);
            fs.WriteByte(0);
            fs.Close();
        }
    }
}
