using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graf
{
    class Program
    { 
        static void Main(string[] args)
        {
            treeBFs();
            
                    
            Console.ReadLine();

        }

        public static void Floyd()
        {
            Graf main = new Graf(4, 4);
            main.PrintGrafAStar();
            var dijkstra = new Floyd(main);
            var path = dijkstra.GetPath(main.graf[0, 0], main.graf[3, 3]);

            for (int i = 0; i < path.Count; i++)
            {
                Console.WriteLine("PozX: " + path[i].pozX.ToString() + "PozY: " + path[i].pozX.ToString() + "\n");
            }


        }


        public static void Dijsktra()
        {
            Graf main = new Graf(4, 4);
            main.PrintGrafDijkstar();
            /*            
            int i=0;
            foreach(Punkt x in main.graf)
            {
                i++;
                Console.WriteLine(i.ToString());
                foreach(Way y in x.ways)
                {
                    Console.WriteLine("start " +y.start.pozX.ToString()+ y.start.pozY.ToString() + "koniec: "+y.end.pozX.ToString() + y.end.pozY.ToString()+"wartosc: " + y.value.ToString());
                }
            }*/
            Dijkstra dijkstra = new Dijkstra();
            dijkstra.GetPath(main.graf[0, 0], main.graf[3, 3]);
            Path path = dijkstra.paths[0];
            foreach (Path x in dijkstra.paths)
            {
                if (path.wayValue > x.wayValue)
                {
                    path = x;
                }

            }
            path.Print();

        }
        public static void treeBFs()
        {
            Tree tree = new Tree(7);
            List<Point> list = new List<Point>(new Point[]{
                new Point(4),
                new Point(3),
                new Point(9)

            });
            tree.TreeBuilderADD(list);

            List<Point> list2 = new List<Point>(new Point[]{
                new Point(1),
                new Point(10),
                new Point(13)

            });

            tree.TreeBuilderADD(list2,4);

            List<Point> list4 = new List<Point>(new Point[]{
                new Point(21),
                new Point(22),
                new Point(23)

            });

            tree.TreeBuilderADD(list4, 3);


            List<Point> list3 = new List<Point>(new Point[]{
                new Point(68),
                new Point(88),
                new Point(99)

            });
            tree.TreeBuilderADD(list3, 22);
            
            tree.printTree();
            Console.WriteLine("program szuka pkt jesli znajdzie to wydrukuje jej value:  ");
            Console.WriteLine(tree.DFS(99).value);

        }
    }
}
