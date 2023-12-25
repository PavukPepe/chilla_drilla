using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Propizdation_AKA_10_practos.Menu;

namespace Propizdation_AKA_10_practos
{
    internal class Buh : ICRUD
    {
        List<Buh_notes> buh_notes = Reader.Read<List<Buh_notes>>("Buh_notes.json");


        public void Action()
        {
            Authorization.Welcome();
            int pos = 2;
            foreach (Buh_notes note in buh_notes)
            {
                Note_info(note, true, true, pos);
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
            var note = new Buh_notes();
            Authorization.Welcome();
            Note_info(note, false);

            int p = Menu.Show(2, 4);
            Creature(note, p);
            do
            {
                Authorization.Welcome();
                int pos = 2;
                foreach (Buh_notes note1 in buh_notes)
                {
                    if (note1.id == note.id || note1.name == note.name || note1.money == note.money || note1.prihod == note.prihod || note.date == note1.date)
                    {
                        Note_info(note1, true, true, pos);
                        pos++;
                    }
                }
                p = Menu.Button();
            } while (p != (int)klavishi.Escape);
            Action();

        }
        public void Read(int pol)
        {
            var note = buh_notes[pol];
            Authorization.Welcome();
            Note_info(note, true);
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
            var note = new Buh_notes();
            int p;
            do
            {
                Authorization.Welcome(); ;
                Note_info(note, false);
                p = Menu.Show(2, 4);
                Creature(note, p);
            } while (p != (int)klavishi.S && p != (int)klavishi.Escape);
            try
            {
                if (note.id != -1 && note.money != -1)
                {
                    buh_notes.Add(note);
                    Reader.Write(buh_notes, "Buh_notes.json");
                }
            }
            finally
            {
                Action();
            }
        }
        public void Update(int pol)
        {
            Buh_notes note = buh_notes[pol];
            int p;
            do
            {
                Console.Clear();
                Authorization.Welcome();
                Note_info(note, false);
                p = Menu.Show(2, 4);
                Creature(note, p);
            } while (p != (int)klavishi.S && p != (int)klavishi.Escape);
            try
            {
                Reader.Write(buh_notes, "Buh_notes.json");
            }
            finally
            {
                Action();
            }
        }
        public void Delete(int pol)
        {
            buh_notes.Remove(buh_notes[pol]);
            Reader.Write(buh_notes, "Buh_notes.json");
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

        static void Creature(Buh_notes note, int p)
        {
            try
            {
                switch (p)
                {
                    case 0:
                        note.id = Convert.ToInt32(Addition(p, note.id > -1 ? Convert.ToString(note.id) : ""));
                        break;
                    case 1:
                        note.name = Convert.ToString(Addition(p, note.name));
                        break;
                    case 2:
                        string bd = Convert.ToString(Addition(p, ""));
                        string[] date = bd.Split(".");
                        note.date = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0);
                        break;
                    case 3:
                        note.money = Convert.ToDouble(Addition(p, note.money > -1 ? Convert.ToString(note.money) : ""));
                        break;
                    case 4:
                        note.prihod = Convert.ToBoolean(Addition(p, Convert.ToString(note.prihod)));
                        break;
                }
            }
            catch
            {
                Console.SetCursorPosition(p, 6);
                Console.WriteLine("Неверный ввод!");
            }
        }

        public void Note_info(Buh_notes kek, bool read, bool table = false, int pos = 0)
        {
            if (table == false)
            {
                Console.WriteLine($"  ID:          {(kek.id > -1 ? kek.id : "")}");
                Console.WriteLine($"  Наименование:{kek.name}");
                Console.WriteLine($"  Дата:        {(kek.date.Year > 1 ? kek.date.ToLongDateString() : "")}");
                Console.WriteLine($"  Сумма:       {(kek.money > -1 ? kek.money : "")}");
                if (read)
                    Console.WriteLine($"  Статус:      {(kek.prihod ? "ДОХОД" : "РАСХОД")}");
                else Console.WriteLine($"  Статус:      {kek.prihod}");
            }
            else
            {
                Console.SetCursorPosition(4, pos);
                Console.Write(kek.id);
                Console.SetCursorPosition(Console.WindowWidth / 6, pos);
                Console.Write(kek.name);
                Console.SetCursorPosition(Console.WindowWidth / 6 * 2, pos);
                Console.Write(kek.date);
                Console.SetCursorPosition(Console.WindowWidth / 6 * 3, pos);
                Console.Write(kek.money);
                Console.SetCursorPosition(Console.WindowWidth / 6 * 4, pos);
                Console.Write(kek.prihod ? "ДОХОД" : "РАСХОД");
            }
        }
    }
}
