using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graf
{
    class Dijkstra
    {


        public List<Path> paths = new List<Path>();


        public void GetPath(Point start, Point end)
        {
            Point pkt2;
            foreach (Way x in start.ways)
            {

                if (x.start == start)
                {
                    pkt2 = x.end;
                }
                else
                {
                    pkt2 = x.start;
                }


                paths.Add(new Path(start, pkt2,x.value));
            }

            Path(end);
            





        }
        public void Path(Point end)
        {
            int x = 0;
            Path subPath;
            List<Path> subPaths = new List<Path>();

            for (int i = paths.Count - 1; i >= 0; i--)
            {
                

                Point pkt2;
                foreach (Way w in paths[i].lastPoint.ways)
                {

                    if (w.start == paths[i].lastPoint)
                    {
                        pkt2 = w.end;
                    }
                    else
                    {
                        pkt2 = w.start;
                    }
                    if (!paths[i].path.Contains(pkt2))
                    {
                        if (paths[i].lastPoint != end)
                        {
                            if (pkt2.value > paths[i].wayValue + w.value)
                            {
                                x++;


                                subPath = new Path(paths[i], pkt2, w.value);

                                pkt2.path = subPath;
                                pkt2.value = paths[i].wayValue + w.value;
                                subPaths.Add(subPath);


                            }
                        }
                    }






                }
                if (paths[i].lastPoint != end) {
                    paths.RemoveAt(i);
                }
            }
            paths.AddRange(subPaths);

           
            if (!PathEnd(end)) { 
                Path(end); }



        }


        public bool PathEnd(Point end)
        {
            for(int i = 0; i < paths.Count; i++)
            {
                if (paths[i].lastPoint != end)
                {
                    return false;
                    
                }
               
                
                
            }
            
         return true;

        }
    }
}
