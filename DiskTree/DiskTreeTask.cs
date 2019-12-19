using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DiskTree
{
    class Comparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return String.CompareOrdinal(x, y);
        }
    }

    public class Node
    {
        public Node Next { get; set; }
        public string Value { get; set; }
        public string PrintValue { get; set; }
        public int Index { get; set; }

        public List<Node> subNode;
        public Node()
        {
            Index = -1;
            Next = null;
            Value = null;
            subNode = new List<Node>();
        }
        public Node(string value,int index)
        {
            Next = null;
            Value = value;
            for (int i = 0; i < index; i++)
            {
                value = " " + value;
            }
            PrintValue = value;
            Index = index;
            subNode = new List<Node>();
        }

        public void InsertNode(Node node,string str)
        {
            List<string> subPath = str.Split('\\').ToList();
            foreach (var item in subPath)
            {
                Node tempNode = node.subNode.Where(x => x.Value.Equals(item,StringComparison.Ordinal)).SingleOrDefault();
                if (tempNode == null) 
                {
                    Node newNode = new Node(item,node.Index + 1);
                    node.subNode.Add(newNode);
                    node = newNode;                    
                }
                else
                {
                    node = tempNode;
                }
                
            }
        } 
        
        public string DiskTreeToList(Node node, LinkedList<string> list)
        {           
            node.subNode = node.subNode.OrderBy(x => x.Value).ToList();
            node.subNode.Reverse();

            foreach (var item in node.subNode)
            {
                list.AddFirst(DiskTreeToList(item,list));
            }
            return node.PrintValue; 
        }
    }

    public static class DiskTreeTask
    { 
        public static List<string> Solve(List<string> list)
        {
            Node rootNode = new Node();
            foreach (var item in list)
            {
                rootNode.InsertNode(rootNode, item);
            }
            var resultList = new LinkedList<string>();
            rootNode.DiskTreeToList(rootNode,resultList);
            return resultList.ToList();
        }


    }
}
