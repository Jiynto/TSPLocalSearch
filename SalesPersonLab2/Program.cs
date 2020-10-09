using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
using System.Diagnostics;

//using System.Timers;

namespace SalesPersonLab1
{
    class Program
    {

        //private static Timer aTimer;

        //private static Route CurrentBest;


        private static RouteGraph ParseTextFile(string fileName)
        {
            var nodes = new List<string>();
            var points = new List<Vector2>();
            StreamReader reader = File.OpenText(fileName);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (!line.StartsWith("\""))
                {

                    string[] elements = line.Split(",");
                    nodes.Add(elements[0]);
                    points.Add(new Vector2(float.Parse(elements[1]),
                                              float.Parse(elements[2])));
                }
            }

            RouteGraph newGraph = new RouteGraph(nodes.ToArray());

            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    newGraph.AddArc((nodes[i], nodes[j], Vector2.Distance(points[i], points[j])));
                }
            }

            return newGraph;
        }



        private static Route Initialise(RouteGraph graph)
        {
            /*
            aTimer = new Timer();
            aTimer.Interval = 2000;

            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;
            */
            var sw = new Stopwatch();

            Route currentBest = graph.RandomRoute();

            sw.Start();
            while (sw.ElapsedMilliseconds < 300000)
            {
                Route newRoute = graph.RandomRoute();
                if(graph.RouteCost(newRoute) < graph.RouteCost(currentBest))
                {
                    currentBest = newRoute;
                }
            }
            sw.Stop();

            return currentBest;
        }

        /*
        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            aTimer.Enabled = false;
        }
        */


        public static Route BestNeighbour(Route currentBest, RouteGraph graph)
        {
            List<Route> currentHood = graph.GetNeighbourhood(currentBest);

            foreach(Route currentRoute in currentHood)
            {
                if(graph.RouteCost(currentRoute) < graph.RouteCost(currentBest))
                {
                    currentBest = currentRoute;
                }
            }

            return currentBest;


        }


        static void Main(string[] args)
        {
            
            /*

            string[] nodes = new string[] { "A", "B", "C", "D" };
            RouteGraph firstGraph = new RouteGraph(nodes);

            firstGraph.AddArc(("A", "B", 20));
            firstGraph.AddArc(("A", "C", 42));
            firstGraph.AddArc(("A", "D", 35));
            firstGraph.AddArc(("B", "C", 30));
            firstGraph.AddArc(("B", "D", 34));
            firstGraph.AddArc(("C", "D", 12));

            firstGraph.PrintMatrix();

            Route routey = firstGraph.RandomRoute();

            routey.route.ForEach(i => Console.Write("{0}\t", i));
            Console.WriteLine(firstGraph.RouteCost(routey));

            */

            
            RouteGraph graph = ParseTextFile("ulysses16.csv");
            graph.PrintMatrix();


            Route routey = Initialise(graph);

            Console.WriteLine();
            routey.route.ForEach(i => Console.Write("{0}\t", i));
            Console.WriteLine();
            Console.WriteLine(graph.RouteCost(routey));


        }
    }
}
