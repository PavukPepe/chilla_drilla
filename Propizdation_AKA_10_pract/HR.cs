using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Propizdation_AKA_10_practos.Menu;

namespace Propizdation_AKA_10_practos
{
    internal class HR : ICRUD
    {
        List<Employeer> employeers = Reader.Read<List<Employeer>>("employeers.json");
        List<User> users = Reader.Read<List<User>>("users.json");

        public void Action()
        {
            Authorization.Welcome();
            int pos = 2;
            foreach (Employeer employeer in employeers)
            {
                Empl_info(employeer, true, true, pos);
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
            Employeer empl = new Employeer();
            Authorization.Welcome();
            Empl_info(empl, false);

            int p = Show(2, 7);
            Creature(empl, p);
            do
            {
                Authorization.Welcome();
                int pos = 2;
                foreach (var employeer in employeers)
                {
                    if (employeer.id == empl.id || employeer.name == empl.name || employeer.surname == empl.surname || employeer.midlename == empl.midlename || employeer.salary == empl.salary || employeer.passport == empl.passport || employeer.birthday == empl.birthday || employeer.user_id == empl.user_id)
                    Empl_info(employeer, true, true, pos);
                    pos++;
                }
                p = Menu.Button();
            } while (p != (int)klavishi.Escape);
            Action();
        }

        public void Read(int pol)
        {
            Authorization.Welcome();
            var kek = employeers[pol];
            Empl_info(kek, true, false, 2);
            int p = Menu.Button();
            if (p == (int)klavishi.Delete)
            {
                Delete(pol);
                Action();
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
            Employeer empl = new Employeer();
            int p;
            do
            {
                Authorization.Welcome();
                Empl_info(empl, false, false, 2);
                p = Menu.Show(2, 7);
                Creature(empl, p);
            } while (p != (int)klavishi.S && p != (int)klavishi.Escape);
            try
            {
                if (empl.id != -1 && empl.salary != -1 && empl.passport != -1 && empl.user_id != -1)
                {
                    employeers.Add(empl);
                    Reader.Write(employeers, "employeers.json");
                }
            }
            finally
            {
                Action();
            }

        }

        static void Creature(Employeer empl, int p)
        {
            try
            {
                switch (p)
                {
                    case 0:
                        empl.id = Convert.ToInt32(Addition(p, empl.id > -1 ? Convert.ToString(empl.id) : ""));
                        break;
                    case 1:
                        empl.name = Convert.ToString(Addition(p, empl.name));
                        break;
                    case 2:
                        empl.surname = Convert.ToString(Addition(p, empl.surname));
                        break;
                    case 3:
                        empl.midlename = Convert.ToString(Addition(p, empl.midlename));
                        break;
                    case 4:
                        string bd = Convert.ToString(Addition(p, ""));
                        string[] date = bd.Split(".");
                        empl.birthday = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0);
                        break;
                    case 5:
                        empl.passport = Convert.ToInt32(Addition(p, empl.passport > -1 ? Convert.ToString(empl.passport) : ""));
                        break;
                    case 6:
                        empl.salary = Convert.ToDouble(Addition(p, empl.salary > -1 ? Convert.ToString(empl.salary) : ""));
                        break;
                    case 7:
                        empl.user_id = Convert.ToInt32(Addition(p, empl.user_id > -1 ? Convert.ToString(empl.user_id) : ""));
                        break;
                }
            }
            catch
            {
                Console.SetCursorPosition(p, 6);
                Console.WriteLine("Неверный ввод!");
            }
        }
        public void Update(int pol)
        {
            Employeer employeer = employeers[pol];
            int p;
            do
            {
                Authorization.Welcome();
                Empl_info(employeer, false, false, 2);
                p = Menu.Show(2, 7);
                Creature(employeer, p);
            } while (p != (int)klavishi.S && p != (int)klavishi.Escape);
            try
            {
                Reader.Write(employeers, "employeers.json");
            }
            finally
            {
                Create();
            }
        }
        public void Delete(int pol)
        {
            employeers.Remove(employeers[pol]);
            Reader.Write(employeers, "employeers.json");
            Action();
        }

        public void Empl_info(Employeer kek, bool read, bool table = false, int pos = 0)
        {
            List<User> sort = new List<User>();
            foreach (User user in users)
            {
                if (user.id == kek.user_id)
                {
                    sort.Add(user);
                }
            }
            if (!table)
            {

                Console.WriteLine($"  ID:               {(kek.id > -1 ? kek.id : "")}");
                Console.WriteLine($"  Имя:              {kek.name}");
                Console.WriteLine($"  Фамилия:          {kek.surname}");
                Console.WriteLine($"  Отчество:         {kek.midlename}");
                Console.WriteLine($"  Дата рождения:    {(kek.birthday.Year > 1 ? kek.birthday.ToLongDateString() : "" )}");
                Console.WriteLine($"  Пасспорт(сер/ном):{(kek.passport> -1 ? kek.passport:"")}");
                if (read)
                {
                    try
                    {
                        Console.WriteLine($"  Должность:        {Enum.GetValues(typeof(Posts)).GetValue(sort[0].post_id)}");
                    }
                    catch
                    {
                        Console.WriteLine("  Должность:        НЕТ");
                    }
                }
                Console.WriteLine($"  Зарплата:         {(kek.salary> -1 ? kek.salary : "")}");
                Console.WriteLine($"  ID польователя:   {(kek.user_id> -1 ? kek.user_id : "")}");
            }
            else
            {
                Console.SetCursorPosition(4, pos);
                Console.Write(kek.id);
                Console.SetCursorPosition(Console.WindowWidth / 5, pos);
                Console.Write(kek.name);
                Console.SetCursorPosition(Console.WindowWidth / 5 * 2, pos);
                Console.Write(kek.midlename);
                Console.SetCursorPosition(Console.WindowWidth / 5 * 3, pos);
                try
                {
                    Console.Write(Enum.GetValues(typeof(Posts)).GetValue(sort[0].post_id));
                }
                catch
                {
                    Console.WriteLine("НЕТ");
                }
            }

        }

        static object Addition(int pol, string v)
        {
            Console.SetCursorPosition(20, pol + 2);
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
    }
}
