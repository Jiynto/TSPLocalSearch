using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SalesPersonLab2
{
    public class Initialisations
    {
        public static Route RandomInitialise(RouteGraph graph)
        {
            var sw = new Stopwatch();

            Route currentBest = graph.RandomRoute();

            sw.Start();
            while (sw.ElapsedMilliseconds < 5000)
            {
                Route newRoute = graph.RandomRoute();
                if (graph.RouteCost(newRoute) < graph.RouteCost(currentBest))
                {
                    currentBest = newRoute;
                }
            }
            sw.Stop();

            return currentBest;
        }
    }
}
