using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;


namespace SalesPersonLab2
{
    public static class LocalSearch
    {


        public static Route Search(Func<RouteGraph, Route> Initialise, Func<List<Route>, Route, RouteGraph, Route> Step, Func<Route, List<Route>> Neighbourhood, RouteGraph graph, int timeout)
        {
            Route currentBest = Initialise(graph);

            var sw = new Stopwatch();
            sw.Start();

            while (sw.ElapsedMilliseconds < timeout)
            {
                List<Route> currentHood = Neighbourhood(currentBest);

                currentBest = Step(currentHood, currentBest, graph);
            }
            sw.Stop();
            return currentBest;

        }


        public static Route BestImprover(List<Route> neighbourhood, Route currentBest, RouteGraph graph)
        {
            foreach (Route currentRoute in neighbourhood)
            {
                if (graph.RouteCost(currentRoute) < graph.RouteCost(currentBest))
                {
                    currentBest = currentRoute;
                }
            }
            return currentBest;
        }
    }
}
