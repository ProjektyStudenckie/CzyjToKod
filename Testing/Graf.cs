using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graf
{
    public class Graf
    {

        private int valueX;
        private int valueY;

        private int _counter = 0;
        public int counter
        {
            get
            {
                _counter++;
                return _counter;
            }
        }


        public Point[,] graf;


        public Graf(int valueX, int valueY)
        {
           
            this.valueX = valueX;
            this.valueY = valueY;
            graf = new Point[valueX, valueY];

            Random rnd = new Random();
            for (int i = 0; i < valueX; i++)
            {
                for (int j = 0; j < valueY; j++)
                {
                   
                    graf[i, j] = new Point( i, j);
                }

            }

            for (int i = 0; i < valueX; i++)
            {
                for (int j = 0; j < valueY; j++)
                {
                    graf[i, j].neighbours = ReturnNeighbours(i, j);
                }

            }
            for (int i = 0; i < valueX; i++)
            {
                
                for (int j = 0; j < valueY; j++)
                {
                    
                    graf[i, j].ways = ReturnWays(i, j,graf[i,j],rnd.Next(10));
                }

            }
        }


        public void PrintGrafAStar()
        {
            String row="";
            for (int i = 0; i < valueY; i++)
            {
                for (int j = 0; j < valueX; j++)
                {
                    row += "[" + graf[i, j].pozX.ToString()+','+ graf[i, j].pozY.ToString() + "], ";
                    /*
                    Console.WriteLine(graf[i, j].ToString());
                    foreach (Punkt x in graf[i, j].neighbours)
                    {
                        Console.WriteLine("Sasiad:  " + x.ToString());
                    }*/
                }
                Console.WriteLine(row);
                row = "";

            }
        }
        public void PrintGrafDijkstar()
        {
            String row = "";
            String row2 = "  ";
            bool ch = false;
            for (int i = 0; i <valueY; i++)
            {
                for (int j = 0; j < valueX; j++)
                {
                    row += "[" + graf[i, j].pozX.ToString() + ',' + graf[i, j].pozY.ToString() + "]";
                    if (j + 1 < valueY && graf[i, j].ReturnWay(graf[i , j+1]) != null) { 
                        row += "-" + graf[i, j].ReturnWay(graf[i, j+1]).value.ToString()+ "-"; 
                    }
                    if (i + 1 < valueY && graf[i, j].ReturnWay(graf[i+1, j]) != null)
                    {
                        row2 += graf[i, j].ReturnWay(graf[i+1, j]).value.ToString()+ "       ";
                        ch = true;
                    }

                }
                Console.WriteLine(row);
                if (ch == true)
                {
                    Console.WriteLine("  |       |       |       |");
                    Console.WriteLine(row2);
                    Console.WriteLine("  |       |       |       |");
                }
                row = "";
                row2 = "  ";
                ch = false;
            }

        }

        public List<Point> ReturnNeighbours(int valueX, int valueY)
        {
            List<Point> returnList = new List<Point>();
            for (int i = -1; i <= 1; i++)
            {
                 if (valueX + i >= 0 && valueX + i < this.valueX )
                    {
                        if (!(i == 0 ))
                        {

                            returnList.Add(graf[valueX + i, valueY]);
                        }
                    }



                
            }
            for (int j = -1; j <= 1; j++)
            {
                if (valueY + j >= 0 && valueY + j < this.valueY)
                {
                    if (!( j == 0))
                    {

                        returnList.Add(graf[valueX , valueY + j]);
                    }
                }



            }

            return returnList;

        }

        public List<Way> ReturnWays(int valueX, int valueY,Point pkt,int x)
        {

            Random rnd = new Random();

            List<Way> returnList = new List<Way>();
           
            foreach (Point i in pkt.neighbours) {
                
                if (i.ReturnWay(pkt) != null)
                {
                    returnList.Add(i.ReturnWay(pkt));
                }
                else
                {
                    if (i != pkt)
                    {
                        Way nowa = new Way(pkt, i, rnd.Next(0,10)+x);
                        returnList.Add(nowa);
                    }
                    
                }

            }

            return returnList;

        }

    }
}
