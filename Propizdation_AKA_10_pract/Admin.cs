using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Propizdation_AKA_10_practos.Menu;

namespace Propizdation_AKA_10_practos
{
    internal class Admin : ICRUD
    {
        List<User> users = Reader.Read<List<User>>("users.json");

        public void Action()
        {
            Authorization.Welcome();
            int pos = 2;
            foreach (User user in users)
            {
                User_info(user, true, true, pos);
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
            User user = new User();
            Authorization.Welcome();
            User_info(user, false);

            int p = Menu.Show(2, 3);
            Creature(user, p);
            do
            {
                Authorization.Welcome();
                int pos = 2;
                foreach (User user1 in users)
                {
                    if (user1.id == user.id || user1.password == user.password || user1.post_id == user.post_id || user1.login == user.login)
                    {
                        User_info(user1, true, true, pos);
                        pos++;
                    }
                }
                p = Menu.Button();
            } while (p != (int)klavishi.Escape);
            Action();

        }
         
        public void Read(int pol)
        {
            var kek = users[pol];
            Authorization.Welcome(); 
            User_info(kek, true);
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
            var user = new User();
            int p;
            do
            {
                Authorization.Welcome();
                User_info(user, false);
                p = Menu.Show(2, 3);
                Creature(user, p);
            } while (p != (int)klavishi.S && p != (int)klavishi.Escape);
            try
            {
                if (user.id != -1 && user.post_id != -1)
                {
                    users.Add(user);
                    Reader.Write(users, "users.json");
                }
            }
            finally
            {
                Action();
            }
        }
        public void Update(int pol)
        {
            User user = users[pol];
            int p;
            do
            {
                Console.Clear();
                Authorization.Welcome(); 
                User_info(user, false);
                p = Menu.Show(2, 3);
                Creature(user, p);   
            } while (p != (int)klavishi.S && p != (int)klavishi.Escape);
            try
            {
                    Reader.Write(users, "users.json");
            }
            finally
            {
                Action();
            }
        }
        public void Delete(int pol)
        {
            users.Remove(users[pol]);
            Reader.Write(users, "users.json");
            Action();
        }

        static object Addition(int pol, string v)
        {
            Console.SetCursorPosition(12, pol + 2);
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

        static void Creature(User user, int p)
        {
            try
                {
                switch (p)
                {
                    case 0:
                        user.id = Convert.ToInt32(Addition(p, user.id > -1 ? Convert.ToString(user.id):""));
                        break;
                    case 1:
                        user.login = Convert.ToString(Addition(p, user.login));
                        break;
                    case 2:
                        user.password = Convert.ToString(Addition(p, user.password));
                        break;
                    case 3:
                        user.post_id = Convert.ToInt32(Addition(p, user.post_id > -1 ? Convert.ToString(user.post_id) : ""));
                        break;
                }
            }
                catch
                {
                Console.SetCursorPosition(p, 6);
                Console.WriteLine("Неверный ввод!");
            }
        }

        public void User_info(User kek, bool read, bool table = false, int pos = 0)
        {
            if (table == false)
            {
                Console.WriteLine($"  ID:       {(kek.id > -1 ? kek.id:"")}");
                Console.WriteLine($"  Login:    {kek.login}");
                Console.WriteLine($"  Password: {kek.password}");
                if (read)
                    try
                    {
                        Console.WriteLine($"  Post: {kek.post_id} {Enum.GetValues(typeof(Posts)).GetValue(kek.post_id)}");
                    }
                    catch
                    {
                        Console.Write($"  Post: НЕТ");
                    }
                else
                    Console.WriteLine($"  Post-id:  {(kek.post_id > -1 ? kek.post_id : "")}");
            }
            else
            {
                Console.SetCursorPosition(4, pos);
                Console.Write(kek.id);
                Console.SetCursorPosition(Console.WindowWidth / 5, pos);
                Console.Write(kek.login);
                Console.SetCursorPosition(Console.WindowWidth / 5 * 2, pos);
                Console.Write(kek.password);
                Console.SetCursorPosition(Console.WindowWidth / 5 * 3, pos);
                try
                {
                    Console.Write(Enum.GetValues(typeof(Posts)).GetValue(kek.post_id));
                }
                catch
                {
                    Console.Write("НЕТ");
                }
            }
        }
    }
}
