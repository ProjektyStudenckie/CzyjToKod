using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graf
{
    public class Path
    {
        private Point _lastPoint;
        public Point lastPoint
        {
            get { return _lastPoint; }
            set { _lastPoint = value; }
        }
        private int _wayValue;
        public int wayValue
        {
            get { return _wayValue; }
            set { _wayValue = value; }
        }
        public Path(Point Start, Point add,int value)
        {

            wayValue += value;
            add.value = value;
            add.path = this;

            this.path.Add(Start);
            this.path.Add(add);
            this.lastPoint = add;
           
        }

        public Path(Path clone,Point add,int value)
        {
            
            wayValue = value+clone.wayValue;
            add.wartosc = wayValue;
            add.path = this;
            this.path.AddRange(clone.path);
            this.path.Add(add);
            this.lastPoint = add;



     }

        
        public List<Point> path = new List<Point>();

        
       public void Print()
        {
            foreach(Point x in path)
            {
                Console.WriteLine("Punkty: ["+x.pozX.ToString()+"]"+ "[" + x.pozY.ToString() + "]");
            }
            Console.WriteLine(wayValue.ToString());
        }
    }
}
