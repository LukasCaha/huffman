using System;
using System.Collections.Generic;
using System.IO;

namespace Huffman
{
    class Program
    {
        static void Main(string[] args)
        {
            IHuffmanBuilder builder = new HuffmanBuilder();

            StreamReader streamReader = builder.LoadFile(args[0]);
            List<Node> charCount = builder.CountChars(streamReader);
            builder.BuildTree(charCount);
            builder.OutputTreeInPrefix(Console.Out);
        }
    }

    public interface IHuffmanBuilder
    {
        StreamReader LoadFile(string path);
        List<Node> CountChars(StreamReader streamReader);
        void BuildTree(List<Node> charCount);
        void OutputTreeInPrefix(TextWriter textWriter);
    }
}
