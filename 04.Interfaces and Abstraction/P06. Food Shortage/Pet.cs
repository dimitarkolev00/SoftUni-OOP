using System;

namespace P04.BorderControl
{
    public class Pet : IBirthdate
    {
        private string name;
        public Pet(string name , string birthdate)
        {
            this.name = name;
            this.Birthdate = DateTime.ParseExact(birthdate,"dd/mm/yyyy",null);
        }
        public DateTime Birthdate { get;private set; }
    }
}
