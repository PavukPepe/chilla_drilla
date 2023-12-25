using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Propizdation_AKA_10_practos.Menu;

namespace Propizdation_AKA_10_practos
{
    internal class Kassir : ICRUD
    {
        List<Buh_notes> buh_notes = Reader.Read<List<Buh_notes>>("Buh_notes.json");
        List<Product_kass> products = Reader.Read<List<Product_kass>>("products.json");

        

        public void Action()
        {
            Authorization.Welcome();
            int pos = 2;
            foreach (Product_kass prod in products)
            {
                Product_info(prod, true, true, pos);
                pos++;
            }

            int pol = Menu.Show(2, pos - 3);
            if (pol == (int)klavishi.Escape)
            {
                Authorization.Author();
            }
            else
            {
                try
                {
                    Read(pol);
                }
                catch
                {
                    Action();
                }
            }
        }
        public void Read(int pol)
        {
            var kek = products[pol];
            int p;
            do
            {
                int pos = 2;
                foreach (Product_kass prod in products)
                {
                    Product_info(prod, true, true, pos);
                    pos++;
                }
                p = Menu.Button();
                if (p == (int)klavishi.Plus && kek.kolvo_kass < kek.kolvo)
                {
                    kek.kolvo_kass += 1;
                }
                if (p == (int)klavishi.Minus && kek.kolvo_kass > 0)
                {
                    kek.kolvo_kass -= 1;
                }
            } while (p != (int)klavishi.S);
            List<Product> prods = new List<Product>();
            foreach (Product_kass prod in products)
            {
                prods.Add(new Product(prod.id, prod.name, prod.price, prod.kolvo - prod.kolvo_kass));
                if (prod.kolvo - prod.kolvo_kass < prod.kolvo)
                {
                    int rnd = new Random().Next(1, 1000);
                    buh_notes.Add(new Buh_notes(rnd, $"{prod.name} x {prod.kolvo_kass}", prod.price * prod.kolvo_kass));
                }
            }
            Reader.Write(buh_notes, "buh_notes.json");
            Reader.Write(prods, "products.json");
            products = Reader.Read<List<Product_kass>>("products.json");
            Action();
        }

        public void Product_info(Product_kass kek, bool flag, bool table = false, int pos = 0)
        {
            if (table == false)
            {
                Console.WriteLine($"  ID:          {(kek.id > -1 ? kek.id : "")}");
                Console.WriteLine($"  Наименование:{kek.name}");
                Console.WriteLine($"  Цену:        {(kek.price > -1 ? kek.price : "")}");
                Console.WriteLine($"  Количество:  {(kek.kolvo > -1 ? kek.kolvo : "")}");
            }
            else
            {
                Console.SetCursorPosition(4, pos);
                Console.Write(kek.id);
                Console.SetCursorPosition(Console.WindowWidth / 5, pos);
                Console.Write(kek.name);
                Console.SetCursorPosition(Console.WindowWidth / 5 * 2, pos);
                Console.Write(kek.price);
                Console.SetCursorPosition(Console.WindowWidth / 5 * 3, pos);
                Console.Write($"{kek.kolvo}/{kek.kolvo_kass}"); 
            }
        }
        public void Search() { }
        public void Create() { }
        public void Update(int pol) { }
        public void Delete(int pol) { }
    }
}
