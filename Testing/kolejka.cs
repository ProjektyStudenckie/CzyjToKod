using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kolejka
{
    public class Kolejka
    {
        Element Ostatni;
        Element Pierwszy;

        public void Dodaj(Element nowy)
        {
            if (!Sprawdzenie())
            {
                Pierwszy = nowy;
                Ostatni = nowy;
            }
            else
            {
                Ostatni.SetNastepny(nowy);
                Ostatni = nowy;
            }

            
        }

        public void Usun()
        {
            if (Sprawdzenie())
            {
                if (Pierwszy == Ostatni)
                {
                    Ostatni = null;
                    Pierwszy = null;
                }
                else
                {


                    Pierwszy = Pierwszy.GetNastepny();
                }

            }
            
            
        }
        public void Wyswietl()
        {
            Element pomocniczy;
            Console.WriteLine("Elementy: ");
            pomocniczy = Pierwszy;
            while (pomocniczy != null)
            {
                Console.WriteLine("Nazwa elementu: " + pomocniczy.GetWartosc());
                pomocniczy = pomocniczy.GetNastepny();
            }
        }

        public bool Sprawdzenie()
        {
            if (Pierwszy == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void WyswietlUsun()
        {
            Element pomocniczy;
            pomocniczy = Pierwszy.GetNastepny();
            Console.WriteLine("Usuniety element: ");
            Console.WriteLine("Nazwa elementu: " + Pierwszy.GetWartosc());
            Pierwszy = pomocniczy;

        }
    }
}
