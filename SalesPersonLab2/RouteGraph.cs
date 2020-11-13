using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SalesPersonLab2
{
    public class RouteGraph
    {

        private double?[,] adMat;
        private int nodeNumber;
        private string[] nodes;


        public RouteGraph(string[] inputNodes)
        {
            nodes = inputNodes;
            nodeNumber = nodes.Length;

            adMat = new double?[nodeNumber, nodeNumber];

        }

        public void AddArc(ValueTuple<string, string, double> newArc)
        {
            //if (Char.IsLetter(newArc.Item1)) newArc.Item1 = Char.ToUpper(newArc.Item1);
            //if (Char.IsLetter(newArc.Item2)) newArc.Item2 = Char.ToUpper(newArc.Item2);
            if (Array.IndexOf(nodes, newArc.Item1) != -1 && Array.IndexOf(nodes, newArc.Item2) != -1)
            {
                adMat[Array.IndexOf(nodes, newArc.Item1), Array.IndexOf(nodes, newArc.Item2)] = newArc.Item3;
                adMat[Array.IndexOf(nodes, newArc.Item2), Array.IndexOf(nodes, newArc.Item1)] = newArc.Item3;
            }
        }



        public void PrintMatrix()
        {
            for (int i = 0; i < adMat.GetLength(0); i++)
            {
                for (int j = 0; j < adMat.GetLength(1); j++)
                {
                    if (adMat[i, j] == null)
                    {
                        Console.Write("\t");
                    }
                    else
                    {
                        Console.Write(String.Format("{0:0.000\t}",adMat[i, j]));
                    }

                    if (j == adMat.GetLength(0) - 1)
                    {
                        Console.Write("\n");
                    }
                }
            }
        }


        public Route RandomRoute()
        {
            Route newRoute = new Route(nodeNumber);

            Random randy = new Random();

            while (newRoute.IsFull == false)
            {

                int input = randy.Next(0, nodeNumber);
                newRoute.AddNode(input);
            }

            return newRoute;


            /*
            newRoute.route.ForEach(i => Console.Write("{0}\t", i));
            Console.WriteLine();

            Console.WriteLine(RouteCost(newRoute));

            */

        }


        public List<Route> GetTwoOptNeighbourhood(Route inputRoute)
        {
            List<Route> Neighbourhood = new List<Route>();

            for(int i = 0; i < inputRoute.route.Count - 1; i++)
            {
                for(int j = 0; j < inputRoute.route.Count; j++)
                {
                    if(i != j)
                    {
                        Route newRoute = new Route(inputRoute.route.Count);
                        List<int> tempRoute = new List<int>(inputRoute.route);
                        int temp = tempRoute[i];
                        tempRoute[i] = tempRoute[j];
                        tempRoute[j] = temp;
                        newRoute.route = tempRoute;
                        if(!Neighbourhood.Contains(newRoute))
                        {
                            Neighbourhood.Add(newRoute);
                        }

                    }
                   
                }
            }

            return Neighbourhood;
        }





        public double RouteCost(Route inputRoute)
        {
            double cost = 0;

            for (int i = 0; i < inputRoute.route.Count -1; i++)
            {
                cost += (double)adMat[inputRoute.route[i], inputRoute.route[i + 1]];
            }
            cost += (double)adMat[inputRoute.route[inputRoute.route.Count - 1], inputRoute.route[0]];

            return cost;
        }



    }
}
