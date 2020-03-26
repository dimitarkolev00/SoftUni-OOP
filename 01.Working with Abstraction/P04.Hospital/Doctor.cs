using System.Collections.Generic;

namespace P04_Hospital
{
    public class Doctor
    {
        private readonly List<Patient> patients;
        private Doctor()
        {
            this.patients = new List<Patient>();
        }
        public Doctor(string firstName,string lastName)
            :this()
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
        public string FirstName { get; }
        public string LastName { get; }
        public IReadOnlyCollection<Patient> Patients => this.patients;
    }
}
