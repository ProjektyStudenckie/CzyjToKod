using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graf
{
    public class Point
    {
        private int _iD;
        public int iD
        {
            get { return _iD; }
            set { _iD = value; }

        }

        private bool _used=false;
        public bool used
        {
            get { return _used; }
            set
            {
                _used = value;
            }
        }
        private int _pozX;
        public int pozX
        {
            get { return _pozX; }
            set { _pozX = value; }
            
        }
        private int _pozY;
        public int pozY
        {
            get { return _pozY; }
            set { _pozY = value; }

        }

        private double _wartosc;

        public double wartosc
        {
            get { return _wartosc; }
            set { _wartosc = value; }
        }

        private int _value;

        public int value
        {
            get { return _value; }
            set { _value = value; }
        }

        private List<Point> _neighbours = new List<Point>();

        public List<Point> neighbours
        {
            get { return _neighbours; }
            set { _neighbours.AddRange(value); }
        }

        private List<Way> _ways = new List<Way>();

        public List<Way> ways
        {
            get { return _ways; }
            set { _ways.AddRange(value); }
        }


        private Path _path;
        public Path path
        {
            get { return _path; }
            set { _path = value; }
        }

        public Point(int pozX, int pozY)
        { 
            this.pozX = pozX;
            this.pozY = pozY;
            this.value = 10000;
            
        }
        private Point _owner;
        public Point owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public Point(int value)
        {
            this.value = value;

        }

        public override string ToString() {
            return "Point pozX: " + pozX.ToString() + " pozY: " + pozY.ToString();
        }


        public void Dodaj(Point punkt)
        {

            if (!neighbours.Contains(punkt)){
                neighbours.Append(punkt);
                
            }

        }

        public void ADDChild(List<Point> points)
        {
            neighbours = points;
        }
        

        public void Usun(Point punkt)
        {
            if (neighbours.Contains(punkt))
            {
                neighbours.Remove(punkt);

            }

        }

        public Double Value(Point end)
        {
            Double value = Math.Sqrt(Math.Pow(end.pozX - this.pozX,2) + Math.Pow(end.pozY - this.pozY,2));
            return value;


        }

        public Way ReturnWay(Point end)
        {
            for(int i = 0; i < ways.Count; i++)
            {
                if (ways[i].end == end)
                {
                    return ways[i];
                }
            }

            return null;
        }
        

       
    }
}
