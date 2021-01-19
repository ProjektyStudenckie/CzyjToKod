using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graf
{
    public class Tree
    {
        

        private Point _start;
        public Point start
        {
            get { return _start; }
            set
            {
                _start = value;

            }
        }
       public Tree(int value)
        {
            start = new Point(value);
        }

       public void TreeBuilderADD(List<Point> Points)
        {
            start.ADDChild(Points);
            foreach (Point x in Points)
            {
                x.owner =start;
            }
        }

        public void TreeBuilderADD(List<Point> Points, int value)
        {
            Point point =DFS(value);
            if (point != null) { 
                point.ADDChild(Points);
                foreach(Point x in Points)
                {
                    x.owner = point;
                }
            }
            
        }

        private Point getChildWithValue(List<Point> points, int value)
        {
            

            if (points.Count > 0)
            {
                List<Point> sublist = new List<Point>();


                foreach (Point x in points)
                {
                    
                    if (x.value == value)
                    {
                        return x;
                        
                    }
                    else if (x.neighbours.Count > 0)
                    {

                        sublist.AddRange(x.neighbours);

                    }
                }

                return getChildWithValue(sublist, value);
            }
            return null;

        }
        public void printTree()
        { 
            if (start.neighbours.Count > 0)
            {
                Console.WriteLine(start.value.ToString());
                PrintReq(start.neighbours);
            }
        }
        

        private void PrintReq(List<Point> points)
        {
           
            if (points.Count > 0)
            {
                Console.WriteLine("stage: ");
                List<Point> sublist = new List<Point>();

                foreach (Point x in points)
                {
                    Console.WriteLine(x.value.ToString()+"owner: "+ x.owner.value.ToString());


                    sublist.AddRange(x.neighbours);


                }
                PrintReq(sublist);
            }
        }

      


        public Point DFS(int value)
        {
            Point point = DFSreq(start.neighbours, value);
            return point;
            

        }

        private Point DFSreq(List<Point> points, int value)
        {


            if (points.Count > 0)
            {

                foreach (Point x in points)
                {

                    if (x.value == value)
                    {
                        return x;

                    }
                    else if(DFSreq(x.neighbours, value) != null) {

                        return DFSreq(x.neighbours, value);



                    }
                }

                
            }
            return null;

        }
    }
}
