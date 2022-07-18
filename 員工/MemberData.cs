using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 員工
{
    class MemberData
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool check_Tag()
        {
            return true;
        }

        private bool fn_CheckName()
        {
            if (Name == "") { return false; }
            else { return true; }
        }

        private bool fn_CheckBirthDate()
        {
            BirthDate.Replace('/', ' ');
            string[] check = BirthDate.Split(' ');

            return true;
        }


        public override string ToString()
        {
            return $" ('{Name}','{BirthDate}','{Phone}','{Email}','{Password}') ";
        }
    }
}
