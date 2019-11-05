using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Huffman
{
    public class HuffmanBuilder: IHuffmanBuilder
    {
        public StreamReader LoadFile(string path) { return new StreamReader(path); }
        public int[] CountChars(StreamReader streamReader) { return new int[1]; }
        public void BuildTree(int[] charCount) { }
        public void OutputTreeInPrefix(TextWriter textWriter) { }
    }
}
