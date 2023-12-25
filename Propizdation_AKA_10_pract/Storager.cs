using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static Propizdation_AKA_10_practos.Menu;

namespace Propizdation_AKA_10_practos
{
    internal class Storager : ICRUD
    {
        List<Product> products = Reader.Read<List<Product>>("products.json");
        public void Action()
        {
            Authorization.Welcome();
            int pos = 2;
            foreach (Product prod in products)
            {
                Product_info(prod, true, true, pos);
                pos++;
            }

            int pol = Menu.Show(2, pos - 3);

            if (pol == (int)klavishi.F1)
            {
                Create();
            }
            else if (pol == (int)klavishi.F2)
            {
                Search();
            }
            else if (pol == (int)klavishi.Escape)
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
        public void Search()
        {
            Product prod = new Product();
            Authorization.Welcome();
            Product_info(prod, false);

            int p = Menu.Show(2, 3);
            Creature(prod, p);
            do
            {
                Authorization.Welcome();
                int pos = 2;
                foreach (Product prod1 in products)
                {
                    if (prod1.id == prod.id || prod.kolvo == prod1.kolvo || prod.name == prod1.name || prod.price == prod1.price)
                    {
                        Product_info(prod1, true, true, pos);
                        pos++;
                    }
                }
                p = Menu.Button();
            } while (p != (int)klavishi.Escape);
            Action();

        }

        public void Read(int pol)
        {
            var kek = products[pol];
            Authorization.Welcome();
            Product_info(kek, true);
            int p = Menu.Button();
            if (p == (int)klavishi.Delete)
            {
                Delete(pol);
            }
            if (p == (int)klavishi.F10)
            {
                Update(pol);
            }
            if (p == (int)klavishi.Escape)
            {
                Action();
            }
        }
        public void Create()
        {
            var prod = new Product();
            int p;
            do
            {
                Authorization.Welcome();
                Product_info(prod, false);
                p = Menu.Show(2, 3);
                Creature(prod, p);
            } while (p != (int)klavishi.S && p != (int)klavishi.Escape);
            try
            {
                if (prod.id != -1 && prod.kolvo != -1 && prod.price != -1)
                {
                    products.Add(prod);
                    Reader.Write(products, "products.json");
                }
            }
            finally
            {
                Action();
            }
        }
        public void Update(int pol)
        {
            Product prod = products[pol];
            int p;
            do
            {
                Console.Clear();
                Authorization.Welcome();
                Product_info(prod, false);
                p = Menu.Show(2, 3);
                Creature(prod, p);
            } while (p != (int)klavishi.S && p != (int)klavishi.Escape);
            try
            {
                Reader.Write(products, "products.json");
            }
            finally
            {
                Action();
            }
        }
        public void Delete(int pol)
        {
            products.Remove(products[pol]);
            Reader.Write(products, "products.json");
            Action();
        }

        static object Addition(int pol, string v)
        {
            Console.SetCursorPosition(15, pol + 2);
            ConsoleKeyInfo n;
            Console.Write(v);
            do
            {
                n = Console.ReadKey();
                if (n.Key == ConsoleKey.Backspace)
                {
                    if (v.Length == 0)
                        break;
                    v = v.Substring(0, v.Length - 1);
                    Console.Write(' ');
                    Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, pol + 2);
                    continue;
                }
                if (n.KeyChar != '\r' && n.KeyChar != '\0')
                {
                    v += n.KeyChar.ToString();
                }
            } while (n.Key != ConsoleKey.DownArrow && n.Key != ConsoleKey.UpArrow && n.Key != ConsoleKey.Enter);
            return v;
        }

        static void Creature(Product kek, int p)
        {
            try
            {
                switch (p)
                {
                    case 0:
                        kek.id = Convert.ToInt32(Addition(p, kek.id > -1 ? Convert.ToString(kek.id) : ""));
                        break;
                    case 1:
                        kek.name = Convert.ToString(Addition(p, kek.name));
                        break;
                    case 2:
                        kek.price = Convert.ToDouble(Addition(p, kek.price > -1 ? Convert.ToString(kek.price) : ""));
                        break;
                    case 3:
                        kek.kolvo = Convert.ToInt32(Addition(p, kek.kolvo > -1 ? Convert.ToString(kek.kolvo) : ""));
                        break;
                }
            }
            catch
            {
                Console.SetCursorPosition(p, 6);
                Console.WriteLine("Неверный ввод!");
            }
        }

        public void Product_info(Product kek, bool flag, bool table = false, int pos = 0)
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
                Console.Write(kek.kolvo);
            }
        }
    }
}
