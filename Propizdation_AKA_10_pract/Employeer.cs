using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propizdation_AKA_10_practos
{
    internal class Employeer
    {
        public int id;
        public string name;
        public string surname;
        public string midlename = "";
        public DateTime birthday;
        public int passport;
        public double salary;
        public int user_id;
        
        public Employeer(int id, string name, string surname, string midlename, DateTime birthday, int passport, double salary, int user_id) 
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.midlename = midlename;
            this.birthday = birthday;
            this.passport = passport;
            this.salary = salary;
            this.user_id = user_id;
        }

        public Employeer()
        {
            id = -1;
            passport = -1;
            salary = -1;
            user_id = -1;
        }

    }
}
