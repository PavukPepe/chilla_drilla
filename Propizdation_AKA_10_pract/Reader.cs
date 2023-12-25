using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propizdation_AKA_10_practos
{
    static class Reader
    {
        public static T Read<T>(string filename)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + "Pepe";
            string json = File.ReadAllText(desktop + "\\" + filename);
            T types = JsonConvert.DeserializeObject<T>(json);
            return types;
        }

        public static void Write<T>(T types, string filename)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + "Pepe";
            string json = JsonConvert.SerializeObject(types);
            File.WriteAllText(desktop + "\\" + filename, json);
        }
    }
}
