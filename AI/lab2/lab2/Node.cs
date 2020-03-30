﻿using System;
using System.Collections.Generic;
using System.Text;

namespace lab2
{
    class Node
    {
        public Node Prev { set; get; }
        public Bucket FirstBucket { set; get; }
        public Bucket SecondBucket { set; get; }
        public string Action { set; get; }

        public List<Node> GenerateNodes(Node node)
        {
            List<Node> nodes = new List<Node>();

            if (node.FirstBucket.Filled > 0)
            {
                if (node.SecondBucket.Filled != node.SecondBucket.Capacity)
                    nodes.Add(PourFirstToSecondBucket(node));

                if (node.FirstBucket.Filled != node.FirstBucket.Capacity)
                    nodes.Add(FillFirstBucket(node));

                nodes.Add(EmptyFirstBucket(node));
            }
            else
                nodes.Add(FillFirstBucket(node));

            if (node.SecondBucket.Filled > 0)
            {
                if (node.FirstBucket.Filled != node.FirstBucket.Capacity)
                    nodes.Add(PourSecondToFirstBucket(node));

                if (node.SecondBucket.Capacity != node.SecondBucket.Filled)
                    nodes.Add(FillSecondBucket(node));

                nodes.Add(EmptySecondBucket(node));
            }
            else
                nodes.Add(FillSecondBucket(node));

            return nodes;
        }
        public Node Algoritm(Node node)
        {
            List<Node> nodes = GenerateNodes(node);
            while (true)
            {
                List<Node> temp = new List<Node>();

                foreach (var item in nodes)
                {
                    temp.AddRange(GenerateNodes(item));
                }
                temp.RemoveAll(x => (x.FirstBucket.Filled == 9 && x.SecondBucket.Filled == 5) ||
                (x.FirstBucket.Filled == 0 && x.SecondBucket.Filled == 0));
                nodes = temp;
                foreach (var item in nodes)
                {
                    if (IsGoal(item))
                    {
                        return item;
                    }
                }
            }
        }
        static bool IsGoal(Node node)
        {
            var result = 3;
            return node.FirstBucket.Filled == result || node.SecondBucket.Filled == result;
        }
        public Node FillFirstBucket(Node node)
        {
            return new Node()
            {
                Prev = node,
                FirstBucket = new Bucket() { Capacity = 9, Filled = 9 },
                SecondBucket = new Bucket() { Capacity = node.SecondBucket.Capacity, Filled = node.SecondBucket.Filled },
                Action = "Fill the first Bucket     "
            };
        }
        public Node FillSecondBucket(Node node)
        {
            return new Node()
            {
                Prev = node,
                FirstBucket = new Bucket() { Capacity = node.FirstBucket.Capacity, Filled = node.FirstBucket.Filled },
                SecondBucket = new Bucket() { Capacity = 5, Filled = 5 },
                Action = "Fill the second Bucket"
            };
        }
        public Node EmptyFirstBucket(Node node)
        {
            return new Node()
            {
                Prev = node,
                FirstBucket = new Bucket() { Capacity = node.FirstBucket.Capacity, Filled = 0 },
                SecondBucket = new Bucket() { Capacity = node.SecondBucket.Capacity, Filled = node.SecondBucket.Filled },
                Action = "Empty the first bucket"
            };
        }
        public Node EmptySecondBucket(Node node)
        {
            return new Node()
            {
                Prev = node,
                FirstBucket = new Bucket() { Capacity = node.FirstBucket.Capacity, Filled = node.FirstBucket.Filled },
                SecondBucket = new Bucket() { Capacity = node.SecondBucket.Capacity, Filled = 0 },
                Action = "Empty the second bucket    "
            };
        }

        public Node PourFirstToSecondBucket(Node node)
        {
            var firstBucket = new Bucket() { Capacity = node.FirstBucket.Capacity };
            var secondBucket = new Bucket() { Capacity = node.SecondBucket.Capacity };

            var freeSpaceInSecond = node.SecondBucket.Capacity - node.SecondBucket.Filled;

            if (node.FirstBucket.Filled >= freeSpaceInSecond)
            {
                secondBucket.Filled = 5;
                firstBucket.Filled = node.FirstBucket.Filled - freeSpaceInSecond;
            }
            else
            {
                secondBucket.Filled += node.FirstBucket.Filled;
                firstBucket.Filled = 0;
            }

            return new Node()
            {
                Prev = node,
                FirstBucket = firstBucket,
                SecondBucket = secondBucket,
                Action = "Pour First To Second Bucket"
            };
        }
        public Node PourSecondToFirstBucket(Node node)
        {
            var firstBucket = new Bucket() { Capacity = node.FirstBucket.Capacity };
            var secondBucket = new Bucket() { Capacity = node.SecondBucket.Capacity };

            var freeSpaceInFirst = node.FirstBucket.Capacity - node.FirstBucket.Filled;

            if (node.SecondBucket.Filled >= freeSpaceInFirst)
            {
                firstBucket.Filled = 9;
                secondBucket.Filled = node.SecondBucket.Filled - freeSpaceInFirst;
            }
            else
            {
                firstBucket.Filled += node.SecondBucket.Filled;
                secondBucket.Filled = 0;
            }

            return new Node()
            {
                Prev = node,
                FirstBucket = firstBucket,
                SecondBucket = secondBucket,
                Action = "Pour Second To First Bucket"
            };
        }
    }
}
