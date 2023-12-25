using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Propizdation_AKA_10_practos
{
    static class Menu
    {
        public static int Show(int minp, int maxp)
        {
            int pos = minp;
            ConsoleKeyInfo key;

            do
            {
                Console.SetCursorPosition(0, pos);
                Console.WriteLine("->");

                key = Console.ReadKey(true);

                Console.SetCursorPosition(0, pos);
                Console.WriteLine("  ");

                if (key.Key == ConsoleKey.DownArrow && pos != maxp + minp)
                    pos++;
                else if (key.Key == ConsoleKey.UpArrow && pos != minp)
                    pos--;
                else if (key.Key == ConsoleKey.Escape)
                    return (int)klavishi.Escape;
                else if (key.Key == ConsoleKey.F1)
                    return (int)klavishi.F1;
                else if (key.Key == ConsoleKey.Delete)
                    return (int)klavishi.Delete;
                else if (key.Key == ConsoleKey.S)
                    return (int)klavishi.S;
                else if (key.Key == ConsoleKey.F10)
                    return (int)klavishi.F10;
                else if (key.Key == ConsoleKey.F2)
                    return (int)klavishi.F2;
                else if (key.Key == ConsoleKey.Subtract)
                    return (int)klavishi.Minus;
                else if (key.Key == ConsoleKey.Add)
                    return (int)klavishi.Plus;
            } while (key.Key != ConsoleKey.Enter);
            return pos - minp;
        }

        public static int Button()
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                    return (int)klavishi.Escape;
                else if (key.Key == ConsoleKey.F1)
                    return (int)klavishi.F1;
                else if (key.Key == ConsoleKey.Delete)
                    return (int)klavishi.Delete;
                else if (key.Key == ConsoleKey.S)
                    return (int)klavishi.S;
                else if (key.Key == ConsoleKey.F10)
                    return (int)klavishi.F10;
                else if (key.Key == ConsoleKey.Subtract)
                    return (int)klavishi.Minus;
                else if (key.Key == ConsoleKey.Add)
                    return (int)klavishi.Plus;
            } while (true);
        }

        internal enum klavishi
        {
            Delete = 666,
            F1 = 500,
            F2 = 600,
            Escape = 100,
            S = 1000,
            F10 = 404,
            Plus = 333,
            Minus = 444
        }

    }
}