using Microsoft.VisualStudio.TestTools.UnitTesting;
using Huffman;
using System;
using System.IO;

namespace Huffman_UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestFirst100CharsOfFile()
        {
            string expected100Chars= "\r\naaabbabbcbabbcabbabbcbabbcabbcbabbcabbcbabbcabbcbabbcabcbabbcabbaca\r\naaabbabbcabbcabbaca\r\naaabba\r\n";
            string actual100Chars = "";

            //actual
            IHuffmanBuilder builder = new HuffmanBuilder();
            StreamReader actualStreamReader = builder.LoadFile("_test_files/soubor.txt");
            using (actualStreamReader)
            {
                for (int i = 0; i < 100; i++)
                {
                    actual100Chars += (char)actualStreamReader.Read();
                }
            }

            Assert.AreEqual((string)expected100Chars, (string)actual100Chars);
        }

        [TestMethod]
        public void TestNonExistingFile()
        {
            string expectedError = "aaaaaaaaaaaaFile error\r\naaaaaaaaaaaaaaaaaa";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                
                IHuffmanBuilder builder = new HuffmanBuilder();
                builder.LoadFile("_test_files/soubor_neexistujici.txt");

                string result = sw.ToString();
                Assert.AreEqual(expectedError, result);
            }
        }
    }
}
