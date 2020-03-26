using System.Collections.Generic;

namespace P04_Hospital
{
    public class Room
    {
        private const int MAX_CAPACITY = 3;
        private readonly List<Patient> patients;
        private Room()
        {
            this.patients = new List<Patient>();
        }
        public Room(byte number)
            : this()
        {
            this.Number = number;
        }
        public byte Number { get; }
        public int Count => this.patients.Count;
        public IReadOnlyCollection<Patient> Patients => this.patients;
        public void AddPatient(Patient patient)
        {
            if (this.Count < MAX_CAPACITY)
            {
                this.patients.Add(patient);
            }
        }
    }
}
