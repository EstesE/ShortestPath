using System.Collections.Generic;
using System.Linq;
using System;


namespace ShortestPath
{
    class Program
    {
        static void Main()
        {
            char[] source = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S' };
            var destination = source;
            var str = new List<Storage>();
            var paths = new Stack<Storage>();
            var g = new Graph();

            foreach (var s in source)
            {
                foreach (var d in destination)
                {
                    //Console.WriteLine("From " + s + " to " + d + ".");
                    g.ShortestPath(s, d, new Stack<char>(), new Stack<Array>(), str, g);
                    foreach (var storage in str.ToList())
                    {
                        var lowestValue = str.First().Value;
                        if (lowestValue < storage.Value)
                        {
                            str.Remove(storage);
                        }
                    }
                    if (str.Count > 0 && !(paths.Contains(str.FirstOrDefault())))
                    {
                        if (str.Count > 1)
                        {
                            str.Remove(str.First(c => c.Path.Count() == str.Max(m => m.Path.Count())));
                        }

                        paths.Push(str.FirstOrDefault());
                        str.Clear();
                    }
                }
            }

            // Variables for user input
            ConsoleKeyInfo src;
            ConsoleKeyInfo dst;
            
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("'Esc' to exit the program");
            Console.ResetColor();

            // Get source
            do
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter Source Node: ");
                Console.ResetColor();
                src = Console.ReadKey();
                if (src.Key == ConsoleKey.Escape)
                {
                    return;
                }
            } while (Char.IsLetter(src.KeyChar) != true);

            // Get destination
            do
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter Destination Node: ");
                Console.ResetColor();
                dst = Console.ReadKey();
                if (dst.Key == ConsoleKey.Escape)
                {
                    return;
                }
            } while (Char.IsLetter(dst.KeyChar) != true);

            Console.WriteLine("\n");

            if (src != dst)
            {
                // Display shortest path from source (src) to destination (dst) if we have one
                var shortestPath = paths.Where(x => x.Path.First() == Char.ToUpper(src.KeyChar) && x.Path.Last() == Char.ToUpper(dst.KeyChar)).ToArray();
                if (shortestPath.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Shortest path from ");
                    Console.ResetColor();
                    Console.Write(Char.ToUpper(src.KeyChar));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" to ");
                    Console.ResetColor();
                    Console.Write(Char.ToUpper(dst.KeyChar));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" is: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(shortestPath.First().Path);
                    Console.Write(" (");
                    Console.Write(shortestPath.First().Value);
                    Console.Write(")");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please pick points from A to S inclusively");
                }

            }
            else if (src == dst)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Shortest Path is: ");
                Console.ResetColor();
                Console.Write(src.KeyChar + " - 0");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR");
            }

            Console.WriteLine("\n");

            // Run again...(recursive call)
            Main();
        }


    }
}
