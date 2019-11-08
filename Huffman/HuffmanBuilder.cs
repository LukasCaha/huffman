using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Huffman
{
    public class Node
    {
        public bool isChar;
        public int character;
        public int count;
        public Node left;
        public Node right;

        public Node(bool isChar, int character, int count, Node left, Node right)
        {
            this.isChar = isChar;
            this.character = character;
            this.count = count;
            this.left = left;
            this.right = right;
        }
    }

    public class HuffmanBuilder : IHuffmanBuilder
    {
        public BinaryReader LoadFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File Error");
                Environment.Exit(0);
                return null;
            }

            return new BinaryReader(File.Open(path, FileMode.Open));
        }

        public List<Node> CountChars(BinaryReader streamReader)
        {
            List<Node> chars = new List<Node>();
            using (streamReader)
            {
                while (streamReader.BaseStream.Position != streamReader.BaseStream.Length)
                {
                    int newChar = streamReader.ReadByte();
                    Node n = chars.Find(n => n.character.Equals(newChar));
                    if (n != null)
                    {
                        n.count++;
                    }
                    else
                    {
                        chars.Add(new Node(true, newChar, 1, null, null));
                    }
                }

                return chars;
            }
        }

        List<Node> usedNodes = new List<Node>();
        Node rootOfTree;
        public void BuildTree(List<Node> charCount)
        {
            charCount = charCount.OrderBy(c => c.count).ThenByDescending(s => s.isChar).ThenBy(s => s.character).ToList();
            while (charCount.Count > 1)
            {
                Node smallest = charCount[0];
                Node secondSmallest = charCount[1];

                charCount.RemoveAt(1);
                charCount.RemoveAt(0);

                int firstID = usedNodes.Count;
                usedNodes.Add(smallest);
                int secondID = usedNodes.Count;
                usedNodes.Add(secondSmallest);

                Node merge = new Node(false, -1, smallest.count + secondSmallest.count, usedNodes[firstID], usedNodes[secondID]);
                charCount.Add(merge);
                if (charCount.Count==1)
                {
                    rootOfTree = merge;
                }
                charCount = charCount.OrderBy(c => c.count).ThenByDescending(s => s.isChar).ThenBy(s => s.character).ToList();
            }
            if (charCount.Count == 1)
            {
                usedNodes.Add(charCount[0]);
                charCount.RemoveAt(0);
            }
        }
        public void OutputTreeInPrefix(TextWriter textWriter)
        {
            using (textWriter)
            {
                Prefix(textWriter, rootOfTree,0);
            }
        }
        bool first = true;
        public void Prefix(TextWriter tw, Node root, int depth)
        {
            if (root.isChar)
            {
                /*for (int i = 0; i < depth; i++)
                {
                    tw.Write(" ");
                }*/
                if (first)
                {
                    tw.Write("*" + root.character + ":" + root.count);
                    first = false;
                }
                else
                {
                    tw.Write(" *" + root.character + ":" + root.count);
                }
            }
            else
            {
                //tw.WriteLine();
                /*for (int i = 0; i < depth; i++)
                {
                    if (i%2==0)
                    {
                        tw.Write("|");
                    }
                    else
                    {
                        tw.Write(" ");
                    }
                }*/
                if (first)
                {
                    tw.Write(root.count);
                    first = false;
                }
                else
                {
                    tw.Write(" " + root.count);
                }
            }
            if (root.left != null) { Prefix(tw, root.left, depth+1); }
            if (root.right != null) { Prefix(tw, root.right, depth + 1); }
        }
    }
}
