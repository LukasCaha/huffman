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

        public int CompareTo(object obj)
        {
            int result = 1;
            if (obj != null && obj is Node)
            {
                Node person = (Node)obj;
                result = this.count.CompareTo(person.count);
            }
            return result;
        }
    }

    public class HuffmanBuilder : IHuffmanBuilder
    {
        public StreamReader LoadFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File Error");
                Environment.Exit(0);
            }

            return new StreamReader(path);
        }

        public List<Node> CountChars(StreamReader streamReader)
        {
            List<Node> chars = new List<Node>();

            while (!streamReader.EndOfStream)
            {
                int newChar = streamReader.Read();
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

            chars.Sort(delegate (Node n1, Node n2) { return n1.count.CompareTo(n2.count); });
            return chars;
        }

        List<Node> usedNodes = new List<Node>();

        public void BuildTree(List<Node> charCount)
        {

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

                charCount.Sort(delegate (Node n1, Node n2) { return n1.count.CompareTo(n2.count); });
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
                Prefix(textWriter, usedNodes[usedNodes.Count - 1]);
            }
        }

        public void Prefix(TextWriter tw, Node root)
        {
            if (root.isChar)
            {
                tw.Write("*"+(char)root.character + ":" + root.count+" ");
            }
            else
            {
                tw.Write(root.count + " ");
            }
            if (root.left != null) { Prefix(tw, root.left); }
            if (root.right != null) { Prefix(tw, root.right); }
        }

        public static void InsertionSort<T>(IList<T> list, Comparison<T> comparison)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (comparison == null)
                throw new ArgumentNullException("comparison");

            int count = list.Count;
            for (int j = 1; j < count; j++)
            {
                T key = list[j];

                int i = j - 1;
                for (; i >= 0 && comparison(list[i], key) > 0; i--)
                {
                    list[i + 1] = list[i];
                }
                list[i + 1] = key;
            }
        }
        static public int Compare(Node x, Node y)
        {
            int result = 1;
            if (x != null && x is Node &&
                y != null && y is Node)
            {
                Node personX = (Node)x;
                Node personY = (Node)y;
                result = personX.CompareTo(personY);
            }
            return result;
        }
    }
}
