using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propizdation_AKA_10_practos
{
    internal class User
    {
        public int id;
        public string login;
        public string password;
        public int post_id;

        public User(int id, string login, string password, int post_id)
        {
            this.id = id;
            this.login = login;
            this.password = password;
            this.post_id = post_id;
        }

        public User()
        {
            this.id = -1;
            this.post_id = -1;
        }
    }

    internal enum Posts
    {
        Admin,
        HR,
        Storager,
        Kassir,
        Buh
    }
}
