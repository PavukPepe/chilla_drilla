using Propizdation_AKA_10_practos;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Threading.Channels;
using static Propizdation_AKA_10_practos.Authorization;

namespace Propizdation_AKA_10_practos
{
    internal class Authorization
    {
        static public User activ = new User();
        static bool flag = false;
        public static void Main(string[] args)
        {
            if (!flag)
            {
                foreach (var arg in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))) 
                {
                    if (arg == (Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) + "\\" + "Pepe")
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + "Pepe");
                    List<User> users = new List<User>() { new User(0, "admin", "admin", 0) };
                    List<Employeer> employeers = new List<Employeer>();
                    List<Buh_notes> buh_Notes = new List<Buh_notes>();
                    List<Product> products = new List<Product>();
                    Reader.Write(buh_Notes, "Buh_notes.json");
                    Reader.Write(employeers, "employeers.json");
                    Reader.Write(products, "products.json");
                    Reader.Write(users, "users.json");
                    flag = true;
                }
            }
            Author();
        }

        public static void Welcome()
        {
            User user = activ;
            List<User> users = Reader.Read<List<User>>("users.json");
            List<Employeer> employeers = Reader.Read<List<Employeer>>("employeers.json");
            string name = user.login;
            foreach (Employeer employeer in employeers) 
            {
                if (employeer.user_id == user.id)
                {
                    name = employeer.name;
                }
            }
            int pos = 0;
            Console.Clear();
            Console.Write("Сексшоп у Кузьмича");
            Console.SetCursorPosition(Console.WindowWidth / 3, pos);
            Console.Write($"Добро пожаловать {name}");
            Console.SetCursorPosition(Console.WindowWidth / 3 * 2, pos);
            Console.WriteLine($"Роль: {Enum.GetValues(typeof(Posts)).GetValue(user.post_id)}");
            for (int i = 0; i < Console.WindowWidth; i++) { Console.Write("-"); }
        }

        public static void Author() 
        {
            string login = "", password = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Сексшоп у Кузьмича");
                for (int i = 0; i < Console.WindowWidth; i++) { Console.Write("-"); }
                Console.WriteLine($"  Логин: {login}");
                Console.Write("  Пароль: ");
                for (int i = 0; i < password.Length; i++) { Console.Write("*"); }
                Console.WriteLine();
                Console.WriteLine("  Авторизоваться");
                int pos = (Menu.Show(2, 2));

                if (pos == (int)position.login)
                {
                    Console.SetCursorPosition(9, pos + 2);
                    ConsoleKeyInfo n;
                    Console.Write(login);
                    do
                    {
                        n = Console.ReadKey();
                        if (n.Key == ConsoleKey.Backspace)
                        {
                            if (login.Length == 0)
                            {
                                break;
                            }
                            login = login.Substring(0, login.Length - 1);
                            Console.Write(' ');
                            Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, pos + 2);
                            continue;
                        }
                        if (n.KeyChar != '\r' && n.KeyChar != '\0')
                        {
                            login += n.KeyChar.ToString();
                        }

                    } while (n.Key != ConsoleKey.DownArrow && n.Key != ConsoleKey.UpArrow && n.Key != ConsoleKey.Enter);
                }
                if (pos == (int)position.password)
                {
                    Console.SetCursorPosition(10, pos + 2);
                    ConsoleKeyInfo n;
                    for (int i = 0; i < password.Length; i++) { Console.Write("*"); }
                    do
                    {
                        n = Console.ReadKey(true);
                        if (n.Key == ConsoleKey.Backspace)
                        {
                            if (password.Length == 0)
                            {
                                break;
                            }
                            password = password.Substring(0, password.Length - 1);
                            Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, pos + 2);
                            Console.Write(' ');
                            Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, pos + 2);
                            continue;
                        }
                        if (n.KeyChar != '\r' && n.KeyChar != '\0')
                        {
                            Console.Write("*");
                            password += n.KeyChar.ToString();
                        }

                    } while (n.Key != ConsoleKey.DownArrow && n.Key != ConsoleKey.UpArrow && n.Key != ConsoleKey.Enter);
                }
                if (pos == (int)position.auth)
                {
                    List<User> users = Reader.Read<List<User>>("users.json");
                    foreach (User user in users)
                    {
                        activ = user;
                        if (user.password == password && user.login == login && user.post_id == (int)Posts.Admin)
                        {
                            Admin admin = new Admin();
                            MakeMenu(admin);
                        }
                        else if (user.password == password && user.login == login && user.post_id == (int)Posts.HR)
                        {
                            HR hr = new HR();
                            MakeMenu(hr);
                        }
                        else if (user.password == password && user.login == login && user.post_id == (int)Posts.Storager)
                        {
                            Storager storager = new Storager();
                            MakeMenu(storager);
                        }
                        else if (user.password == password && user.login == login && user.post_id == (int)Posts.Kassir)
                        {
                            Kassir kassir = new Kassir();
                            MakeMenu(kassir);
                        }
                        else if (user.password == password && user.login == login && user.post_id == (int)Posts.Buh)
                        {
                            Buh buh = new Buh();
                            MakeMenu(buh);
                        }
                    }
                }

            }
        }

        static void MakeMenu(ICRUD cRUD)
        {
            for (int i = 0; i < Console.WindowWidth; i++) { Console.Write("-"); }
            cRUD.Action();
        }

        internal enum position
        {
            login,
            password,
            auth
        }

    }
}