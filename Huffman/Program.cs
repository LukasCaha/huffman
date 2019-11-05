using System;
using System.IO;

namespace Huffman
{
    class Program
    {
        static void Main(string[] args)
        {
            IHuffmanBuilder builder = new HuffmanBuilder();

            StreamReader streamReader = builder.LoadFile(args[0]);
            int[] charCount = builder.CountChars(streamReader);
            builder.BuildTree(charCount);
            builder.OutputTreeInPrefix(Console.Out);
        }
    }

    public interface IHuffmanBuilder
    {
        StreamReader LoadFile(string path);
        int[] CountChars(StreamReader streamReader);
        void BuildTree(int[] charCount);
        void OutputTreeInPrefix(TextWriter textWriter);
    }
}
