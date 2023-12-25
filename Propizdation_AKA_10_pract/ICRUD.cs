using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propizdation_AKA_10_practos
{
    internal interface ICRUD
    {
        void Action();
        void Search();
        void Create();
        void Update(int pol);
        void Delete(int pol);
        void Read(int pol);

    }
}
