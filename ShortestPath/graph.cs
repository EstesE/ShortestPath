using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortestPath
{
    public class Graph
    {
        public int Count = 0;
        private Dictionary<char, Dictionary<char, int>> _graph;
        public Graph()
        {
            _graph = new Dictionary<char, Dictionary<char, int>>
            {
                {'A', new Dictionary<char, int> {{'B', 70}, {'I', 100}}},
                {'B', new Dictionary<char, int> {{'A', 70}, {'C', 10}, {'D', 25}}},
                {'C', new Dictionary<char, int> {{'B', 10}, {'D', 15}, {'E', 5}}},
                {'D', new Dictionary<char, int> {{'B', 25}, {'C', 15}, {'G', 45}}},
                {'E', new Dictionary<char, int> {{'C', 5}, {'F', 5}}},
                {'F', new Dictionary<char, int> {{'E', 5}, {'I', 35}}},
                {'G', new Dictionary<char, int> {{'D', 45}, {'H', 10}}},
                {'H', new Dictionary<char, int> {{'G', 10}, {'M', 45}, {'N', 160}}},
                {'I', new Dictionary<char, int> {{'A', 100}, {'F', 35}, {'K', 30}}},
                {'J', new Dictionary<char, int> {{'K', 20}, {'L', 7}}},
                {'K', new Dictionary<char, int> {{'I', 30}, {'J', 20}, {'L', 90}}},
                {'L', new Dictionary<char, int> {{'J', 7}, {'K', 90}, {'N', 60}}},
                {'M', new Dictionary<char, int> {{'H', 45}, {'N', 210}}},
                {'N', new Dictionary<char, int> {{'H', 160}, {'L', 60}, {'M', 210}, {'P', 12}}},
                {'O', new Dictionary<char, int> {{'P', 180}, {'Q', 65}, {'S', 300}}},
                {'P', new Dictionary<char, int> {{'N', 12}, {'O', 180}, {'S', 10}}},
                {'Q', new Dictionary<char, int> {{'O', 65}, {'R', 10 }, {'S', 75}}},
                {'R', new Dictionary<char, int> {{'Q', 10}, {'S', 60}}},
                {'S', new Dictionary<char, int> {{'O', 300}, {'P', 10}, {'Q', 75}, {'R', 60}}}
            };
        }

        public void ShortestPath(char src, char dst, Stack<char> visitedNodes, Stack<Array> paths, List<Storage> str, Graph g)
        {
            // If visitedNodes contains our destination and our destination is the first element then push it to our paths variable
            if (visitedNodes.Contains(dst) && visitedNodes.First() == dst)
            {
                Count++;
                var totalDistance = 0;
                var fromNode = new char();
                
                foreach (var visitedNode in visitedNodes.Reverse())
                {
                    var toNode = visitedNode;
                    
                    //Console.Write(visitedNode);
                    if (Char.IsLetter(fromNode) && fromNode != toNode)
                    {
                        totalDistance = totalDistance + g._graph[fromNode].Where(x => x.Key == toNode).Select(x => x.Value).FirstOrDefault();
                    }
                    fromNode = visitedNode;
                }

                // Print paths
                //if (visitedNodes.Last().ToString() == "B" && visitedNodes.First().ToString() == "D")
                //{
                //    Console.Write(visitedNodes.Reverse().ToArray());
                //    Console.Write(" - " + totalDistance + "\n");
                //}

                paths.Push(visitedNodes.ToArray());
                var storage = new Storage
                {
                    Path = new string(visitedNodes.Reverse().ToArray()),
                    Value = totalDistance
                };

                if (str.Count > 1)
                {
                    str.Remove(str.First(x => x.Value == str.Max(y => y.Value)));
                }
                str.Add(storage);
            }

            if (!visitedNodes.Contains(src))
            {
                visitedNodes.Push(src);

                if (visitedNodes.Contains(dst))
                {
                    return;
                }
            }

            var neighbors = g._graph.FirstOrDefault(y => y.Key == src);
            foreach (var node in neighbors.Value)
            {
                if (!visitedNodes.Contains(node.Key))
                {
                    visitedNodes.Push(node.Key);
                    src = node.Key;
                    // Recursive call
                    ShortestPath(src, dst, visitedNodes, paths, str, g);
                    visitedNodes.Pop();
                }
                
            }
        }
    }
}
