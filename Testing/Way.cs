using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graf
{
    public class Way
    {
        public Way(Point start,Point end, int value)
        {
            this.start = start;
            this.end = end;
            this.value = value;
        }
        private Point _start;
        public Point start
        {
            get
            {
                return _start;
            }
            set { _start = value; }

        }
        private Point _end;
        public Point end
        {
            get
            {
                return _end;
            }
            set { _end = value; }

        }

        private int _value;
        public int value
        {
            get
            {
                return _value;
            }
            set { _value = value; }

        }
    }

}
