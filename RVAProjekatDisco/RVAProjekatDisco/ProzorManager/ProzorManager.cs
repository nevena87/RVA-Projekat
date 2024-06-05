using RVAProjekatDisco.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RVAProjekatDisco.WindowManager
{
    public class ProzorManager : IProzorManager
    {
        Stack<Window> stek = new Stack<Window>();

        public ProzorManager(Window roditelj)
        {
            stek.Push(roditelj);
        }

        public void PrethodnaStrana()
        {
            Window top = stek.Pop();
            top.Closed -= TopWindowClosed;
            top.Close();
            stek.Peek().Show();
        }

        public void PrikaziStranu(StanjeProzora sledecaStrana)
        {
            switch (sledecaStrana)
            {
                case StanjeProzora.Pocetna:
                    PrikaziSledecuStranu(new Pocetna(this));
                    break;

                case StanjeProzora.DodajKorisnika:
                    PrikaziSledecuStranu(new DodajKorisnika(this));
                    break;

                case StanjeProzora.IzmeniPodatkeKorisnika:
                    PrikaziSledecuStranu(new IzmeniPodatkeKorisnika(this));
                    break;
                case StanjeProzora.Plejliste:
                    PrikaziSledecuStranu(new Plejliste(this));
                    break;

                case StanjeProzora.Pesme:
                    PrikaziSledecuStranu(new Pesme(this));
                    break;
            }
        }

        private void PrikaziSledecuStranu(Window sledecaStrana)
        {
            stek.Peek().Visibility = Visibility.Hidden;
            sledecaStrana.Closed += TopWindowClosed;
            stek.Push(sledecaStrana);
            sledecaStrana.Show();
        }

        private void TopWindowClosed(object sender, EventArgs e)
        {
            stek.Pop().Closed -= TopWindowClosed;
            stek.Peek().Show();
        }
    }
}
