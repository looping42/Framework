using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DynamicProgrammation.Huffman
{
    public class TreeHuffman
    {
        private List<NodeHuffman> nodes = new List<NodeHuffman>();
        public NodeHuffman Root { get; set; }
        public Dictionary<char, int> Frequencies = new Dictionary<char, int>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        public void Build(string source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (!Frequencies.ContainsKey(source[i]))
                {
                    Frequencies.Add(source[i], 0);
                }

                Frequencies[source[i]]++;
            }

            foreach (KeyValuePair<char, int> symbol in Frequencies)
            {
                nodes.Add(new NodeHuffman() { Symbol = symbol.Key, Frequency = symbol.Value });
            }

            while (nodes.Count > 1)
            {
                List<NodeHuffman> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<NodeHuffman>();

                if (orderedNodes.Count >= 2)
                {
                    // Take first two items
                    List<NodeHuffman> taken = orderedNodes.Take(2).ToList<NodeHuffman>();

                    // Create a parent node by combining the frequencies
                    NodeHuffman parent = new NodeHuffman()
                    {
                        Symbol = '*',
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        Left = taken[0],
                        Right = taken[1]
                    };

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                this.Root = nodes.FirstOrDefault();
            }
        }

        /// <summary>
        /// Encodage avec des bits de la string
        /// </summary>
        /// <param name="source">string courant</param>
        /// <returns>les bits</returns>
        public BitArray Encode(string source)
        {
            List<bool> encodedSource = new List<bool>();

            for (int i = 0; i < source.Length; i++)
            {
                List<bool> encodedSymbol = this.Root.Traverse(source[i], new List<bool>());
                encodedSource.AddRange(encodedSymbol);
            }

            BitArray bits = new BitArray(encodedSource.ToArray());

            return bits;
        }

        /// <summary>
        /// Decodage des bits pour en faire des lettres
        /// </summary>
        /// <param name="bits">Tableau bit</param>
        /// <returns>Mot courant</returns>
        public string Decode(BitArray bits)
        {
            NodeHuffman current = this.Root;
            string decoded = "";

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }

                if (IsLeaf(current))
                {
                    decoded += current.Symbol;
                    current = this.Root;
                }
            }

            return decoded;
        }

        /// <summary>
        /// Si le noeud est une feuille
        /// </summary>
        /// <param name="node">noeud</param>
        /// <returns>booléen</returns>
        public bool IsLeaf(NodeHuffman node)
        {
            return (node.Left == null && node.Right == null);
        }
    }
}