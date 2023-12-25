using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propizdation_AKA_10_practos
{
    internal class Product
    {
        public int id;
        public string name;
        public double price;
        public int kolvo;

        public Product(int id, string name, double price, int kolvo)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.kolvo = kolvo;
        }

        public Product()
        {
            this.id = -1;
            this.price = -1;
            this.kolvo = -1;
        }
    }
}
