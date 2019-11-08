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
            if (args.Length < 1)
            {
                Console.WriteLine("Argument Error");
            }
            BinaryReader streamReader = builder.LoadFile(args[0]);
            List<Node> charCount = builder.CountChars(streamReader);
            builder.BuildTree(charCount);
            builder.OutputTreeInPrefix(Console.Out);
        }
    }

    public interface IHuffmanBuilder
    {
        BinaryReader LoadFile(string path);
        List<Node> CountChars(BinaryReader streamReader);
        void BuildTree(List<Node> charCount);
        void OutputTreeInPrefix(TextWriter textWriter);
    }
}
