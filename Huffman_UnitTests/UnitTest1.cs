using Microsoft.VisualStudio.TestTools.UnitTesting;
using Huffman;

namespace Huffman_UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestInput()
        {
            IHuffmanBuilder builder = new HuffmanBuilder() ;
            Assert.AreEqual(new System.IO.StreamReader("_test_files/soubor.txt").GetHashCode(),builder.LoadFile("_test_files/soubor.txt").GetHashCode());
        }
    }
}
