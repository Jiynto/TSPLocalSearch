using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SalesPersonLab1
{
    public class Route
    {
        private bool isFull = false;
        public bool IsFull { get { return isFull; } }
        public List<int> route;
        private int length;

        public Route(int inputLength)
        {
            route = new List<int>();
            length = inputLength;
        }


        public void AddNode(int node)
        {
            if (!route.Contains(node) && !isFull)
            {
                route.Add(node);
                if (route.Count == length)
                {
                    isFull = true;
                }

            }
        }

    }
}
