using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graf
{
    public class Floyd
    {
      
        public Graf graf { get; set; }
        public Floyd(Graf graf)
        {
            this.graf = graf;
        }

        public List<Point> GetPath(Point start,Point end)
        {
 
            List<Point> path = new List<Point>();
            path.Add(start);
            Path(start, end, path);
            return path;
                      
        }
        public void Path(Point pkt, Point end, List<Point> path)
        {
            Point lowestValue = null;

            for (int i = 0; i < pkt.neighbours.Count(); i++)
            {
                if (lowestValue == null)
                {
                    pkt.neighbours[i].wartosc = pkt.neighbours[i].Value(end);
                    lowestValue = pkt.neighbours[i];
                   

                }
                else if(lowestValue.wartosc> pkt.neighbours[i].Value(end))
                {
                    pkt.neighbours[i].wartosc = pkt.neighbours[i].Value(end);
                    lowestValue = pkt.neighbours[i];
                }
            }
            path.Add(lowestValue);
            if (lowestValue != end)
            {
                Path(lowestValue, end, path);
            }
            
           
        }
    }
}