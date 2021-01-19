using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kolejka
{

    public class Element
    {
        private string wartosc;
        private Element nastepny;

        public Element GetNastepny()
        {
            return nastepny;
        }

        public void SetNastepny(Element nowy)
        {
            nastepny = nowy;
        }

        public string GetWartosc()
        {
            return wartosc;
        }

        ~Element()
        {

        }

        public Element(string wartosc)
        {
            this.wartosc = wartosc;
        }
    }
}
