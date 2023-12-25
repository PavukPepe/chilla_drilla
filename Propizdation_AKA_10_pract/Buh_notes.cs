using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Propizdation_AKA_10_practos
{
    internal class Buh_notes
    {
        public int id;
        public string name;
        public DateTime date;
        public double money;
        public bool prihod;

        public Buh_notes()
        {
            this.id = -1;
            this.money = -1;
        }

        public Buh_notes(int id, string name, double money)
        {
            this.id = id;
            this.name = name;
            this.money = money;
            this.prihod = true;
            this.date = DateTime.Now;
        }

    }
}
